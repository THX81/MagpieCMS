using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Collections.ObjectModel;

namespace ExclusiveReality.ViewComponents
{
    public class LeftMenu : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "LeftMenuNodes" + Request.Uri;
            List<ExclusiveReality.Models.Base.ISiteNode> nodes = ExclusiveReality.Helpers.CacheHelper.Get<List<ExclusiveReality.Models.Base.ISiteNode>>(cacheKey);

            if (nodes == null || nodes.Count == 0)
            {
                Section secondLevelSection = FindSecondLevel(PropertyBag["CurrentSection"] as Section);

                if (secondLevelSection != null)
                    nodes = secondLevelSection.GetSectionNodes(true);

                if (nodes != null && nodes.Count > 0)
                    ExclusiveReality.Helpers.CacheHelper.Set(cacheKey, nodes);
            }

            PropertyBag["Nodes"] = nodes;
            base.Render();
        }


        private Section FindSecondLevel(Section section)
        {
            if (Thread.CurrentThread.CurrentCulture.LCID == 1029)
            {
                if (section.ParentSection == null || section.ParentSection.ParentSection == null)
                    return section;
                else
                    return FindSecondLevel(section.ParentSection);
            }
            else
            {
                if (section.ParentSection == null || section.ParentSection.ParentSection == null || section.ParentSection.ParentSection.ParentSection == null)
                    return section;
                else
                    return FindSecondLevel(section.ParentSection);
            }
        }
    }
}
