using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ExclusiveReality.Models.Attributes
{
    [Serializable, AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ClassLocalizationAttribute : Attribute
    {
        private String twoLetterISOLanguageName;
        private String itemText;
        private String itemsText;

        public ClassLocalizationAttribute(String twoLetterISOLanguageName, String itemText, String itemsText)
        {
            this.twoLetterISOLanguageName = twoLetterISOLanguageName;
            this.itemText = itemText;
            this.itemsText = itemsText;
        }


        public String TwoLetterISOLanguageName
        {
            get { return twoLetterISOLanguageName; }
            set { twoLetterISOLanguageName = value; }
        }

        public String ItemText
        {
            get { return itemText; }
            set { itemText = value; }
        }

        public String ItemsText
        {
            get { return itemsText; }
            set { itemsText = value; }
        }


    }
}
