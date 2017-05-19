using Clickers.Models.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Generators
{
    class EntityGenerator<T> where T : class
    {
        private Reflectionner reflectionner;
        private Dictionary<String, Object> itemProperties;

        public EntityGenerator()
        {
            reflectionner = new Reflectionner();

            if (typeof(T).Name.Equals(TypeEnum.LIST))
            {
                var type = Type.GetType(typeof(List<T>).AssemblyQualifiedName);
                var list = (List<T>)Activator.CreateInstance(type);
                itemProperties = reflectionner.ReadClass<T>();
            }
            else
            {
                itemProperties = reflectionner.ReadClass<T>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inheritance"></param>
        /// <returns></returns>
     /*   public T GenerateItem(Int32 inheritance = 2)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            if (inheritance > 0)
            {
                inheritance--;

                foreach (var item in itemProperties)
                {
                    PropertyInfo property = typeof(T).GetProperty(item.Key);
                    if (property.CanWrite && property.GetSetMethod( true).IsPublic)
                    {
                        switch (property.PropertyType.Name)
                        {
                            case TypeEnum.INT32:
                                property.SetValue(result, Faker.Number.RandomNumber(Int32.MaxValue));
                                break;
                            case TypeEnum.INT:
                                property.SetValue(result, Faker.Number.RandomNumber(Int32.MaxValue));
                                break;
                            case TypeEnum.STRING:
                                property.SetValue(result, Faker.Name.FullName());
                                break;
                         case TypeEnum.LIST:
                                object list = Activator.CreateInstance(
                                    typeof(List<>).MakeGenericType(property.PropertyType.GetGenericArguments()));
                                property.SetValue(result, list);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return result;
        }*/

       /* public IEnumerable<T> GenerateListItems()
        {
            List<T> result = (List<T>)Activator.CreateInstance(typeof(List<T>));
            for (int i = 0; i < Faker.Number.RandomNumber(0, 100); i++)
            {
                result.Add(GenerateItem());
            }
            return result;
        }*/
    }
}
