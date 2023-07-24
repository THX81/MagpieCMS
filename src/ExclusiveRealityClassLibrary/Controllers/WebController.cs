using System;
using System.Threading;
using System.Web;
using ExclusiveReality.Models;

namespace ExclusiveReality.Controllers
{
    public class WebController : BaseController
    {
        private void ResolveCaching()
        {
            object expiration = HttpContext.Current.Application["OutputExpiration"];
            DateTime newExpiration = DateTime.Now.AddSeconds(600);
            if (expiration == null || !(expiration is DateTime))
            {
                HttpContext.Current.Application["OutputExpiration"] = newExpiration;
                Response.CachePolicy.SetExpires(newExpiration);
            }
            else if (expiration is DateTime)
            {
                if (DateTime.Parse(expiration.ToString()) < DateTime.Now)
                {
                    HttpContext.Current.Application["OutputExpiration"] = null;
                }
            }
        }

        protected override void Initialize()
        {
            this.ResolveCaching();

            base.Initialize();
        }

        public void ContentPage()
        {
            LayoutName = "default_web_" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            if (HttpContext.Current.Items["Page"] == null)
            {
                this.Page404();
            }
            else
            {
                var page = HttpContext.Current.Items["Page"] as Page;
                var section = HttpContext.Current.Items["Section"] as Section;
                PropertyBag["CurrentPage"] = page;
                PropertyBag["CurrentSection"] = section;
                if (page != null)
                    PropertyBag["CurrentCultureId"] = page.GetEnsuredCulture(true).Id;

                if (page == null || page.PageTemplate == null || String.IsNullOrEmpty(page.PageTemplate.Name))
                {
                    RenderView("IndexPage");
                }
                else
                {
                    RenderView(page.PageTemplate.Name);
                }
            }
        }

        //public void IndexPage()
        //{
        //    if (System.Web.HttpContext.Current.Items["Section"] == null)
        //        Page404();
        //    else
        //    {
        //        Section actualSection = System.Web.HttpContext.Current.Items["Section"] as Section;
        //        PropertyBag["Section"] = actualSection;
        //        RenderView("IndexPage");
        //    }
        //}


        public void Page404()
        {
            PropertyBag["Request"] = Request;

            Response.StatusCode = 404;
            RenderView("Page404");
        }
    }
}