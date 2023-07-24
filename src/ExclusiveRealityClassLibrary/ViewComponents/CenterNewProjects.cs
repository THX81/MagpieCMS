using System.Threading;
using Castle.ActiveRecord.Queries;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;

namespace ExclusiveReality.ViewComponents
{
    public class CenterNewProjects : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "CenterNewProjects" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var newProjects = CacheHelper.Get<DeveloperProject[]>(cacheKey);

            if (newProjects == null || newProjects.Length == 0)
            {
                var newProjectsQuery =
                    new SimpleQuery<DeveloperProject>("from DeveloperProject d where d.Publish=1 order by d.Created desc");
                newProjectsQuery.SetQueryRange(4);
                newProjects = newProjectsQuery.Execute();

                if (newProjects.Length > 0)
                {
                    CacheHelper.Set(cacheKey, newProjects);
                }
            }

            PropertyBag["NewProjects"] = newProjects;

            base.Render();
        }
    }
}