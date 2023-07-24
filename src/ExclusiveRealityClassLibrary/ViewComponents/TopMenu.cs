using System.Collections.Generic;
using System.Text;
using System.Threading;
using Castle.ActiveRecord.Queries;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.ViewComponents
{
    public class TopMenu : ViewComponent
    {
        public override void Render()
        {
            List<ISiteNode> finalNodes = this.GetNodes();
            if (finalNodes.Count > 0)
            {
                PropertyBag["Nodes"] = finalNodes;
                PropertyBag["LastNode"] = this.GetLastNode();
                PropertyBag["TopMenu"] = this;
            }
            base.Render();
        }

        private List<ISiteNode> GetNodes()
        {
            string cacheKey = "TopMenuNodes" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var finalNodes = CacheHelper.Get<List<ISiteNode>>(cacheKey);

            if (finalNodes == null || finalNodes.Count == 0)
            {
                finalNodes = new List<ISiteNode>();
                Section[] rootSection = null;
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs")
                {
                    rootSection = new SimpleQuery<Section>(typeof (Section), "from Section s where s.Name = '' and s.Published = 1 and s.ParentSection is null").Execute();
                }
                else if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
                {
                    rootSection = new SimpleQuery<Section>(typeof (Section), "from Section s where s.Name = 'en' and s.Published = 1 and s.ParentSection.Name=''").Execute();
                }


                if (rootSection.Length > 0)
                {
                    List<ISiteNode> nodes = rootSection[0].GetSectionNodes(true);

                    foreach (ISiteNode node in nodes)
                    {
                        if (node.Name.Contains("vyhledavani") || node.Name.Contains("search"))
                        {
                            nodes.Remove(node);
                            break;
                        }
                    }
                    foreach (ISiteNode node in nodes)
                    {
                        if (node.Name.Contains("mapa-webu") || node.Name.Contains("sitemap"))
                        {
                            nodes.Remove(node);
                            break;
                        }
                    }
                    foreach (ISiteNode node in nodes)
                    {
                        if (node.Name.Contains("aktuality") || node.Name.Contains("actualities"))
                        {
                            nodes.Remove(node);
                            break;
                        }
                    }
                    foreach (ISiteNode node in nodes)
                    {
                        if (node.Name.ToLower().StartsWith("en"))
                        {
                            nodes.Remove(node);
                            break;
                        }
                    }

                    foreach (ISiteNode node in nodes)
                    {
                        if (node.Published)
                        {
                            finalNodes.Add(node);
                        }
                    }

                    if (finalNodes.Count > 0)
                    {
                        CacheHelper.Set(cacheKey, finalNodes);
                    }
                }
            }

            return finalNodes;
        }

        private ISiteNode GetLastNode()
        {
            string cacheKey = "TopMenuLastNode" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var lastNode = CacheHelper.Get<ISiteNode>(cacheKey);

            if (lastNode == null)
            {
                Page[] search = null;
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "cs")
                {
                    search = new SimpleQuery<Page>(typeof (Page), "from Page p where p.Name = 'vyhledavani'").Execute();
                }
                else if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
                {
                    search = new SimpleQuery<Page>(typeof (Page), "from Page p where p.Name = 'search'").Execute();
                }
                if (search.Length > 0)
                {
                    lastNode = search[0];
                }

                if (lastNode != null)
                {
                    CacheHelper.Set(cacheKey, lastNode);
                }
            }

            return lastNode;
        }


        public string GetSubNodesMenu(ISiteNode parentNode)
        {
            var sb = new StringBuilder();

            if (parentNode is Section)
            {
                var sec = parentNode as Section;
                List<ISiteNode> subNodes = sec.GetSectionNodes(false);
                if (subNodes.Count > 0)
                {
                    int sectionsCount = 0;
                    foreach (ISiteNode node in subNodes)
                    {
                        if (node is Section && ((Section) node).Published)
                        {
                            sectionsCount++;
                        }
                    }

                    if (sectionsCount > 0)
                    {
                        sb.AppendLine("<div>");
                        sb.AppendLine("    <ul>");
                        foreach (ISiteNode node in subNodes)
                        {
                            if (node is Section)
                            {
                                sb.AppendLine("        <li><strong>" + node.Title + "</strong>");
                                this.GetSubMenu(node, ref sb);
                                sb.AppendLine("        </li>");
                            }
                        }
                        sb.AppendLine("    </ul>");
                        sb.AppendLine("</div>");
                    }
                }
            }

            return sb.ToString();
        }

        private void GetSubMenu(ISiteNode parentNode, ref StringBuilder sb)
        {
            if (parentNode != null && parentNode is Section)
            {
                var parentSection = parentNode as Section;
                List<ISiteNode> subNodes = parentSection.GetSectionNodes(false);

                if (subNodes.Count > 0)
                {
                    sb.AppendLine("    <ul>");
                    foreach (ISiteNode subNode in subNodes)
                    {
                        if (subNode.Published)
                        {
                            sb.AppendLine("        <li><a href=\"" + subNode.GetUrl(false) + "\">" + subNode.Title
                                          + "</a>");
                            this.GetSubMenu(subNode, ref sb);
                            sb.AppendLine("        </li>");
                        }
                    }
                    sb.AppendLine("    </ul>");
                }
            }
        }
    }
}