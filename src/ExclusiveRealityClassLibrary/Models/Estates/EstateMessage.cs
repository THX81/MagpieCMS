using System;
using Castle.ActiveRecord;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord]
    [ClassLocalization("cs", "Nabídka/poptávka nemovitosti", "Nabídka/poptávka nemovitostí")]
    [ClassLocalization("en", "Offer/request of property", "Offers/requests of properties")]
    public class EstateMessage : BusinessObjectBase<EstateMessage>
    {
        public EstateMessage()
        {
            created = DateTime.Now;
        }

        [PropertyLocalization("cs", "Typ zprávy")]
        [PropertyLocalization("en", "Type of message")]
        [Property("MessageType")]
        public virtual string MessageType { get; set; }

        [PropertyLocalization("cs", "Typ nemovitosti")]
        [PropertyLocalization("en", "Type of property")]
        [Property("EstateType")]
        public virtual string EstateType { get; set; }

        [PropertyLocalization("cs", "Typ nabídky")]
        [PropertyLocalization("en", "Type of offer")]
        [Property("EstateOfferType")]
        public virtual string EstateOfferType { get; set; }

        [PropertyLocalization("cs", "Velikost/dispozice")]
        [PropertyLocalization("en", "Size/Disposition")]
        [Property("Ps")]
        public virtual string Ps { get; set; }

        [PropertyLocalization("cs", "Minimální cena")]
        [PropertyLocalization("en", "Minimum price")]
        [Property("PriceFrom")]
        public virtual string PriceFrom { get; set; }

        [PropertyLocalization("cs", "Maximální cena")]
        [PropertyLocalization("en", "Maximum price")]
        [Property("PriceTo")]
        public virtual string PriceTo { get; set; }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Description")]
        [Property("Description", ColumnType = "StringClob")]
        public virtual string Description { get; set; }

        [PropertyLocalization("cs", "Jméno")]
        [PropertyLocalization("en", "First name")]
        [Property("FirstName")]
        public virtual string FirstName { get; set; }

        [PropertyLocalization("cs", "Pøíjmení")]
        [PropertyLocalization("en", "Last name")]
        [Property("LastName")]
        public virtual string LastName { get; set; }

        [PropertyLocalization("cs", "Telefon")]
        [PropertyLocalization("en", "Telephone")]
        [Property("Phone")]
        public virtual string Phone { get; set; }

        [PropertyLocalization("cs", "E-mail")]
        [PropertyLocalization("en", "E-mail")]
        [Property("Email")]
        public virtual string Email { get; set; }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.MessageType) && !String.IsNullOrEmpty(this.EstateType)
                && !String.IsNullOrEmpty(this.EstateOfferType))
            {
                return Id + " - " + this.MessageType + " - " + this.EstateType + " - " + this.EstateOfferType;
            }
            return base.ToString();
        }
    }
}