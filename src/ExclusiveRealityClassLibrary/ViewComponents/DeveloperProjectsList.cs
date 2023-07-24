using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Collections.ObjectModel;

namespace ExclusiveReality.ViewComponents
{
    public class DeveloperProjectsList : ViewComponent
    {
        private int foreignProject;
        
        public DeveloperProjectsList()
        {
            foreignProject = 0;
        }

        public override void Initialize()
        {
            var foreignProjectTmp = ComponentParams["ForeignProject"] as string;
            if (!String.IsNullOrEmpty(foreignProjectTmp) && Regex.IsMatch(foreignProjectTmp, "^[0-9]+$", RegexOptions.IgnoreCase))
                foreignProject = int.Parse(foreignProjectTmp);
            base.Initialize();
        }

        public override void Render()
        {
            DeveloperProject[] projects;

            #region databinding for project repeater

            var sbProjectsSql = new StringBuilder("from DeveloperProject p where p.Publish = 1 and p.ForeignProject = " + this.foreignProject);

            string cacheKey = "EstatesRepeater_" + Request.QueryString["page"] + "_" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            projects = Helpers.CacheHelper.Get<DeveloperProject[]>(cacheKey);

            PropertyBag["PagerHtml"] = GetPagerHtml(GetProjectsCount(sbProjectsSql.ToString()));
            if (projects == null || projects.Length == 0)
            {
                try
                {
                    // razeni od nejnovejsich
                    sbProjectsSql.Append(" order by p.Created desc");

                    projects = GetPagedProjects(sbProjectsSql.ToString());
                }
                catch (Exception ex)
                {
                    Response.Write(ex + "<br>");
                }

                if (projects != null)
                    if (projects.Length > 0)
                        Helpers.CacheHelper.Set(cacheKey, projects);
            }

            #endregion

            PropertyBag["DeveloperProjects"] = projects;


            base.Render();

        }

        private DeveloperProject[] GetPagedProjects(string sql)
        {
            var rowsPage = 0;
            const int maxRows = 8;

            if (!String.IsNullOrEmpty(Request.QueryString["page"]) &&
                Regex.IsMatch(Request.QueryString["page"], "^[0-9]+$", RegexOptions.IgnoreCase))
                rowsPage = int.Parse(Request.QueryString["page"]);

            var sqProjects = new SimpleQuery<DeveloperProject>(sql);
            sqProjects.SetQueryRange(rowsPage * maxRows, maxRows);
            return sqProjects.Execute();
        }

        private static int GetProjectsCount(string sql)
        {
            var sqProjects = new SimpleQuery<int>(typeof(Estate), "select p.Id " + sql);
            return sqProjects.Execute().Length;
        }

        private string GetPagerHtml(int totalCount)
        {
            const decimal maxRows = 8;
            int actualPage = (!String.IsNullOrEmpty(Request.QueryString["page"]) &&
                              Regex.IsMatch(Request.QueryString["page"], "^[0-9]+$", RegexOptions.IgnoreCase)
                                  ? int.Parse(Request.QueryString["page"])
                                  : 0);

            decimal pages = (decimal)totalCount / maxRows;

            if (pages <= 1) return String.Empty;


            var sb = new StringBuilder();
            sb.Append("  <p class=\"right\">");
            if (actualPage > 0)
                sb.Append("    <strong><a href=\"" + GetUrlAndQueryString("page", (actualPage - 1)) +
                          "\">&laquo;</a></strong> ");

            for (int x = 0; x < pages; x++)
            {
                if (x == actualPage)
                    sb.Append(x + 1);
                else
                    sb.Append("<a href=\"" + GetUrlAndQueryString("page", x) + "\">" + (x + 1) + "</a>");
                if ((x + 1) < pages)
                    sb.Append(", ");
            }

            if (actualPage < (pages - 1))
                sb.Append("    <strong><a href=\"" + GetUrlAndQueryString("page", actualPage + 1) +
                          "\">&raquo;</a></strong>  ");
            sb.Append("  </p>");

            return sb.ToString();
        }

        private string GetUrlAndQueryString(string parname, object parValue)
        {
            var result = new StringBuilder(Request.QueryString["originalurl"] + "?");

            foreach (string key in Request.QueryString.Keys)
                if (key != "originalurl" && key != parname)
                    result.Append(key + "=" + Request.QueryString[key] + "&");

            if (result.ToString().EndsWith("&"))
                result.Remove(result.Length - 1, 1);

            if (!String.IsNullOrEmpty(parname))
                result.Append("&" + parname + "=" + parValue);

            return result.ToString();
        }

    }
}
