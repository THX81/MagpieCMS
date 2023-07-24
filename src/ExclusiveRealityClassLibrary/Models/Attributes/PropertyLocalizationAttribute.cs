using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ExclusiveReality.Models.Attributes
{
    [Serializable, AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyLocalizationAttribute : Attribute
    {
        private String twoLetterISOLanguageName;
        private String text;

        public PropertyLocalizationAttribute(String twoLetterISOLanguageName, String text)
        {
            this.twoLetterISOLanguageName = twoLetterISOLanguageName;
            this.text = text;
        }


        public String TwoLetterISOLanguageName
        {
            get { return twoLetterISOLanguageName; }
            set { twoLetterISOLanguageName = value; }
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }


    }
}
