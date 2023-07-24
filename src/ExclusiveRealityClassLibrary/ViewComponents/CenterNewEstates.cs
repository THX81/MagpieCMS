using System.Threading;
using Castle.ActiveRecord.Queries;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;

namespace ExclusiveReality.ViewComponents
{
    public class CenterNewEstates : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "CenterNewEstates" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var newEstates = CacheHelper.Get<Estate[]>(cacheKey);

            if (newEstates == null || newEstates.Length == 0)
            {
                var newEstatesQuery =
                    new SimpleQuery<Estate>("from Estate e where e.Publish=1 and e.Saled=0 and e.Rented=0 and e.DeveloperProject is null order by e.Created desc");
                newEstatesQuery.SetQueryRange(4, 10);
                newEstates = newEstatesQuery.Execute();

                if (newEstates.Length > 0)
                {
                    CacheHelper.Set(cacheKey, newEstates);
                }
            }

            if (newEstates != null)
            {
                PropertyBag["NewEstates"] = newEstates;
            }


            string cache3Key = "CenterNew3Estates" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var new3Estates = CacheHelper.Get<Estate[]>(cache3Key);

            if (new3Estates == null || new3Estates.Length == 0)
            {
                var new3EstatesQuery =
                    new SimpleQuery<Estate>("from Estate e where e.Publish=1 and e.Saled=0 and e.Rented=0 and e.DeveloperProject is null order by e.Created desc");
                new3EstatesQuery.SetQueryRange(4);
                new3Estates = new3EstatesQuery.Execute();

                if (new3Estates.Length > 0)
                {
                    CacheHelper.Set(cache3Key, new3Estates);
                }
            }

            if (new3Estates != null)
            {
                PropertyBag["New3Estates"] = new3Estates;
            }

            base.Render();
        }
    }
}