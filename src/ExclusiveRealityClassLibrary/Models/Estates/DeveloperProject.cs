using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord, LocalizedPropertiesType(typeof (DeveloperProjectCulture))]
    [ClassLocalization("cs", "Developerský projekt", "Developerské projekty")]
    public class DeveloperProject : BusinessObjectBase<DeveloperProject>
    {
        private IList<Estate> estates = new List<Estate>();
        private IList<DeveloperProjectAttachment> developerProjectAttachments = new List<DeveloperProjectAttachment>();
        private IList<DeveloperProjectImage> developerProjectImages = new List<DeveloperProjectImage>();
        
        public DeveloperProject()
        {
            created = DateTime.Now;
        }

        [PrimaryKey(PrimaryKeyType.Native, "DeveloperProjectId")]
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        [HasMany(typeof (DeveloperProjectCulture), Table = "DeveloperProjectCulture", ColumnKey = "DeveloperProjectId",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public virtual IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }

        [PropertyLocalization("cs", "Èíslo zakázky")]
        [PropertyLocalization("en", "Order number")]
        [Property("OrderNumber")]
        public string OrderNumber { get; set; }

        [PropertyLocalization("cs", "Doporuèujeme")]
        [PropertyLocalization("en", "We recommend")]
        [Property("HotTip")]
        public bool HotTip { get; set; }

        [PropertyLocalization("cs", "Zahranièní developerský projekt")]
        [PropertyLocalization("en", "Foreign developer project")]
        [Property("ForeignProject")]
        public bool ForeignProject { get; set; }

        [PropertyLocalization("cs", "Publikovat")]
        [PropertyLocalization("en", "Publish")]
        [Property("Publish")]
        public bool Publish { get; set; }

        [PropertyLocalization("cs", "Základní údaje o projektu")]
        [PropertyLocalization("en", "Basic project info")]
        [Nested(ColumnPrefix = "EstateAddressInfo")]
        public EstateAddressInfo EstateAddressInfo { get; set; }

        [BelongsTo("EstateManCardId")]
        public EstateManCard EstateManCard { get; set; }

        [BelongsTo("DeveloperId")]
        public CompanyInfo Developer { get; set; }

        [BelongsTo("InvestorId")]
        public CompanyInfo Investor { get; set; }

        [PropertyLocalization("cs", "Další informace")]
        [PropertyLocalization("en", "Next info")]
        [Nested(ColumnPrefix = "ProjectNextInfo")]
        public ProjectNextInfo ProjectNextInfo { get; set; }

        [PropertyLocalization("cs", "Ostatní informace")]
        [PropertyLocalization("en", "Other info")]
        [Nested(ColumnPrefix = "AdditionalProjectObjectsInfo")]
        public AdditionalProjectObjectsInfo AdditionalProjectObjectsInfo { get; set; }

        [Nested(ColumnPrefix = "BuildingConstructionInfo")]
        public BuildingConstructionInfo BuildingConstructionInfo { get; set; }

        [Nested(ColumnPrefix = "TelecomunicationInfo")]
        public TelecomunicationInfo TelecomunicationInfo { get; set; }

        [Nested(ColumnPrefix = "IngSitesInfo")]
        public IngSitesInfo IngSitesInfo { get; set; }

        [Nested(ColumnPrefix = "ElectricityInfo")]
        public ElectricityInfo ElectricityInfo { get; set; }

        [Nested(ColumnPrefix = "FacilitiesInfo")]
        public FacilitiesInfo FacilitiesInfo { get; set; }

        [PropertyLocalization("cs", "Nemovitosti")]
        [PropertyLocalization("en", "Estates")]
        [HasMany(
            Table = "Estate", ColumnKey = "DeveloperProjectId", OrderBy = "Created DESC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.Delete)]
        public IList<Estate> Estates
        {
            get { return this.estates; }
            set { this.estates = value; }
        }

        [PropertyLocalization("cs", "Fotky")]
        [PropertyLocalization("en", "Photos")]
        [HasMany(
            Table = "DeveloperProjectImage", ColumnKey = "DeveloperProjectId", OrderBy = "IsMain DESC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<DeveloperProjectImage> DeveloperProjectImages
        {
            get { return this.developerProjectImages; }
            set { this.developerProjectImages = value; }
        }

        [PropertyLocalization("cs", "Pøílohy")]
        [PropertyLocalization("en", "Attachments")]
        [HasMany(
            Table = "DeveloperProjectAttachment", ColumnKey = "DeveloperProjectId", OrderBy = "Description_cs ASC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<DeveloperProjectAttachment> DeveloperProjectAttachments
        {
            get { return this.developerProjectAttachments; }
            set { this.developerProjectAttachments = value; }
        }

        public override string ToString()
        {
            foreach (DeveloperProjectCulture item in this.LocalizedObjects)
            {
                if (item.Culture.Id == Thread.CurrentThread.CurrentCulture.LCID)
                {
                    return item.Name;
                }
            }
            return base.ToString();
        }

        public virtual String GetFirstPhotoLinkForWeb()
        {
            foreach (DeveloperProjectImage image in this.DeveloperProjectImages)
            {
                return ("/estateimage.axd?id=" + image.Id + "&amp;source=developerproject");
            }

            return String.Empty;
        }

        public List<ImageInfo> GetAllPhotosLinksForWeb()
        {
            var result = new List<ImageInfo>();

            foreach (DeveloperProjectImage image in this.DeveloperProjectImages)
            {
                var img = new ImageInfo
                {
                    Url = ("/estateimage.axd?id=" + image.Id + "&amp;source=developerproject"),
                    Description = (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs" ? image.Description_cs : image.Description_en)
                };
                result.Add(img);
            }

            return result;
        }

        public object GetMinMaxPrice()
        {
            int minPrice = 999999999;
            int maxPrice = 0;

            foreach (Estate estate in estates)
            {
                if (estate.EstatePriceInfo.PriceValue < minPrice)
                    minPrice = estate.EstatePriceInfo.PriceValue;

                if (estate.EstatePriceInfo.PriceValue > maxPrice)
                    maxPrice = estate.EstatePriceInfo.PriceValue;
            }

            if (minPrice == 999999999)
                minPrice = 0;

            return new { MinPrice = minPrice, MaxPrice = maxPrice };
        }

        /// <summary>
        /// URL se získává dle nastavení šablony stránek, pokud je nalezena šablona tak se najdou stránky vytvoøené dle této šablony. Vyfiltrují se nakonec dle aktuálního jazyka webu.
        /// Lze tak libovolnì pøejmenovávat název stránky v URL.
        /// </summary>
        /// <returns></returns>
        public string GetUrl(int cultureId)
        {
            string url = "";
            //string pageTemplateName = "developerprojectspage";
            //string pageTemplateForeignName = "foreigndeveloperprojectspage";

            //string sqlPages = "from Page p where (p.PageTemplate.Name = '" + pageTemplateName + "' or p.PageTemplate.Name = '" + pageTemplateForeignName + "') and p.PageTemplate.ForeignProject = " + (this.ForeignProject ? 1 : 0) + " and (p.Section.Name like 'developer%') and p.Published = 1";
            string sqlPages = "from Page p where p.PageTemplate.ForeignProject = " + (this.ForeignProject ? 1 : 0) + " and (p.Section.Name like 'developer%') and p.Published = 1";
            Page[] pages = new SimpleQuery<Page>(typeof(Page), sqlPages).Execute();

            if (pages.Length > 0)
            {
                Page page = null;
                foreach (Page p in pages)
                {
                    if (p.GetEnsuredCulture(false).Id == cultureId)
                    {
                        page = p;
                        break;
                    }
                }

                if (page != null)
                {
                    url = page.GetUrl(false);
                }
            }

            url += "?detail=" + this.Id;

            return url;
        }

        public virtual DeveloperProjectCulture GetLocalizedObject()
        {
            foreach (DeveloperProjectCulture item in this.LocalizedObjects)
            {
                if (Thread.CurrentThread.CurrentCulture.LCID == item.Culture.Id)
                {
                    return item;
                }
            }
            return this.LocalizedObjects[0] as DeveloperProjectCulture;
        }

        public static int TotalCount()
        {
            return Count(typeof(DeveloperProject));
        }
    }

    [ActiveRecord]
    public class DeveloperProjectCulture : LocalizationObjectBase<DeveloperProjectCulture>
    {
        [BelongsTo("DeveloperProjectId")]
        public DeveloperProject DeveloperProject { get; set; }

        [PropertyLocalization("cs", "Název")]
        [PropertyLocalization("en", "Name")]
        [Property("Name")]
        public string Name { get; set; }

        [PropertyLocalization("cs", "Popis lokality")]
        [PropertyLocalization("en", "Lokality description")]
        [Property("LocalityDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        public string LocalityDescription { get; set; }

        [PropertyLocalization("cs", "Základní popis")]
        [PropertyLocalization("en", "Basic description")]
        [Property("BasicDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        public string BasicDescription { get; set; }

        [PropertyLocalization("cs", "Kompletní popis projektu")]
        [PropertyLocalization("en", "Complete project description")]
        [Property("FullDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        public string FullDescription { get; set; }

        [PropertyLocalization("cs", "Poèet a popis etap projektu")]
        [PropertyLocalization("en", "Project periods")]
        [Property("ProjectsEtapsDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        public string ProjectsEtapsDescription { get; set; }

        [PropertyLocalization("cs", "Parkování")]
        [PropertyLocalization("en", "Parking")]
        [Property("Parking")]
        public string Parking { get; set; }

        [PropertyLocalization("cs", "URL stránky detailu")]
        [PropertyLocalization("en", "Detail page URL")]
        [Property("DetailPageUrl")]
        [FormGeneratorBehaviorAttribute(FormControlType.Hidden)]
        public string DetailPageUrl { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.Name))
            {
                return this.Name;
            }
            return base.ToString();
        }
    }


    public class ProjectNextInfo
    {
        [PropertyLocalization("cs", "K nastìhování")]
        [PropertyLocalization("en", "For move")]
        [Property("ForMove")]
        public string ForMove { get; set; }

        [PropertyLocalization("cs", "Datum zahájení stavby")]
        [PropertyLocalization("en", "Date of construction start")]
        [Property("BuildStartDate")]
        public string BuildStartDate { get; set; }

        [PropertyLocalization("cs", "Datum ukonèení stavby")]
        [PropertyLocalization("en", "Date of construction end")]
        [Property("BuildEndDate")]
        public string BuildEndDate { get; set; }

        [PropertyLocalization("cs", "Datum zahájení prodeje")]
        [PropertyLocalization("en", "Date of sale start")]
        [Property("SaleStartDate")]
        public string SaleStartDate { get; set; }

        [PropertyLocalization("cs", "Základní vklad (%)")]
        [PropertyLocalization("en", "Basic deposit (%)")]
        [Property("BaseInvestPercents")]
        public int BaseInvestPercents { get; set; }

        [PropertyLocalization("cs", "Stavební spoøení (%)")]
        [PropertyLocalization("en", "Building saving (%)")]
        [Property("BuildingSavingPercents")]
        public int BuildingSavingPercents { get; set; }

        [PropertyLocalization("cs", "Hypotéka (%)")]
        [PropertyLocalization("en", "Encumbrance (%)")]
        [Property("HypoPercents")]
        public int HypoPercents { get; set; }

        [PropertyLocalization("cs", "Hypotéèní ústav")]
        [PropertyLocalization("en", "Encumbrancer")]
        [Property("HypoCompany")]
        public string HypoCompany { get; set; }

        [PropertyLocalization("cs", "Možnost pøevedení do os. vlastnictví")]
        [PropertyLocalization("en", "Can by converted to personal ownership")]
        [Property("PersonalRight")]
        public bool PersonalRight { get; set; }
    }


    [ClassLocalization("cs", "Obrázek", "Obrázky")]
    [ClassLocalization("en", "Picture", "Pictures")]
    public class AdditionalProjectObjectsInfo
    {
        [PropertyLocalization("cs", "Ostatní plochy")]
        [PropertyLocalization("en", "Other areas")]
        [Property("OtherAreas", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        public string OtherAreas { get; set; }

        [PropertyLocalization("cs", "Garáže")]
        [PropertyLocalization("en", "Garages")]
        [Property("Garages")]
        public bool Garages { get; set; }

        [PropertyLocalization("cs", "Poèet parkovacích míst")]
        [PropertyLocalization("en", "Parking count")]
        [Property("ParkingCount")]
        public int ParkingCount { get; set; }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Obrázek", "Obrázky")]
    [ClassLocalization("en", "Picture", "Pictures")]
    public class DeveloperProjectImage : BusinessObjectBase<DeveloperProjectImage>
    {
        public DeveloperProjectImage()
        {
            this.IsMain = false;
        }

        public DeveloperProjectImage(DeveloperProject developerProject, String filePath, String description_cs, String description_en)
            : this()
        {
            this.DeveloperProject = developerProject;
            this.FilePath = filePath;
            this.Description_cs = description_cs;
            this.Description_en = description_en;
        }

        [PropertyLocalization("cs", "Cesta k souboru")]
        [PropertyLocalization("en", "File path")]
        [Property("FilePath")]
        public string FilePath { get; set; }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Description")]
        [Property("Description_cs")]
        public String Description_cs { get; set; }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Description")]
        [Property("Description_en")]
        public String Description_en { get; set; }

        [PropertyLocalization("cs", "Publikovat")]
        [PropertyLocalization("en", "Publish")]
        [Property("IsMain")]
        public bool IsMain { get; set; }

        [PropertyLocalization("cs", "Developerský projekt")]
        [PropertyLocalization("en", "Developer project")]
        [BelongsTo("DeveloperProjectId")]
        public DeveloperProject DeveloperProject { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.FilePath))
            {
                return this.FilePath;
            }

            return base.ToString();
        }

        public void DeleteFile()
        {
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
                HttpContext.Current.Response.Write(ex);
            }
        }
    }


    [ActiveRecord]
    [ClassLocalization("cs", "Pøíloha", "Pøílohy")]
    [ClassLocalization("en", "Attachment", "Attachments")]
    public class DeveloperProjectAttachment : BusinessObjectBase<DeveloperProjectAttachment>
    {
        public DeveloperProjectAttachment() { }

        public DeveloperProjectAttachment(DeveloperProject developerProject, String filePath, long fileSize, String description_cs, String description_en)
            : this()
        {
            this.DeveloperProject = developerProject;
            this.FilePath = filePath;
            this.FileSize = fileSize;
            this.Description_cs = description_cs;
            this.Description_en = description_en;
        }

        [PropertyLocalization("cs", "Cesta k souboru")]
        [PropertyLocalization("en", "File path")]
        [Property("FilePath")]
        public string FilePath { get; set; }

        [PropertyLocalization("cs", "Velikost souboru")]
        [PropertyLocalization("en", "File size")]
        [Property("FileSize")]
        public long FileSize { get; set; }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Description")]
        [Property("Description_cs")]
        public string Description_cs { get; set; }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Description")]
        [Property("Description_en")]
        public string Description_en { get; set; }

        [PropertyLocalization("cs", "Developerský projekt")]
        [PropertyLocalization("en", "Developer project")]
        [BelongsTo("DeveloperProjectId")]
        public DeveloperProject DeveloperProject { get; set; }


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
                       + Path.GetExtension(this.FilePath).Remove(0, 1).ToUpper() + ",&nbsp;" + (this.FileSize / 1024)
                       + "KB]</a>";
            }
            else if (!String.IsNullOrEmpty(this.FilePath))
            {
                return "<a href=\"" + this.FilePath + "\" title=\"" + Path.GetFileName(this.FilePath) + "\">"
                       + Path.GetFileName(this.FilePath) + "</a>";
            }
            return base.ToString();
        }

        public void DeleteFile()
        {
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
                HttpContext.Current.Response.Write(ex);
            }
        }
    }

}