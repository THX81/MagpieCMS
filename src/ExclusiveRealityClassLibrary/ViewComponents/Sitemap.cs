using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Collections.ObjectModel;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.ViewComponents
{
    public class Sitemap : ViewComponent
    {
        public override void Render()
        {
            Section[] rootSection = null;
            if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs")
                rootSection = new SimpleQuery<Section>(typeof(Section), "from Section s where s.Name = '' and s.Published = 1 and s.ParentSection is null").Execute();
            else if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
                rootSection = new SimpleQuery<Section>(typeof(Section), "from Section s where s.Name = 'en' and s.Published = 1 and s.ParentSection.Name=''").Execute();

            if (rootSection.Length > 0)
                RenderNodes(rootSection[0], " ");

            base.Render();
        }

        private void RenderNodes(Section section, string indent)
        {
            IList<ExclusiveReality.Models.Base.ISiteNode> nodes = section.GetSectionNodes(false);

            if (nodes.Count > 0)
            {
                base.RenderText(indent + "<ul>" + Environment.NewLine);
            }
            foreach (ExclusiveReality.Models.Base.ISiteNode node in nodes)
            {
                if (
                    (node.Name.ToLower() == "index" && node is Page)
                    || (node.Name.ToLower().StartsWith("en") && node is Section)
                    )
                    continue;

                base.RenderText(indent + indent + "<li class=\"" + (node is Section ? "section_ico" : "page_ico") + "\">");

                base.RenderText("<a href=\"");
                base.RenderText(node.GetUrl(true) + "\" title=\"" + node.Title + "\">");
                base.RenderText(node.Title);
                base.RenderText("</a>" + Environment.NewLine);
                if (node is Section)
                    RenderNodes(node as Section, (indent + indent + indent));

                base.RenderText(indent + indent + "</li>" + Environment.NewLine);
            }
            if (nodes.Count > 0)
                base.RenderText(indent + "</ul>" + Environment.NewLine);
        }
    }
}
