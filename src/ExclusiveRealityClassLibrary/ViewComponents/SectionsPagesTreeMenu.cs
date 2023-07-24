using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Data;
using System.Reflection;

namespace ExclusiveReality.ViewComponents
{
    public class SectionsPagesTreeMenu : ViewComponent
    {
        private ExclusiveReality.Models.Base.ISiteNode selectedNode;



        public override void Render()
        {
            this.selectedNode = PropertyBag["Page"] as ExclusiveReality.Models.Base.ISiteNode;
            if (this.selectedNode == null)
                this.selectedNode = PropertyBag["Section"] as ExclusiveReality.Models.Base.ISiteNode;

            StringBuilder sbMenu = new StringBuilder();

            //sbMenu.Append("<a href=\"/admin_exclusivereal/sectionspages/newpage.aspx\" href=\"Vytvořit novou stránku\">Vytvořit novou stránku</a><br />" + Environment.NewLine);
            //sbMenu.Append("<a href=\"/admin_exclusivereal/sectionspages/newsection.aspx\" href=\"Vytvořit novou sekci\">Vytvořit novou sekci</a><br />" + Environment.NewLine);
            //sbMenu.Append("<br />" + Environment.NewLine);
            //sbMenu.Append("<br />" + Environment.NewLine);

            Section[] sections = new SimpleQuery<Section>(typeof(Section), "from Section s where s.ParentSection is null order by s.OrderPriority asc, s.Title asc").Execute();
            if (sections.Length > 0)
                BuildMenu(sections[0], " ", ref sbMenu);

            RenderText(sbMenu.ToString());
            //base.Render();
        }

        private void BuildMenu(Section section, String indent, ref StringBuilder sbMenu)
        {

            IList<ExclusiveReality.Models.Base.ISiteNode> nodes = section.GetSectionNodes(false);

            if (nodes.Count > 0)
            {
                sbMenu.Append(indent + "<ul>" + Environment.NewLine);
            }
            foreach (ExclusiveReality.Models.Base.ISiteNode node in nodes)
            {
                sbMenu.Append(indent + indent + "<li class=\"" + (node is Section ? "section_ico" : "page_ico") + "\">");

                sbMenu.Append("<a href=\"/admin_exclusivereal/sectionspages/" + (node is Section ? "editsection" : "editpage") + ".aspx");
                sbMenu.Append("?id=" + node.Id + "\" title=\"" + node.Title + "\">");
                if (this.selectedNode != null)
                    if (this.selectedNode.Id == node.Id && this.selectedNode.GetType() == node.GetType())
                        sbMenu.Append("<b>");
                sbMenu.Append(node.Title);
                if (this.selectedNode != null)
                    if (this.selectedNode.Id == node.Id && this.selectedNode.GetType() == node.GetType())
                        sbMenu.Append("</b>");
                sbMenu.Append("</a>" + Environment.NewLine);
                if (node is Section)
                    BuildMenu(node as Section, (indent + indent + indent), ref sbMenu);

                sbMenu.Append(indent + indent + "</li>" + Environment.NewLine);
            }
            if (nodes.Count > 0)
                sbMenu.Append(indent + "</ul>" + Environment.NewLine);

        }
    }
}
