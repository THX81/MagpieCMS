using System;
using Castle.ActiveRecord;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord]
    [ClassLocalization("cs", "�ablona str�nky", "�ablony str�nek")]
    [ClassLocalization("en", "Page template", "Pages templates")]
    public class PageTemplate : BusinessObjectBase<PageTemplate>
    {
        [PropertyLocalization("cs", "Popisn� titulek")]
        [PropertyLocalization("en", "Descriptive title")]
        [Property("Title")]
        public string Title { get; set; }

        [PropertyLocalization("cs", "N�zev (mus� se shodovat s n�zvem souboru �ablony)")]
        [PropertyLocalization("en", "Name (must be same as templates file name)")]
        [Property("Name")]
        public string Name { get; set; }

        [BelongsTo("EstateOfferTypeId")]
        public virtual EstateOfferType EstateOfferType { get; set; }

        [BelongsTo("EstateTypeId")]
        public virtual EstateType EstateType { get; set; }

        [Property("ForeignProject")]
        public bool ForeignProject { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Title))
            {
                return this.Title;
            }
            if (!String.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }

            return base.ToString();
        }
    }
}