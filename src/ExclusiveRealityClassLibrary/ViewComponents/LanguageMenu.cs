using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;

namespace ExclusiveReality.ViewComponents
{
    public class LanguageMenu : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "LanguageMenu" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            Page connectedPage = ExclusiveReality.Helpers.CacheHelper.Get<Page>(cacheKey);

            if (connectedPage == null)
            {
                Page currentPage = (base.Context.ContextVars["CurrentPage"] as Page);
                if (currentPage != null)
                    connectedPage = currentPage.GetConnectedPage(true);

                if (connectedPage != null)
                    ExclusiveReality.Helpers.CacheHelper.Set(cacheKey, connectedPage);
            }

            PropertyBag["ConnectedPage"] = connectedPage;
            PropertyBag["TwoLetterISOLanguageName"] = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            base.Render();
        }
    }
}
