using System.Collections;
using System.Reflection;
using F23.PlistParser.Internal.Model;

namespace F23.PlistParser.Internal;

internal static class ObjectTableObjectMapper<T> where T : new()
{
    public static T MapObject(ObjectTable objectTable)
    {
        if (!objectTable.TopLevelIsDictionary)
        {
            throw new ArgumentOutOfRangeException(nameof(objectTable),
                "MapObject<T>() requires plist with top-level dictionary.");
        }

        var dictionary = objectTable.TopLevelDictionary;

        var type = typeof(T);

        T result = (T)MapDictionary(dictionary, type);

        return result;
    }

    internal static object ChangeGenericListType(IList<object> items, Type type, bool performConversion = false)
    {
        var containedType = type.GenericTypeArguments.First();
        var castMethod = GetEnumerableCastMethod(containedType);
        var toListMethod = GetEnumerableToListMethod(containedType);

        var itemsToCast = performConversion ? items.Select(item => Convert.ChangeType(item, containedType)) : items;

        var castedItems = castMethod.Invoke(null, new object?[] { itemsToCast });

        return toListMethod.Invoke(null, new[] { castedItems })!;
    }
    
    internal static MethodInfo GetEnumerableCastMethod(Type containedType) =>
        typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))!.MakeGenericMethod(containedType);

    internal static MethodInfo GetEnumerableToListMethod(Type containedType) =>
        typeof(Enumerable).GetMethod(nameof(Enumerable.ToList))!.MakeGenericMethod(containedType);
    
    private static object MapDictionary(IDictionary<string, object> dictionary, Type targetType)
    {
        var properties = targetType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite)
            .ToList();

        var result = Activator.CreateInstance(targetType)
                     ?? throw new InvalidOperationException($"Failed to activate type {targetType.Name}");

        foreach (var property in properties)
        {
            if(!dictionary.TryGetValue(property.Name, out var value))
            {
                // TODO.JB - "Strict" behavior that throws here instead?
                continue;
            }

            var propertyResult = MapProperty(property, value);
            property.SetValue(result, propertyResult);
        }

        return result;
    }

    private static object MapProperty(PropertyInfo property, object value)
    {
        var targetType = property.PropertyType;

        object propertyResult;

        if (SimpleTypes.Contains(targetType))
        {
            propertyResult = value;
        }
        else if (IsDictionaryType(targetType))
        {
            var dictValue = (IDictionary<string, object>)value;
            propertyResult = MapDictionary(dictValue, targetType);
        }
        else if (IsArrayType(targetType))
        {
            propertyResult = MapArray(value, targetType);
        }
        else
        {
            throw new NotSupportedException($"Unsupported target type: {targetType.FullName}");
        }

        return propertyResult;
    }

    private static object MapArray(object value, Type targetType)
    {
        var typeParameter = targetType.GetGenericArguments().Single();

        if (value is not IList list)
        {
            throw new InvalidOperationException($"Can't convert {value.GetType().Name} to IList.");
        }

        var isSimpleType = SimpleTypes.Contains(typeParameter);

        var listResult = list.Cast<object>()
            .Select(i => isSimpleType
                ? i
                : MapDictionary((IDictionary<string, object>)i, typeParameter))
            .ToList();

        return ChangeGenericListType(listResult, targetType);
    }

    private static readonly Type[] SimpleTypes = 
    {
        typeof(bool),

        typeof(short),
        typeof(int),
        typeof(long),

        typeof(float),
        typeof(double),

        typeof(DateTime),

        //typeof(byte[]), // TODO.JB
        typeof(string),
        //typeof(Guid) // TODO.JB
    };

    private static bool IsArrayType(Type targetType) =>
        // TODO.JB - Other list/array-like types?
        targetType.IsGenericType &&
        targetType.GetGenericTypeDefinition() == typeof(IList<>);

    private static bool IsDictionaryType(Type targetType) =>
        targetType.IsGenericType &&
        targetType.GetGenericTypeDefinition() == typeof(IDictionary<,>);
}