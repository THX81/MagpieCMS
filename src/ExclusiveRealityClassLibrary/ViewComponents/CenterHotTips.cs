using System.Threading;
using Castle.ActiveRecord.Queries;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;

namespace ExclusiveReality.ViewComponents
{
    public class CenterHotTips : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "CenterHotTips" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var hotTipEstates = CacheHelper.Get<Estate[]>(cacheKey);

            if (hotTipEstates == null || hotTipEstates.Length == 0)
            {
                var hotTipEstatesQuery =
                    new SimpleQuery<Estate>("from Estate e where e.Publish=1 and e.HotTip=1 and e.Saled=0 and e.Rented=0 and e.DeveloperProject is null order by e.Created desc");
                hotTipEstatesQuery.SetQueryRange(4);
                hotTipEstates = hotTipEstatesQuery.Execute();

                if (hotTipEstates.Length > 0)
                {
                    CacheHelper.Set(cacheKey, hotTipEstates);
                }
            }

            if (hotTipEstates != null)
            {
                PropertyBag["HotTipEstates"] = hotTipEstates;
            }
            base.Render();
        }
    }
}