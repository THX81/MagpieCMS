namespace ExclusiveReality.Utils
{
    using Castle.MonoRail.Framework;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Text;

    public static class CommonUtils
    {
        public static string BuildQueryString(IServerUtility serverUtil, IDictionary parameters, bool encodeAmp)
        {
            if ((parameters == null) || (parameters.Count == 0))
            {
                return string.Empty;
            }
            if (serverUtil == null)
            {
                throw new ArgumentNullException("serverUtil");
            }
            object[] objArray = new object[1];
            StringBuilder builder = new StringBuilder();
            foreach (DictionaryEntry entry in parameters)
            {
                if (entry.Value == null)
                {
                    continue;
                }
                IEnumerable enumerable = objArray;
                if (!(entry.Value is string) && (entry.Value is IEnumerable))
                {
                    enumerable = (IEnumerable) entry.Value;
                }
                else
                {
                    objArray[0] = entry.Value;
                }
                foreach (object obj2 in enumerable)
                {
                    string str = serverUtil.UrlEncode(Convert.ToString(obj2, CultureInfo.CurrentCulture));
                    builder.Append(serverUtil.UrlEncode(entry.Key.ToString())).Append('=').Append(str);
                    if (encodeAmp)
                    {
                        builder.Append("&amp;");
                        continue;
                    }
                    builder.Append("&");
                }
            }
            return builder.ToString();
        }

        public static string BuildQueryString(IServerUtility serverUtil, NameValueCollection parameters, bool encodeAmp)
        {
            if ((parameters == null) || (parameters.Count == 0))
            {
                return string.Empty;
            }
            if (serverUtil == null)
            {
                throw new ArgumentNullException("serverUtil");
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in parameters.Keys)
            {
                if (str == null)
                {
                    continue;
                }
                foreach (string str2 in parameters.GetValues(str))
                {
                    builder.Append(serverUtil.UrlEncode(str)).Append('=').Append(serverUtil.UrlEncode(str2));
                    if (encodeAmp)
                    {
                        builder.Append("&amp;");
                    }
                    else
                    {
                        builder.Append("&");
                    }
                }
            }
            return builder.ToString();
        }

        internal static void MergeOptions(IDictionary userOptions, IDictionary defaultOptions)
        {
            foreach (DictionaryEntry entry in defaultOptions)
            {
                if (!userOptions.Contains(entry.Key))
                {
                    userOptions[entry.Key] = entry.Value;
                }
            }
        }

        internal static string ObtainEntry(IDictionary attributes, string key)
        {
            if ((attributes != null) && attributes.Contains(key))
            {
                return (string) attributes[key];
            }
            return null;
        }

        internal static string ObtainEntry(IDictionary attributes, string key, string defaultValue)
        {
            string str = ObtainEntry(attributes, key);
            if (str == null)
            {
                return defaultValue;
            }
            return str;
        }

        internal static string ObtainEntryAndRemove(IDictionary attributes, string key)
        {
            string str = null;
            if ((attributes != null) && attributes.Contains(key))
            {
                str = (string) attributes[key];
                attributes.Remove(key);
            }
            return str;
        }

        internal static string ObtainEntryAndRemove(IDictionary attributes, string key, string defaultValue)
        {
            string str = ObtainEntryAndRemove(attributes, key);
            if (str == null)
            {
                return defaultValue;
            }
            return str;
        }

        internal static object ObtainObjectEntryAndRemove(IDictionary attributes, string key)
        {
            object obj2 = null;
            if ((attributes != null) && attributes.Contains(key))
            {
                obj2 = attributes[key];
                attributes.Remove(key);
            }
            return obj2;
        }
    }
}

