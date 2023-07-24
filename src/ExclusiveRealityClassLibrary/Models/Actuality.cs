using System;
using System.Collections;
using System.Threading;
using Castle.ActiveRecord;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord, LocalizedPropertiesType(typeof (ActualityCulture))]
    [ClassLocalization("cs", "Aktualita", "Aktuality")]
    [ClassLocalization("en", "Newsreel", "Newsreel")]
    public class Actuality : BusinessObjectBase<Actuality>
    {
        protected bool publish;

        [HasMany(typeof (ActualityCulture), Table = "ActualityCulture", ColumnKey = "ActualityId", Inverse = true,
            Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public override IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }

        [PropertyLocalization("cs", "Publikovat")]
        [PropertyLocalization("en", "Publish")]
        [Property("Publish")]
        public bool Publish
        {
            get { return this.publish; }
            set { this.publish = value; }
        }

        public string GetUrl()
        {
            string url = "/aktuality.aspx?detail=" + Id;

            return url;
        }

        public ActualityCulture GetLocalizedObject()
        {
            foreach (ActualityCulture a in this.LocalizedObjects)
            {
                if (Thread.CurrentThread.CurrentCulture.LCID == a.Culture.Id)
                {
                    return a;
                }
            }
            return this.LocalizedObjects[0] as ActualityCulture;
        }

        public override string ToString()
        {
            foreach (ActualityCulture item in this.LocalizedObjects)
            {
                if (item.Culture.Id == Thread.CurrentThread.CurrentCulture.LCID)
                {
                    return item.Heading;
                }
            }
            return base.ToString();
        }
    }

    [ActiveRecord]
    public class ActualityCulture : LocalizationObjectBase<ActualityCulture>
    {
        [BelongsTo("ActualityId")]
        public Actuality Actuality { get; set; }

        [PropertyLocalization("cs", "Nadpis")]
        [PropertyLocalization("en", "Heading")]
        [Property("Heading")]
        public string Heading { get; set; }

        [PropertyLocalization("cs", "Perex")]
        [PropertyLocalization("en", "Perex")]
        [Property("Perex", ColumnType = "StringClob")]
        public string Perex { get; set; }

        [PropertyLocalization("cs", "Obsah")]
        [PropertyLocalization("en", "Content")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        [Property("Content", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        public string Content { get; set; }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Heading))
            {
                return this.Heading;
            }
            return base.ToString();
        }
    }
}