using Castle.ActiveRecord;
using ExclusiveReality.Models.Attributes;
using NHibernate.Expression;

namespace ExclusiveReality.Models.Base
{
    public abstract class LocalizationObjectBase<T> : ActiveRecordBase<T>
    {
        protected Culture culture;
        protected int id;

        [PropertyLocalization("cs", "Id")]
        [PrimaryKey]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        [BelongsTo("CultureId")]
        public Culture Culture
        {
            get { return this.culture; }
            set { this.culture = value; }
        }


        public static T[] FindByCulture(Culture culture)
        {
            return FindAll(Expression.Eq("Culture", culture));
        }

        public static T GetById(int id)
        {
            return (T) ActiveRecordMediator.FindByPrimaryKey(typeof (T), id, false);
        }
    }
}