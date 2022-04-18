using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DFMS.Shared.Tools
{
    public static class ReflectionToolbox
    {
        public static List<Type> GetAllNonAbstractSubclasses(Type type)
        {
            return Assembly
                .GetAssembly(type)
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type))
                .ToList();
        }

        public static List<Type> GetAllNonAbstractImplementingInterface(Type @interface)
        {
            return Assembly
                .GetAssembly(@interface)
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && @interface.IsAssignableFrom(t))
                .ToList();
        }

        public static object CallPropertyMethod(object obj, string propertyName, string methodName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            var method = property.PropertyType.GetMethod(methodName);
            var value = method.Invoke(property.GetValue(obj), null);

            return value;
        }

        public static object GetObjectPropertyValue(object obj, string propertyName)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new MissingMemberException($"Type [{type.Name}] does not have [{propertyName}] property");
            return property.GetValue(obj);
        }

        public static T GetObjectPropertyValue<T>(object obj, string propertyName)
        {
            return (T)GetObjectPropertyValue(obj, propertyName);
        }

        public static object GetEnumAsJsonObject<T>() where T : Enum
        {
            var result = new List<object>();
            var enumValues = Enum.GetValues(typeof(T));

            foreach (int value in enumValues)
            {
                result.Add(new { value, name = Enum.GetName(typeof(T), value) });
            }

            return result;
        }
    }
}
