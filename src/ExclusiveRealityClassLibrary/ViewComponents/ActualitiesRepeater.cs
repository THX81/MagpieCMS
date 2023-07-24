using System;
using System.Text.RegularExpressions;
using System.Threading;
using Castle.ActiveRecord.Queries;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;

namespace ExclusiveReality.ViewComponents
{
    public class ActualitiesRepeater : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "ActualitiesRepeater_" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var actualities = CacheHelper.Get<Actuality[]>(cacheKey);

            if (actualities == null || actualities.Length == 0)
            {
                actualities =
                    new SimpleQuery<Actuality>("from Actuality a where a.Publish = 1 order by a.Created desc").Execute();

                if (actualities.Length > 0)
                {
                    CacheHelper.Set(cacheKey, actualities);
                }
            }
            PropertyBag["Actualities"] = actualities;


            if (!String.IsNullOrEmpty(Request.QueryString["detail"])
                && Regex.IsMatch(Request.QueryString["detail"], "^[0-9]+$", RegexOptions.IgnoreCase))
            {
                string cacheKey1 = "EstatesRepeater_" + Request.QueryString["detail"] + "_"
                                   + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                var actuality = CacheHelper.Get<Actuality[]>(cacheKey1);

                if (actuality == null || actuality.Length == 0)
                {
                    actuality =
                        new SimpleQuery<Actuality>("from Actuality a where a.Publish = 1 and a.Id="
                                                   + Request.QueryString["detail"]).Execute();

                    if (actuality.Length > 0)
                    {
                        CacheHelper.Set(cacheKey1, actuality);
                    }
                }
                if (actuality.Length > 0)
                {
                    PropertyBag["SelectedActuality"] = actuality[0];
                }
            }

            base.Render();
        }
    }
}