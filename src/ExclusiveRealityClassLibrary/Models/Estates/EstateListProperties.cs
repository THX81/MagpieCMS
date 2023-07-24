using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using Castle.ActiveRecord;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord]
    [ClassLocalization("cs", "Realitní poradce", "Realitní poradci")]
    [ClassLocalization("en", "Broker", "Brokers")]
    public class EstateManCard : BusinessObjectBase<EstateManCard>
    {
        private string firstName;
        private string lastName;
        private string title;

        [PropertyLocalization("cs", "Titul")]
        [PropertyLocalization("en", "Title")]
        [Property("Title")]
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        [PropertyLocalization("cs", "Jméno")]
        [PropertyLocalization("en", "First Name")]
        [Property("FirstName")]
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }

        [PropertyLocalization("cs", "Pøíjmení")]
        [PropertyLocalization("en", "Last Name")]
        [Property("LastName")]
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }

        [PropertyLocalization("cs", "Email")]
        [PropertyLocalization("en", "Email")]
        [Property("Email")]
        public string Email { get; set; }

        [PropertyLocalization("cs", "Kontaktní telefon")]
        [PropertyLocalization("en", "Telephone")]
        [Property("Telephone")]
        public string Telephone { get; set; }

        [PropertyLocalization("cs", "Mobilní telefon")]
        [PropertyLocalization("en", "Mobil phone")]
        [Property("Mobil")]
        public string Mobil { get; set; }


        public override string ToString()
        {
            return (String.IsNullOrEmpty(this.title) ? String.Empty : this.title + " ") + this.firstName + " "
                   + this.lastName;
        }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Spoleènost", "Spoleènosti")]
    [ClassLocalization("en", "Company", "Companies")]
    public class CompanyInfo : BusinessObjectBase<CompanyInfo>
    {
        private string firmName;

        [PropertyLocalization("cs", "Název spoleènosti")]
        [PropertyLocalization("en", "Company name")]
        [Property("FirmName")]
        public string FirmName
        {
            get { return this.firmName; }
            set { this.firmName = value; }
        }

        [PropertyLocalization("cs", "Ulice")]
        [PropertyLocalization("en", "Street")]
        [Property("Street")]
        public string Street { get; set; }

        [PropertyLocalization("cs", "Mìsto")]
        [PropertyLocalization("en", "City")]
        [Property("City")]
        public string City { get; set; }

        [PropertyLocalization("cs", "PSÈ")]
        [PropertyLocalization("en", "Zip")]
        [Property("Zip")]
        public string Zip { get; set; }

        [PropertyLocalization("cs", "Zemì")]
        [PropertyLocalization("en", "State")]
        [Property("State")]
        public string State { get; set; }

        [PropertyLocalization("cs", "WWW")]
        [PropertyLocalization("en", "WWW")]
        [Property("www")]
        public string Www { get; set; }

        [PropertyLocalization("cs", "Email")]
        [PropertyLocalization("en", "Email")]
        [Property("Email")]
        public string Email { get; set; }

        [PropertyLocalization("cs", "Telefon")]
        [PropertyLocalization("en", "Telephone")]
        [Property("Telephone")]
        public string Telephone { get; set; }

        [PropertyLocalization("cs", "Fax")]
        [PropertyLocalization("en", "Fax")]
        [Property("Fax")]
        public string Fax { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.firmName))
            {
                return this.firmName;
            }
            return base.ToString();
        }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Region", "Regiony")]
    [ClassLocalization("en", "Region", "Regions")]
    public class Region : BusinessObjectBase<Region>
    {
        private string name;

        public Region() {}

        public Region(string name)
        {
            this.name = name;
        }

        [PropertyLocalization("cs", "Název")]
        [PropertyLocalization("en", "Name")]
        [Property("Name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.name))
            {
                return this.name;
            }

            return base.ToString();
        }
    }


    [ActiveRecord, LocalizedPropertiesType(typeof (EstateOfferTypeCulture))]
    [ClassLocalization("cs", "Typ nabídky", "Typy nabídek")]
    [ClassLocalization("en", "Type of offer", "Types of offers")]
    public class EstateOfferType : BusinessObjectBase<EstateOfferType>
    {
        [HasMany(typeof (EstateOfferTypeCulture), Table = "EstateOfferTypeCulture", ColumnKey = "EstateOfferTypeId",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public override IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }


        public override string ToString()
        {
            foreach (EstateOfferTypeCulture item in this.LocalizedObjects)
            {
                if (item.Culture.Id == Thread.CurrentThread.CurrentCulture.LCID)
                {
                    return item.Name;
                }
            }
            return base.ToString();
        }
    }

    [ActiveRecord]
    public class EstateOfferTypeCulture : LocalizationObjectBase<EstateOfferTypeCulture>
    {
        public EstateOfferTypeCulture() {}

        public EstateOfferTypeCulture(Culture culture)
        {
            this.culture = culture;
        }

        [BelongsTo("EstateOfferTypeId")]
        public EstateOfferType EstateOfferType { get; set; }

        [PropertyLocalization("cs", "Název")]
        [PropertyLocalization("en", "Name")]
        [Property("Name")]
        public string Name { get; set; }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }
            return base.ToString();
        }
    }


    [ActiveRecord, LocalizedPropertiesType(typeof (EstateTypeCulture))]
    [ClassLocalization("cs", "Typ nemovitosti", "Typy nemovitostí")]
    [ClassLocalization("en", "Type of property", "Types of properties")]
    public class EstateType : BusinessObjectBase<EstateType>
    {
        [HasMany(typeof (EstateTypeCulture), Table = "EstateTypeCulture", ColumnKey = "EstateTypeId", Inverse = true,
            Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public override IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }


        public override string ToString()
        {
            foreach (EstateTypeCulture item in this.LocalizedObjects)
            {
                if (item.Culture.Id == Thread.CurrentThread.CurrentCulture.LCID)
                {
                    return item.Name;
                }
            }
            return base.ToString();
        }
    }

    [ActiveRecord]
    public class EstateTypeCulture : LocalizationObjectBase<EstateTypeCulture>
    {
        public EstateTypeCulture() {}

        public EstateTypeCulture(Culture culture)
        {
            this.culture = culture;
        }

        [BelongsTo("EstateTypeId")]
        public EstateType EstateType { get; set; }

        [PropertyLocalization("cs", "Název")]
        [PropertyLocalization("en", "Name")]
        [Property("Name")]
        public string Name { get; set; }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }
            return base.ToString();
        }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Typ mìny", "Typy mìn")]
    [ClassLocalization("en", "Type of currency", "Types of currencies")]
    public class CurrencyType : BusinessObjectBase<CurrencyType>
    {
        private string name;

        public CurrencyType() {}

        public CurrencyType(string name)
        {
            this.name = name;
        }

        [PropertyLocalization("cs", "Zkratka")]
        [PropertyLocalization("en", "Abbrev")]
        [Property("Name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.name))
            {
                return this.name;
            }

            return base.ToString();
        }
    }


    [ActiveRecord, LocalizedPropertiesType(typeof (PriceTypeCulture))]
    [ClassLocalization("cs", "Typ ceny", "Typy cen")]
    [ClassLocalization("en", "Type of price", "Type of prices")]
    public class PriceType : BusinessObjectBase<PriceType>
    {
        [HasMany(typeof (PriceTypeCulture), Table = "PriceTypeCulture", ColumnKey = "PriceTypeId", Inverse = true,
            Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public override IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }


        public override string ToString()
        {
            foreach (PriceTypeCulture item in this.LocalizedObjects)
            {
                if (item.Culture.Id == Thread.CurrentThread.CurrentCulture.LCID)
                {
                    return item.Name;
                }
            }
            return base.ToString();
        }
    }

    [ActiveRecord]
    public class PriceTypeCulture : LocalizationObjectBase<PriceTypeCulture>
    {
        public PriceTypeCulture() {}

        public PriceTypeCulture(Culture culture)
        {
            this.culture = culture;
        }

        [BelongsTo("PriceTypeId")]
        public PriceType PriceType { get; set; }

        [PropertyLocalization("cs", "Název")]
        [PropertyLocalization("en", "Name")]
        [Property("Name")]
        public string Name { get; set; }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }
            return base.ToString();
        }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Obrázek", "Obrázky")]
    [ClassLocalization("en", "Picture", "Pictures")]
    public class EstateImage : BusinessObjectBase<EstateImage>
    {
        private String filePath;

        public EstateImage()
        {
            this.IsMain = false;
        }

        public EstateImage(Estate estate, String filePath, String description_cs, String description_en)
            : this()
        {
            this.Estate = estate;
            this.filePath = filePath;
            this.Description_cs = description_cs;
            this.Description_en = description_en;
        }

        [PropertyLocalization("cs", "Cesta k souboru")]
        [PropertyLocalization("en", "File path")]
        [Property("FilePath")]
        public String FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Popis")]
        [Property("Description_cs")]
        public String Description_cs { get; set; }

        [PropertyLocalization("cs", "Description")]
        [PropertyLocalization("en", "Description")]
        [Property("Description_en")]
        public String Description_en { get; set; }

        [PropertyLocalization("cs", "Publikovat")]
        [PropertyLocalization("en", "Publish")]
        [Property("IsMain")]
        public bool IsMain { get; set; }

        [PropertyLocalization("cs", "Inzerát")]
        [PropertyLocalization("en", "Estate")]
        [BelongsTo("EstateId")]
        public Estate Estate { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.filePath))
            {
                return this.filePath;
            }

            return base.ToString();
        }

        public override void Delete()
        {
            DeleteFile();
            base.Delete();
        }

        public override void DeleteAndFlush()
        {
            DeleteFile();
            base.DeleteAndFlush();
        }

        public void DeleteFile()
        {
            Logger.EnterFunction(MethodBase.GetCurrentMethod());

            string path = this.FilePath;
            if (String.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(path);
                if (!File.Exists(fullPath))
                {
                    return;
                }

                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                Logger.Error(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Pøíloha", "Pøílohy")]
    [ClassLocalization("en", "Attachment", "Attachments")]
    public class EstateAttachment : BusinessObjectBase<EstateAttachment>
    {
        public EstateAttachment() {}

        public EstateAttachment(Estate estate, String filePath, long fileSize, String description_cs,
                                String description_en)
            : this()
        {
            this.Estate = estate;
            this.FilePath = filePath;
            this.FileSize = fileSize;
            this.Description_cs = description_cs;
            this.Description_en = description_en;
        }

        [PropertyLocalization("cs", "Cesta k souboru"), PropertyLocalization("en", "File path"), Property("FilePath")]
        public string FilePath { get; set; }

        [PropertyLocalization("cs", "Velikost souboru"), PropertyLocalization("en", "File size"), Property("FileSize")]
        public long FileSize { get; set; }

        [PropertyLocalization("cs", "Popis"), PropertyLocalization("en", "Popis"), Property("Description_cs")]
        public string Description_cs { get; set; }

        [PropertyLocalization("cs", "Description"), PropertyLocalization("en", "Description"), Property("Description_en")]
        public string Description_en { get; set; }

        [PropertyLocalization("cs", "Inzerát")]
        [PropertyLocalization("en", "Estate")]
        [BelongsTo("EstateId")]
        public Estate Estate { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.FilePath)
                && (!String.IsNullOrEmpty(this.Description_cs) || (!String.IsNullOrEmpty(this.Description_en))))
            {
                return "<a href=\"" + this.FilePath + "\" title=\""
                       +
                       (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs"
                            ? this.Description_cs
                            : this.Description_en) + "\">"
                       +
                       (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs"
                            ? this.Description_cs
                            : this.Description_en).Replace(" ", "&nbsp;") + "&nbsp;["
                       + Path.GetExtension(this.FilePath).Remove(0, 1).ToUpper() + ",&nbsp;" + (this.FileSize/1024)
                       + "KB]</a>";
            }
            else if (!String.IsNullOrEmpty(this.FilePath))
            {
                return "<a href=\"" + this.FilePath + "\" title=\"" + Path.GetFileName(this.FilePath) + "\">"
                       + Path.GetFileName(this.FilePath) + "</a>";
            }
            return base.ToString();
        }

        public override void Delete()
        {
            DeleteFile();
            base.Delete();
        }

        public override void DeleteAndFlush()
        {
            DeleteFile();
            base.DeleteAndFlush();
        }

        public void DeleteFile()
        {
            Logger.EnterFunction(MethodBase.GetCurrentMethod());

            string path = this.FilePath;
            if (String.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(path);
                if (!File.Exists(fullPath))
                {
                    return;
                }

                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                Logger.Error(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}