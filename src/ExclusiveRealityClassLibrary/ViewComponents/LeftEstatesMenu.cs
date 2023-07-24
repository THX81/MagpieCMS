using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections;

namespace ExclusiveReality.ViewComponents
{
    public class LeftEstatesMenu : ViewComponent
    {
        public override void Render()
        {
            string cacheKey = "LeftEstatesMenuNodes" + Request.Uri;
            IList<Section> nodes = ExclusiveReality.Helpers.CacheHelper.Get<IList<Section>>(cacheKey);

            if (nodes == null || nodes.Count == 0)
            {
                Section secondLevelSection = FindSecondLevel(PropertyBag["CurrentSection"] as Section);

                if (secondLevelSection != null)
                    nodes = secondLevelSection.Sections;

                if (nodes != null && nodes.Count > 0)
                    ExclusiveReality.Helpers.CacheHelper.Set(cacheKey, nodes);
            }

            PropertyBag["Sections"] = nodes;
            base.Render();
        }


        private Section FindSecondLevel(Section section)
        {
            if (Thread.CurrentThread.CurrentCulture.LCID == 1029)
            {
                if (section.ParentSection == null || section.ParentSection.ParentSection == null)
                    return section;

                return FindSecondLevel(section.ParentSection);
            }
            
            if (section.ParentSection == null || section.ParentSection.ParentSection == null || section.ParentSection.ParentSection.ParentSection == null)
                return section;

            return this.FindSecondLevel(section.ParentSection);
        }
    }
}
