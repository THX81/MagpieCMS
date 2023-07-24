using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord, LocalizedPropertiesType(typeof (EstateCulture))]
    [ClassLocalization("cs", "Nemovitost", "Nemovitosti")]
    [ClassLocalization("en", "Property", "Properties")]
    public class Estate : BusinessObjectBase<Estate>
    {
        private IList<EstateAttachment> estateAttachments = new List<EstateAttachment>();
        private IList<EstateImage> estateImages = new List<EstateImage>();

        public Estate()
        {
            created = DateTime.Now;
        }

        [PrimaryKey(PrimaryKeyType.Native, "EstateId")]
        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        [HasMany(typeof (EstateCulture), Table = "EstateCulture", ColumnKey = "EstateId", Inverse = true,
            Cascade = ManyRelationCascadeEnum.Delete)]
        public virtual IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }

        [BelongsTo("EstateTypeId")]
        public virtual EstateType EstateType { get; set; }

        [PropertyLocalization("cs", "Developerský projekt")]
        [PropertyLocalization("en", "Developer project")]
        [BelongsTo("DeveloperProjectId")]
        public DeveloperProject DeveloperProject { get; set; }

        [BelongsTo("EstateOfferTypeId")]
        public virtual EstateOfferType EstateOfferType { get; set; }

        [PropertyLocalization("cs", "Doporuèujeme")]
        [PropertyLocalization("en", "We recommend")]
        [Property("HotTip")]
        public virtual bool HotTip { get; set; }

        [PropertyLocalization("cs", "Publikovat")]
        [PropertyLocalization("en", "Publish")]
        [Property("Publish")]
        public virtual bool Publish { get; set; }

        [PropertyLocalization("cs", "Prodáno")]
        [PropertyLocalization("en", "Saled")]
        [Property("Saled")]
        public virtual bool Saled { get; set; }

        [PropertyLocalization("cs", "Pronajato")]
        [PropertyLocalization("en", "Rented")]
        [Property("Rented")]
        public virtual bool Rented { get; set; }

        [PropertyLocalization("cs", "Rezervováno")]
        [PropertyLocalization("en", "Reservated")]
        [Property("Reserved")]
        public virtual bool Reserved { get; set; }

        [PropertyLocalization("cs", "Exkluzivita")]
        [PropertyLocalization("en", "Exclusiveness")]
        [Property("Exclusivity")]
        public virtual bool Exclusivity { get; set; }

        [PropertyLocalization("cs", "Zahranièní nemovitost")]
        [PropertyLocalization("en", "Foreign property")]
        [Property("ForeignProperty")]
        public virtual bool ForeignProperty { get; set; }

        [OneToOne(Cascade = CascadeEnum.Delete)]
        public virtual EstateProperties EstateProperties { get; set; }

        [PropertyLocalization("cs", "Adresa nemovitosti")]
        [PropertyLocalization("en", "Estate address")]
        [Nested(ColumnPrefix = "EstateAddressInfo")]
        public virtual EstateAddressInfo EstateAddressInfo { get; set; }

        [PropertyLocalization("cs", "Cena nemovitosti")]
        [PropertyLocalization("en", "Estate price")]
        [Nested(ColumnPrefix = "EstatePriceInfo")]
        public virtual EstatePriceInfo EstatePriceInfo { get; set; }

        [BelongsTo("EstateManCardId")]
        public virtual EstateManCard EstateManCard { get; set; }

        [PropertyLocalization("cs", "Rozšiøující informace")]
        [PropertyLocalization("en", "Additional informations")]
        [Nested(ColumnPrefix = "EstateExtendedInfo")]
        public virtual EstateExtendedInfo EstateExtendedInfo { get; set; }

        [PropertyLocalization("cs", "Fotky")]
        [PropertyLocalization("en", "Photos")]
        [HasMany(
            Table = "EstateImage", ColumnKey = "EstateId", OrderBy = "IsMain DESC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<EstateImage> EstateImages
        {
            get { return this.estateImages; }
            set { this.estateImages = value; }
        }

        [PropertyLocalization("cs", "Pøílohy")]
        [PropertyLocalization("en", "Attachments")]
        [HasMany(
            Table = "EstateAttachment", ColumnKey = "EstateId", OrderBy = "Description_cs ASC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<EstateAttachment> EstateAttachments
        {
            get { return this.estateAttachments; }
            set { this.estateAttachments = value; }
        }

        public override string ToString()
        {
            foreach (EstateCulture item in this.LocalizedObjects)
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
            foreach (EstateImage image in this.EstateImages)
            {
                return ("/estateimage.axd?id=" + image.Id);
            }

            return String.Empty;
        }

        public List<ImageInfo> GetAllPhotosLinksForWeb()
        {
            var result = new List<ImageInfo>();

            foreach (EstateImage image in this.EstateImages)
            {
                var img = new ImageInfo
                          {
                              Url = ("/estateimage.axd?id=" + image.Id),
                              Description = (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs"
                                                 ? image.Description_cs
                                                 : image.Description_en)
                          };
                result.Add(img);
            }

            return result;
        }

        /// <summary>
        /// URL se získává dle nastavení šablony stránek, pokud je nalezena šablona tak se najdou stránky vytvoøené dle této šablony. Vyfiltrují se nakonec dle aktuálního jazyka webu.
        /// Lze tak libovolnì pøejmenovávat název stránky v URL.
        /// </summary>
        /// <returns></returns>
        public string GetUrl(int cultureId)
        {
            string url = "";
            int estateOfferTypeId = this.EstateOfferType.Id;
            int estateTypeId = this.EstateType.Id;

            string sqlPages = "from Page p where p.PageTemplate.EstateOfferType.Id = " + estateOfferTypeId + " and p.PageTemplate.EstateType.Id = " + estateTypeId + " and p.Published = 1";
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

        public virtual EstateCulture GetLocalizedObject()
        {
            foreach (EstateCulture item in this.LocalizedObjects)
            {
                if (Thread.CurrentThread.CurrentCulture.LCID == item.Culture.Id)
                {
                    return item;
                }
            }
            return this.LocalizedObjects[0] as EstateCulture;
        }

        public override void Delete()
        {
            DeleteImagesAndAttachments();
            base.Delete();
        }

        public override void DeleteAndFlush()
        {
            DeleteImagesAndAttachments();
            base.DeleteAndFlush();
        }

        private void DeleteImagesAndAttachments()
        {
            Logger.EnterFunction(MethodBase.GetCurrentMethod());

            foreach (EstateImage estateImage in this.EstateImages)
            {
                estateImage.Delete();
            }

            foreach (EstateAttachment estateAttachment in this.EstateAttachments)
            {
                estateAttachment.Delete();
            }
        }

        public static int TotalCount()
        {
            return Count(typeof (Estate));
        }
    }

    [ActiveRecord]
    public class EstateCulture : LocalizationObjectBase<EstateCulture>
    {
        [BelongsTo("EstateId")]
        public Estate Estate { get; set; }

        [PropertyLocalization("cs", "Název")]
        [PropertyLocalization("en", "Name")]
        [Property("Name")]
        public string Name { get; set; }

        [PropertyLocalization("cs", "Základní popis")]
        [PropertyLocalization("en", "Basic description")]
        [Property("BasicDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        public string BasicDescription { get; set; }

        [PropertyLocalization("cs", "Hlavní popis")]
        [PropertyLocalization("en", "Full description")]
        [Property("FullDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        public string FullDescription { get; set; }

        [PropertyLocalization("cs", "Ostatní text")]
        [PropertyLocalization("en", "Additional description")]
        [Property("AdditionalDescription", ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        public string AdditionalDescription { get; set; }

        [PropertyLocalization("cs", "Poznámka k cenì")]
        [PropertyLocalization("en", "Price comment")]
        [Property("PriceComment")]
        public string PriceComment { get; set; }

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

    public class ImageInfo
    {
        public string Url { get; set; }

        public string Description { get; set; }
    }


    [ActiveRecord, LocalizedPropertiesType(typeof (EstatePropertiesCulture))]
    [ClassLocalization("cs", "Specifické informace", "Specifické informace")]
    [ClassLocalization("en", "Specific information", "Specific informations")]
    public class EstateProperties : BusinessObjectBase<EstateProperties>
    {
        [PrimaryKey(PrimaryKeyType.Foreign, "EstateId")]
        public new int Id
        {
            get { return id; }
            set { id = value; }
        }

        [OneToOne]
        public Estate Estate { get; set; }

        [HasMany(typeof (EstatePropertiesCulture), Table = "EstatePropertiesCulture", ColumnKey = "EstateId",
            Inverse = true, Cascade = ManyRelationCascadeEnum.Delete)]
        public new IList LocalizedObjects
        {
            get { return localizedObjects; }
            set { localizedObjects = value; }
        }

        [PropertyLocalization("cs", "Dispozice")]
        [PropertyLocalization("en", "Disposition")]
        [Property("Disposition")]
        public string Disposition { get; set; }

        [PropertyLocalization("cs", "Velikost jednotky")]
        [PropertyLocalization("en", "Size of unit")]
        [Property("FlatSize")]
        public string FlatSize { get; set; }

        [PropertyLocalization("cs", "Podlaží")]
        [PropertyLocalization("en", "Storey")]
        [Property("Floor")]
        public string Floor { get; set; }

        [PropertyLocalization("cs", "Poèet podlaží objektu")]
        [PropertyLocalization("en", "Number od storeys")]
        [Property("TotalFloors")]
        public string TotalFloors { get; set; }

        [PropertyLocalization("cs", "Podlahová plocha")]
        [PropertyLocalization("en", "Floor area")]
        [Property("FloorSize")]
        public string FloorSize { get; set; }

        [PropertyLocalization("cs", "Poèet bytù")]
        [PropertyLocalization("en", "Number of apartments")]
        [Property("FlatsTotal")]
        public string FlatsTotal { get; set; }

        [PropertyLocalization("cs", "Typy bytù")]
        [PropertyLocalization("en", "Types of apartments")]
        [Property("FlatsTypes")]
        public string FlatsTypes { get; set; }

        [PropertyLocalization("cs", "Celková plocha bytových jednotek")]
        [PropertyLocalization("en", "Total apartment area")]
        [Property("TotalFlatsSize")]
        public string TotalFlatsSize { get; set; }

        [PropertyLocalization("cs", "Rok rekonstrukce")]
        [PropertyLocalization("en", "Year of reconstruction")]
        [Property("ReconstructionYear")]
        public string ReconstructionYear { get; set; }

        [PropertyLocalization("cs", "Rok výstavby")]
        [PropertyLocalization("en", "Year of construction")]
        [Property("BuildYear")]
        public string BuildYear { get; set; }

        [PropertyLocalization("cs", "Rok kolaudace")]
        [PropertyLocalization("en", "Year of final building approval")]
        [Property("ColaudationYear")]
        public string ColaudationYear { get; set; }

        [PropertyLocalization("cs", "K nastìhování")]
        [PropertyLocalization("en", "Ready to move in")]
        [Property("ForMovement")]
        public string ForMovement { get; set; }

        [PropertyLocalization("cs", "Zastavìná plocha")]
        [PropertyLocalization("en", "Built-up area")]
        [Property("BuildedPlaceSize")]
        public string BuildedPlaceSize { get; set; }

        [PropertyLocalization("cs", "Užitná plocha")]
        [PropertyLocalization("en", "Utility area")]
        [Property("UseablePlaceSize")]
        public string UseablePlaceSize { get; set; }

        [PropertyLocalization("cs", "Plocha parcely")]
        [PropertyLocalization("en", "Parcel area")]
        [Property("PropertySize")]
        public string PropertySize { get; set; }

        [PropertyLocalization("cs", "Plocha teras")]
        [PropertyLocalization("en", "Terrace area")]
        [Property("TeracesSize")]
        public string TeracesSize { get; set; }

        [PropertyLocalization("cs", "Plocha zahrady")]
        [PropertyLocalization("en", "Garden area")]
        [Property("GardenSize")]
        public string GardenSize { get; set; }

        [PropertyLocalization("cs", "Plocha sklepa")]
        [PropertyLocalization("en", "Cellar area")]
        [Property("UndergroundSize")]
        public string UndergroundSize { get; set; }

        [PropertyLocalization("cs", "Plocha")]
        [PropertyLocalization("en", "Area")]
        [Property("PlaceSize")]
        public string PlaceSize { get; set; }


        public EstatePropertiesCulture GetLocalizedObject()
        {
            foreach (EstatePropertiesCulture item in this.LocalizedObjects)
            {
                if (Thread.CurrentThread.CurrentCulture.LCID == item.Culture.Id)
                {
                    return item;
                }
            }
            if (this.LocalizedObjects.Count > 0)
            {
                return this.LocalizedObjects[0] as EstatePropertiesCulture;
            }
            return null; // new EstatePropertiesCulture();
        }
    }

    [ActiveRecord]
    public class EstatePropertiesCulture : LocalizationObjectBase<EstatePropertiesCulture>
    {
        [BelongsTo("EstateId")]
        public EstateProperties EstateProperties { get; set; }

        [PropertyLocalization("cs", "Typ objektu")]
        [PropertyLocalization("en", "Type of object")]
        [Property("ObjectType")]
        public string ObjectType { get; set; }

        [PropertyLocalization("cs", "Typ budovy")]
        [PropertyLocalization("en", "Type of building")]
        [Property("BuildingType")]
        public string BuildingType { get; set; }

        [PropertyLocalization("cs", "Stav budovy")]
        [PropertyLocalization("en", "Condition of buildning")]
        [Property("BuildingState")]
        public string BuildingState { get; set; }

        [PropertyLocalization("cs", "Druh bytu")]
        [PropertyLocalization("en", "Type of apartment")]
        [Property("EstateType")]
        public string EstateType { get; set; }

        [PropertyLocalization("cs", "Nebytové prostory")]
        [PropertyLocalization("en", "Non-residential premises")]
        [Property("NoLiveArea")]
        public string NoLiveArea { get; set; }

        [PropertyLocalization("cs", "Konstrukèní prvky")]
        [PropertyLocalization("en", "Constructional elements")]
        [Property("Constructions")]
        public string Constructions { get; set; }

        [PropertyLocalization("cs", "Vlastnictví")]
        [PropertyLocalization("en", "Ownership")]
        [Property("OwnerShip")]
        public string OwnerShip { get; set; }

        [PropertyLocalization("cs", "Vybavenost bytu")]
        [PropertyLocalization("en", "Apartment ratio")]
        [Property("Properties")]
        public string Properties { get; set; }

        [PropertyLocalization("cs", "Okolí")]
        [PropertyLocalization("en", "Neighbourhood")]
        [Property("LocalityInfo")]
        public string LocalityInfo { get; set; }

        [PropertyLocalization("cs", "Zeleò")]
        [PropertyLocalization("en", "Greenery")]
        [Property("Green")]
        public string Green { get; set; }

        [PropertyLocalization("cs", "Sítì")]
        [PropertyLocalization("en", "Network")]
        [Property("Sites")]
        public string Sites { get; set; }

        [PropertyLocalization("cs", "Komunikace")]
        [PropertyLocalization("en", "Traffic accessability")]
        [Property("Comunication")]
        public string Comunication { get; set; }

        [PropertyLocalization("cs", "Telekomunikace")]
        [PropertyLocalization("en", "Telecommunications")]
        [Property("Telecomunication")]
        public string Telecomunication { get; set; }

        [PropertyLocalization("cs", "Elektøina")]
        [PropertyLocalization("en", "Eletricity")]
        [Property("Electricity")]
        public string Electricity { get; set; }

        [PropertyLocalization("cs", "Voda")]
        [PropertyLocalization("en", "Water")]
        [Property("Water")]
        public string Water { get; set; }

        [PropertyLocalization("cs", "Odpad")]
        [PropertyLocalization("en", "Waste")]
        [Property("Channel")]
        public string Channel { get; set; }

        [PropertyLocalization("cs", "Topení")]
        [PropertyLocalization("en", "Heating")]
        [Property("Heating")]
        public string Heating { get; set; }

        [PropertyLocalization("cs", "Plyn")]
        [PropertyLocalization("en", "Gas")]
        [Property("Gas")]
        public string Gas { get; set; }

        [PropertyLocalization("cs", "Doprava")]
        [PropertyLocalization("en", "Transportation")]
        [Property("Trafic")]
        public string Trafic { get; set; }

        [PropertyLocalization("cs", "Vybavenost")]
        [PropertyLocalization("en", "Facilities")]
        [Property("Facilities")]
        public string Facilities { get; set; }

        [PropertyLocalization("cs", "Typ zaøízení")]
        [PropertyLocalization("en", "Device type")]
        [Property("UseType")]
        public string UseType { get; set; }

        [PropertyLocalization("cs", "Sociální zaøízení")]
        [PropertyLocalization("en", "Social settlement")]
        [Property("SocialMachine")]
        public string SocialMachine { get; set; }

        [PropertyLocalization("cs", "Schody")]
        [PropertyLocalization("en", "Stairs")]
        [Property("Stairs")]
        public string Stairs { get; set; }

        [PropertyLocalization("cs", "Ostatní")]
        [PropertyLocalization("en", "Others")]
        [Property("OtherInfo")]
        public string OtherInfo { get; set; }


        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.BuildingType))
            {
                return this.BuildingType;
            }
            return base.ToString();
        }
    }
}