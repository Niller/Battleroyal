using System;

namespace Reflection
{
    public class AttributeInfo
    {
        public Type ClassType;
        public Attribute Attribute;

        public AttributeInfo(Type classType, Attribute attribute)
        {
            ClassType = classType;
            Attribute = attribute;
        }
    }
}