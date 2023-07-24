using System;
using System.Collections.Specialized;
using System.Reflection;
using Castle.ActiveRecord;
using ExclusiveReality.Models.Attributes;

namespace ExclusiveReality.Models
{
    [ClassLocalization("cs", "Cena nemovitosti", "Ceny nemovitostí")]
    [ClassLocalization("en", "Price of property", "Prices of properties")]
    public class EstatePriceInfo
    {
        [PropertyLocalization("cs", "Èástka")]
        [PropertyLocalization("en", "Amount")]
        [Property("PriceValue")]
        public int PriceValue { get; set; }

        [PropertyLocalization("cs", "Typ mìny")]
        [PropertyLocalization("en", "Type of currency")]
        [BelongsTo("CurrencyTypeId")]
        public CurrencyType CurrencyType { get; set; }

        [BelongsTo("PriceTypeId")]
        public PriceType PriceType { get; set; }

        [PropertyLocalization("cs", "Prodej z konkurzní podstaty")]
        [PropertyLocalization("en", "Prodej z konkurzní podstaty")]
        [Property("BankruptcyEstateSale")]
        public bool BankruptcyEstateSale { get; set; }
    }

    [ClassLocalization("cs", "Adresa", "Adresy")]
    [ClassLocalization("en", "Address", "Addresses")]
    public class EstateAddressInfo
    {
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

        [BelongsTo("RegionId")]
        public Region Region { get; set; }
    }

    [ClassLocalization("cs", "Konstrukèní prvky", "Konstrukèní prvky")]
    [ClassLocalization("en", "Constructional elements", "Constructional elements")]
    public class BuildingConstructionInfo
    {
        [Property, PropertyLocalization("cs", "Základy")]
        [PropertyLocalization("en", "Staples")]
        public string Platform { get; set; }

        [Property, PropertyLocalization("cs", "Stropy")]
        [PropertyLocalization("en", "Ceiling")]
        public string Roofliner { get; set; }

        [Property, PropertyLocalization("cs", "Støecha")]
        [PropertyLocalization("en", "Roof")]
        public string Roof { get; set; }

        [Property, PropertyLocalization("cs", "Krytina")]
        [PropertyLocalization("en", "Roof covering")]
        public string Covering { get; set; }

        [Property, PropertyLocalization("cs", "Vnitøní omítky")]
        [PropertyLocalization("en", "Vnitøní omítky")]
        public string InnerPlaster { get; set; }

        [Property, PropertyLocalization("cs", "Fasádní omítky")]
        [PropertyLocalization("en", "Fasádní omítky")]
        public string OuterPlaster { get; set; }

        [Property, PropertyLocalization("cs", "Vnitøní obklady")]
        [PropertyLocalization("en", "Vnitøní obklady")]
        public string InnerCoat { get; set; }

        [Property, PropertyLocalization("cs", "Vnìjší obklady")]
        [PropertyLocalization("en", "Vnìjší obklady")]
        public string OuterCoat { get; set; }

        [Property, PropertyLocalization("cs", "Klempíøská konst.")]
        [PropertyLocalization("en", "Plumbing constructions")]
        public string PlumbConstruction { get; set; }

        [Property, PropertyLocalization("cs", "Podlahy")]
        [PropertyLocalization("en", "Floors")]
        public string Floors { get; set; }

        [Property, PropertyLocalization("cs", "Schody")]
        [PropertyLocalization("en", "Stairs")]
        public string Stairs { get; set; }

        [Property, PropertyLocalization("cs", "Dveøe")]
        [PropertyLocalization("en", "Doors")]
        public string Doors { get; set; }

        [Property, PropertyLocalization("cs", "Okna")]
        [PropertyLocalization("en", "Windows")]
        public string Windows { get; set; }

        [Property, PropertyLocalization("cs", "Kuchyòská linka v cenì")]
        [PropertyLocalization("en", "Kitchen included")]
        public bool Kitchen { get; set; }
    }

    [ClassLocalization("cs", "Telekomunikace", "Telekomunikace")]
    [ClassLocalization("en", "Telecommunications", "Telecommunications")]
    public class TelecomunicationInfo
    {
        [Property, PropertyLocalization("cs", "Telefon")]
        [PropertyLocalization("en", "Phone")]
        public bool Telephone { get; set; }

        [Property, PropertyLocalization("cs", "Internet")]
        [PropertyLocalization("en", "Internet")]
        public bool Internet { get; set; }

        [Property, PropertyLocalization("cs", "Satelit")]
        [PropertyLocalization("en", "Satelite")]
        public bool Satelit { get; set; }

        [Property, PropertyLocalization("cs", "Kabelová televize")]
        [PropertyLocalization("en", "Cabel television")]
        public bool CableTV { get; set; }

        [Property, PropertyLocalization("cs", "Kabelové rozvody")]
        [PropertyLocalization("en", "Cabel network")]
        public bool CableWires { get; set; }

        [Property, PropertyLocalization("cs", "Ostatní rozvody")]
        [PropertyLocalization("en", "Other networks")]
        public bool OtherWires { get; set; }
    }

    [ClassLocalization("cs", "Inženýrské sítì", "Inženýrské sítì")]
    [ClassLocalization("en", "Engineering sites", "Engineering sites")]
    public class IngSitesInfo
    {
        [Property, PropertyLocalization("cs", "Lokální plynové topení")]
        [PropertyLocalization("en", "Local gas heating ")]
        public bool LocalGasHeating { get; set; }

        [Property, PropertyLocalization("cs", "Lokální topení na tuhá paliva")]
        [PropertyLocalization("en", "Local solid propellant heating")]
        public bool LocalSolidHeating { get; set; }

        [Property, PropertyLocalization("cs", "Lokální elektrické topení")]
        [PropertyLocalization("en", "Local electric heating")]
        public bool LocalElHeating { get; set; }

        [Property, PropertyLocalization("cs", "Ústøední plynové topení")]
        [PropertyLocalization("en", "Central gas heating")]
        public bool CentralGasHeating { get; set; }

        [Property, PropertyLocalization("cs", "Ústøední topení na tuhá paliva")]
        [PropertyLocalization("en", "Central solid propellant heating")]
        public bool CentralSolidHeating { get; set; }

        [Property, PropertyLocalization("cs", "Ústøední elektrické topení")]
        [PropertyLocalization("en", "Central electric heating")]
        public bool CentralElHeating { get; set; }

        [Property, PropertyLocalization("cs", "Dálkové topení")]
        [PropertyLocalization("en", "Remote heating")]
        public bool CentralRemoteHeating { get; set; }

        [Property, PropertyLocalization("cs", "Jiné topení")]
        [PropertyLocalization("en", "Other heating")]
        public bool AnotherHeating { get; set; }

        [Property, PropertyLocalization("cs", "Vlak")]
        [PropertyLocalization("en", "Train")]
        public bool TransportRail { get; set; }

        [Property, PropertyLocalization("cs", "Dálnice")]
        [PropertyLocalization("en", "Highway")]
        public bool TransportHighWay { get; set; }

        [Property, PropertyLocalization("cs", "Silnice")]
        [PropertyLocalization("en", "Road")]
        public bool TransportRoad { get; set; }

        [Property, PropertyLocalization("cs", "MHD")]
        [PropertyLocalization("en", "City transport")]
        public bool TransportMHD { get; set; }

        [Property, PropertyLocalization("cs", "Autobus")]
        [PropertyLocalization("en", "Bus")]
        public bool TransportBus { get; set; }

        [Property, PropertyLocalization("cs", "Voda - zdroj pro celý objekt")]
        [PropertyLocalization("en", "Water - source for entire object")]
        public bool CentralWater { get; set; }

        [Property, PropertyLocalization("cs", "Voda - dálkový vodovod")]
        [PropertyLocalization("en", "Remote water conduit")]
        public bool RemoteWater { get; set; }

        [Property, PropertyLocalization("cs", "Voda - rozvod studené a teplé vody")]
        [PropertyLocalization("en", "Water - cool and hot conduit")]
        public bool HotCoolWater { get; set; }

        [Property, PropertyLocalization("cs", "Kanalizace")]
        [PropertyLocalization("en", "Waste")]
        public bool Drainage { get; set; }

        [Property, PropertyLocalization("cs", "ÈOV pro celý objekt")]
        [PropertyLocalization("en", "Sewerage plant for entire object")]
        public bool CentralWaterCleaner { get; set; }

        [Property, PropertyLocalization("cs", "Plyn - individuální")]
        [PropertyLocalization("en", "Individual gas")]
        public bool IndividualGas { get; set; }

        [Property, PropertyLocalization("cs", "Plynovod")]
        [PropertyLocalization("en", "Gas conduit")]
        public bool GasConduit { get; set; }
    }

    [ClassLocalization("cs", "Elektøina", "Elektøina")]
    [ClassLocalization("en", "Eletricity", "Eletricity")]
    public class ElectricityInfo
    {
        [Property, PropertyLocalization("cs", "Elektro - 120 V")]
        [PropertyLocalization("en", "Eletricity - 120 V")]
        public bool El120V { get; set; }

        [Property, PropertyLocalization("cs", "Elektro - 230 V")]
        [PropertyLocalization("en", "Eletricity - 230 V")]
        public bool El230V { get; set; }

        [Property, PropertyLocalization("cs", "Elektro - 380 V")]
        [PropertyLocalization("en", "Eletricity - 380 V")]
        public bool El380V { get; set; }

        [Property, PropertyLocalization("cs", "Elektro - 400 V")]
        [PropertyLocalization("en", "Eletricity - 400 V")]
        public bool El400V { get; set; }
    }

    [ClassLocalization("cs", "Vybavenost", "Vybavenost")]
    [ClassLocalization("en", "Facilities", "Facilities")]
    public class FacilitiesInfo
    {
        [Property, PropertyLocalization("cs", "Škola")]
        [PropertyLocalization("en", "School")]
        public bool School { get; set; }

        [Property, PropertyLocalization("cs", "Školka")]
        [PropertyLocalization("en", "Playschool")]
        public bool PlayGroup { get; set; }

        [Property, PropertyLocalization("cs", "Zdravotnická zaøízení")]
        [PropertyLocalization("en", "Sanitation")]
        public bool Hospital { get; set; }

        [Property, PropertyLocalization("cs", "Pošta")]
        [PropertyLocalization("en", "Post")]
        public bool Post { get; set; }

        [Property, PropertyLocalization("cs", "Supermarket")]
        [PropertyLocalization("en", "Supermarket")]
        public bool Supermarket { get; set; }

        [Property, PropertyLocalization("cs", "Kompletní sí obchodù a služeb")]
        [PropertyLocalization("en", "Complet grid of markets and services")]
        public bool CompleteSite { get; set; }
    }

    [ClassLocalization("cs", "Rozšiøující informace", "Rozšiøující informace")]
    [ClassLocalization("en", "Aditional information", "Aditional informations")]
    public class EstateExtendedInfo
    {
        [PropertyLocalization("cs", "Èíslo zakázky"), Property("OrderNumber")]
        [PropertyLocalization("en", "Order number")]
        public string OrderNumber { get; set; }

        [PropertyLocalization("cs", "Url virtuání prohlídky"), Property("VirtualTourUrl")]
        [PropertyLocalization("en", "Virtual tour Url")]
        public string VirtualTourUrl { get; set; }
    }

    [ClassLocalization("cs", "Obrázky", "Obrázky")]
    [ClassLocalization("en", "Picture", "Pictures")]
    public class EstatePictures
    {
        [PropertyLocalization("cs", "Fotka 1"),
         Property("Photo1", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 1")]
        public byte[] Photo1 { get; set; }

        [PropertyLocalization("cs", "Fotka 2"),
         Property("Photo2", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 2")]
        public byte[] Photo2 { get; set; }

        [PropertyLocalization("cs", "Fotka 3"),
         Property("Photo3", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 3")]
        public byte[] Photo3 { get; set; }

        [PropertyLocalization("cs", "Fotka 4"),
         Property("Photo4", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 4")]
        public byte[] Photo4 { get; set; }

        [PropertyLocalization("cs", "Fotka 5"),
         Property("Photo5", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 5")]
        public byte[] Photo5 { get; set; }

        [PropertyLocalization("cs", "Fotka 6"),
         Property("Photo6", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 6")]
        public byte[] Photo6 { get; set; }

        [PropertyLocalization("cs", "Fotka 7"),
         Property("Photo7", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 7")]
        public byte[] Photo7 { get; set; }

        [PropertyLocalization("cs", "Fotka 8"),
         Property("Photo8", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 8")]
        public byte[] Photo8 { get; set; }

        [PropertyLocalization("cs", "Fotka 9"),
         Property("Photo9", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 9")]
        public byte[] Photo9 { get; set; }

        [PropertyLocalization("cs", "Fotka 10"),
         Property("Photo10", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 10")]
        public byte[] Photo10 { get; set; }

        [PropertyLocalization("cs", "Fotka 11"),
         Property("Photo11", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 11")]
        public byte[] Photo11 { get; set; }

        [PropertyLocalization("cs", "Fotka 12"),
         Property("Photo12", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 12")]
        public byte[] Photo12 { get; set; }

        [PropertyLocalization("cs", "Fotka 13"),
         Property("Photo13", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 13")]
        public byte[] Photo13 { get; set; }

        [PropertyLocalization("cs", "Fotka 14"),
         Property("Photo14", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 14")]
        public byte[] Photo14 { get; set; }

        [PropertyLocalization("cs", "Fotka 15"),
         Property("Photo15", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 15")]
        public byte[] Photo15 { get; set; }

        [PropertyLocalization("cs", "Fotka 16"),
         Property("Photo16", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 16")]
        public byte[] Photo16 { get; set; }

        [PropertyLocalization("cs", "Fotka 17"),
         Property("Photo17", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 17")]
        public byte[] Photo17 { get; set; }

        [PropertyLocalization("cs", "Fotka 18"),
         Property("Photo18", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 18")]
        public byte[] Photo18 { get; set; }

        [PropertyLocalization("cs", "Fotka 19"),
         Property("Photo19", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 19")]
        public byte[] Photo19 { get; set; }

        [PropertyLocalization("cs", "Fotka 20"),
         Property("Photo20", ColumnType = "BinaryBlob", SqlType = "varbinary(MAX)")]
        [PropertyLocalization("en", "Photo 20")]
        public byte[] Photo20 { get; set; }


        public StringCollection GetAllPhotosLinksForWeb(int id, Type declaringType)
        {
            var resutl = new StringCollection();

            foreach (PropertyInfo prop in GetType().GetProperties())
            {
                if (prop.Name.StartsWith("Photo") && prop.PropertyType == typeof (byte[]))
                {
                    if ((prop.GetValue(this, null) as byte[]) != null)
                    {
                        if ((prop.GetValue(this, null) as byte[]).Length > 0)
                        {
                            resutl.Add("/estateimage.axd?t=" + declaringType.Name + "&amp;p=" + prop.Name + "&amp;id="
                                       + id + "&amp;cached=true");
                        }
                    }
                }
            }

            return resutl;
        }
    }
}