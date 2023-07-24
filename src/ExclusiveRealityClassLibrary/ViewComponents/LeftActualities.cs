using System.Collections.Generic;
using System.Threading;
using Castle.ActiveRecord.Queries;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;

namespace ExclusiveReality.ViewComponents
{
    public class LeftActualities : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "LeftActualities" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var actualities = CacheHelper.Get<List<Actuality>>(cacheKey);

            if (actualities == null || actualities.Count == 0)
            {
                actualities = new List<Actuality>();
                Actuality[] actualitiesTmp =
                    new SimpleQuery<Actuality>(typeof (Actuality),
                                               "from Actuality a where a.Publish=1 order by a.Created desc").Execute();

                for (int x = 0; x < actualitiesTmp.Length; x++)
                {
                    actualities.Add(actualitiesTmp[x]);
                    if (x == 1)
                    {
                        break;
                    }
                }

                if (actualities.Count > 0)
                {
                    CacheHelper.Set(cacheKey, actualities);
                }
            }

            if (actualities != null)
            {
                PropertyBag["Actualities"] = actualities.ToArray();
            }

            base.Render();
        }
    }
}