using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ExclusiveReality.Models.Attributes
{
    [Serializable, AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormGeneratorBehaviorAttribute : Attribute
    {
        private FormControlType formControlType;

        public FormGeneratorBehaviorAttribute(FormControlType formControlType)
        {
            this.formControlType = formControlType;
        }


        public FormControlType FormControlType
        {
            get { return formControlType; }
            set { formControlType = value; }
        }


    }

    public enum FormControlType { WYSIWYG, Auto, Hidden }
}
