using System;
using Castle.ActiveRecord;

namespace ExclusiveReality.Models.Base
{
    public interface ISiteNode
    {
        [PrimaryKey]
        int Id { get; set; }

        [Property("Title")]
        String Title { get; set; }

        [Property("Name")]
        String Name { get; set; }

        [BelongsTo("CultureId")]
        Culture Culture { get; set; }

        [Property("OrderPriority")]
        Int32 OrderPriority { get; set; }

        [Property("Created")]
        DateTime Created { get; set; }

        [Property("Published")]
        bool Published { get; set; }


        String GetUrl(bool cached);
    }
}