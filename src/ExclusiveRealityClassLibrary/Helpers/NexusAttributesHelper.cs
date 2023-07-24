using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;

namespace ExclusiveReality.Helpers
{
    public static class NexusAttributeHelper<T> where T : Attribute
    {
        public static Collection<PropertyInfo> GetPropertiesWhereIs(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            Collection<PropertyInfo> result = new Collection<PropertyInfo>();

            foreach (PropertyInfo prop in instance.GetType().GetProperties())
            {
                object[] classTmpAtts = prop.GetCustomAttributes(typeof(T), true);
                if (classTmpAtts.Length > 0)
                    result.Add(prop);
            }

            return result;
        }
    }
}
