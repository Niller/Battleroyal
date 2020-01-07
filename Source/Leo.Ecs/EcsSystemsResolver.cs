using System;
using System.Collections.Generic;
using Reflection;

namespace Leopotam.Ecs
{
    public class EcsSystemsResolver
    {
        //TODO [Alexander Borisov] Use pool for systems

        private readonly Dictionary<Type, Type> _systemTypes = new Dictionary<Type, Type>(); 

        public void Init()
        {
            var overrides = ReflectionManager.GetAttributes<SystemOverrideAttribute>();
            foreach (var attributeInfo in overrides)
            {
                var overrideAttribute = (SystemOverrideAttribute) attributeInfo.Attribute;
                var baseType = overrideAttribute.Type;
                if (_systemTypes.ContainsKey(baseType))
                {
                    throw new Exception($"There are 2 or more overrides of system {baseType.FullName}");
                }

                _systemTypes[baseType] = attributeInfo.ClassType;
            }
        }

        public T Resolve<T>() where T : IEcsSystem, new()
        {
            T system;
            if (_systemTypes.TryGetValue(typeof(T), out var systemType))
            {
                system = (T)Activator.CreateInstance(systemType);
            }
            else
            {
                system = new T();
            }

            return system;
        }
    }
}