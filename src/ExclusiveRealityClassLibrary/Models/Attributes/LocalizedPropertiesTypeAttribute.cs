using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ExclusiveReality.Models.Attributes
{
    [Serializable, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class LocalizedPropertiesTypeAttribute : Attribute
    {
        private Type arType;

        public LocalizedPropertiesTypeAttribute(Type arType)
        {
            this.arType = arType;
        }

        public Type ARType
        {
            get { return arType; }
        }
    }
}
