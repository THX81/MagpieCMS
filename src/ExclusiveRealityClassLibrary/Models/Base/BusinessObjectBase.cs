using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using Castle.ActiveRecord;
using ExclusiveReality.Models.Attributes;

namespace ExclusiveReality.Models.Base
{
    public abstract class BusinessObjectBase<T> : ActiveRecordBase<T>
    {
        protected DateTime created = DateTime.Now;
        protected int id;
        protected IList localizedObjects = new ArrayList();

        [PropertyLocalization("cs", "Id")]
        [PropertyLocalization("cs", "Id")]
        [PrimaryKey]
        public virtual int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [PropertyLocalization("cs", "Datum vytvoøení")]
        [PropertyLocalization("en", "Date of creation")]
        [Property("Created")]
        public virtual DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }

        public virtual IList LocalizedObjects
        {
            get { return this.localizedObjects; }
            set { this.localizedObjects = value; }
        }

        public virtual Object GetLocalizedProperties()
        {
            return this.GetLocalizedProperties(Thread.CurrentThread.CurrentCulture);
        }

        public virtual Object GetLocalizedProperties(CultureInfo culture)
        {
            foreach (Object props in this.LocalizedObjects)
            {
                if (props.GetType().GetProperty("Culture") != null)
                {
                    if (((Culture) props.GetType().GetProperty("Culture").GetValue(props, null)).Id == culture.LCID)
                    {
                        return props;
                    }
                }
            }

            if (this.localizedObjects.Count > 0)
            {
                return this.localizedObjects[0];
            }

            return null;
        }

        public static T GetById(int id)
        {
            return FindByPrimaryKey(id, false);
        }
    }
}