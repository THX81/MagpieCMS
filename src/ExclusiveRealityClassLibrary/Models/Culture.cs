using System;
using System.Globalization;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord]
    [ClassLocalization("cs", "Kultura", "Kultury")]
    [ClassLocalization("en", "Culture", "Cultures")]
    public class Culture : BusinessObjectBase<Culture>
    {
        public Culture() {}

        public Culture(Int32 id)
        {
            this.id = id;
            this.IsDefault = false;
        }

        public Culture(Int32 id, bool isDefault)
        {
            this.id = id;
            this.IsDefault = isDefault;
        }

        public Culture(CultureInfo cultureInfo)
        {
            id = cultureInfo.LCID;
            this.IsDefault = false;
        }

        [PrimaryKey(Generator = PrimaryKeyType.Assigned)]
        public new int Id
        {
            get { return id; }
            set { id = value; }
        }

        [PropertyLocalization("cs", "Jako standardní kultura")]
        [PropertyLocalization("en", "As default culture")]
        [Property("IsDefault")]
        public bool IsDefault { get; set; }

        public static Culture GetDefaultCulture()
        {
            Culture[] culture =
                new SimpleQuery<Culture>(typeof (Culture), "from Culture c where c.IsDefault = 1").Execute();
            if (culture.Length > 0)
            {
                return culture[0];
            }
            return null;
        }

        public override string ToString()
        {
            if (id > 0)
            {
                return new CultureInfo(id).TwoLetterISOLanguageName;
            }

            return base.ToString();
        }
    }
}