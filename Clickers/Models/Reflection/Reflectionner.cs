using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Reflection
{
    class Reflectionner
    {
        public Dictionary<String, Object> ReadClass<T>()
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();
            var props = typeof(T).GetProperties();
            T item = (T)Activator.CreateInstance(typeof(T));
            foreach (var prop in props)
            {
                result.Add(prop.Name, prop.GetValue(item, null));
            }

            return result;
        }

        public Dictionary<String, Object> ReadList<T>(List<T> list)
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();
            var props = typeof(T).GetProperties();
            foreach (T item in list)
            {
                foreach (var prop in props)
                {
                    result.Add(prop.Name, prop.GetValue(item));
                }
            }

            return result;
        }

        public Dictionary<String, Object> ReadList<T>(T list) where T : System.Collections.IList
        {
            Dictionary<String, Object> result = new Dictionary<string, object>();
            foreach (Object item in list)
            {
                var props = item.GetType().GetProperties();
                foreach (var prop in props)
                {
                    result.Add(prop.Name, prop.GetValue(item, null));
                }
            }

            return result;
        }

        public bool IsGenericList(object o)
        {
            bool isGenericList = false;

            var oType = o.GetType();

            if (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)))
                isGenericList = true;

            return isGenericList;
        }

        public bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
