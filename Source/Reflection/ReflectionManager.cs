using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reflection
{
    public static class ReflectionManager
    {
        //Referenced assemblies
        private static readonly Dictionary<Assembly, string[]> Assemblies = new Dictionary<Assembly, string[]>();
        private static readonly Dictionary<Type, List<AttributeInfo>> Attributes = new Dictionary<Type, List<AttributeInfo>>();

        static ReflectionManager()
        {
            CollectAssemblies();
        }


        private static void CollectAssemblies()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var referencedAssemblies = assembly.GetReferencedAssemblies();
                var referencedAssembliesNames = new string[referencedAssemblies.Length];

                for (int i = 0, l = referencedAssemblies.Length; i < l; ++i)
                {
                    referencedAssembliesNames[i] = referencedAssemblies[i].Name;
                }

                Assemblies[assembly] = referencedAssembliesNames;
            }
        }

        public static IEnumerable<Type> AvailableTypes(Type assemblyFilter = null)
        {
            var assemblies = assemblyFilter == null
                ? AppDomain.CurrentDomain.GetAssemblies()
                : GetAssembliesUtilize(assemblyFilter);

            foreach (var assembly in assemblies)
            {
                Type[] assemblyTypes;
                try
                {
                    assemblyTypes = assembly.GetTypes();
                }
                catch (Exception)
                {
                    continue;
                }

                for (int j = 0, typesLen = assemblyTypes.Length; j < typesLen; ++j)
                {
                    yield return assemblyTypes[j];
                }
            }
        }


        public static List<AttributeInfo> GetAttributes<T>() where T : Attribute
        {
            var attributeType = typeof(T);
            if (Attributes.TryGetValue(attributeType, out var result))
            {
                return result;
            }

            var types = AvailableTypes(attributeType);
            result = new List<AttributeInfo>();
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<T>();
                if (attribute != null)
                {
                    result.Add(new AttributeInfo(type, attribute));
                }
            }

            Attributes[attributeType] = result;
            return result;
        }

        public static Dictionary<string, Type> GetInterfacesOf<TInterface>()
            where TInterface : class
        {
            return GetInterfaceOrImplementationOf<TInterface>(false);
        }

        public static Dictionary<string, Type> GetImplementationsOf<TInterface>()
            where TInterface : class
        {
            return GetInterfaceOrImplementationOf<TInterface>(true);
        }

        public static Dictionary<string, Type> GetSubclassesOf<TBaseClass>(bool useFilter = true)
            where TBaseClass : class
        {
            var baseType = typeof(TBaseClass);
            return GetSubclassesOf(baseType);
        }

        public static Dictionary<string, Type> GetSubclassesOf(Type baseType, bool useFilter = true)
        {
            var result = new Dictionary<string, Type>();

            foreach (var type in AvailableTypes(useFilter ? baseType : null))
            {
                if (type.IsSubclassOf(baseType))
                {
                    result[type.Name] = type;
                }
            }

            return result;
        }

        private static Dictionary<string, Type> GetInterfaceOrImplementationOf<TInterface>(bool impl)
            where TInterface : class
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new NotSupportedException("You need to pass interface into GetInterfacesOf().");
            }

            var result = new Dictionary<string, Type>();

            foreach (var type in AvailableTypes(typeof(TInterface)))
            {
                if ((impl ? type.IsClass : type.IsInterface) && type.GetInterface(typeof(TInterface).Name) != null)
                {
                    result[type.Name] = type;
                }
            }

            return result;
        }

        private static IEnumerable<Assembly> GetAssembliesUtilize(Type type)
        {
            var main = type.Assembly;
            var mainAssemblyName = main.GetName().Name;
            var result = new List<Assembly> { main };

            foreach (var assembly in Assemblies)
            {
                var len = assembly.Value.Length;
                for (var i = 0; i < len; ++i)
                {
                    if (assembly.Value[i] != mainAssemblyName)
                    {
                        continue;
                    }

                    result.Add(assembly.Key);
                    break;
                }
            }

            return result;
        }
    }
}
