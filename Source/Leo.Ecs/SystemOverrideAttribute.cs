using System;

namespace Leopotam.Ecs
{
    public class SystemOverrideAttribute : Attribute
    {
        public Type Type
        {
            get;
        }
        public SystemOverrideAttribute(Type type)
        {
            Type = type;
        }
    }
}