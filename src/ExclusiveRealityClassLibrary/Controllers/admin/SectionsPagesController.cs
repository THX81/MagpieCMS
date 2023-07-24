namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Models;
    using Castle.ActiveRecord.Queries;
    using Castle.ActiveRecord.Framework.Internal;
    using Castle.MonoRail.ActiveRecordSupport;

    public class SectionsPagesController : AdminARSmartDispatcherController
	{
        public void Index()
        {
        }

        public void NewSection()
        {
            Section newSection = new Section();
            ActiveRecordModel model = ActiveRecordModel.GetModel(newSection.GetType());

            PropertyBag["Section"] = newSection;
            PropertyBag["model"] = model;
            PropertyBag["prefix"] = model.Type.Name;
            PropertyBag["AllSections"] = Section.FindAll();
        }

        public void CreateSection()
        {
            try
            {
                Section newSection = new Section();
                newSection.Title = Request.Params["Section.Title"];
                newSection.Name = Request.Params["Section.Name"];
                if (!String.IsNullOrEmpty(Request.Params["Section.OrderPriority"]))
                    newSection.OrderPriority = Convert.ToInt32(Request.Params["Section.OrderPriority"]);
                if (!String.IsNullOrEmpty(Request.Params["Section.Culture"]))
                    newSection.Culture = Culture.GetById(Convert.ToInt32(Request.Params["Section.Culture"]));
                if (!String.IsNullOrEmpty(Request.Params["Section.Published"]))
                    newSection.Published = bool.Parse(Request.Params["Section.Published"].Split(',')[0]);
                if (!String.IsNullOrEmpty(Request.Params["Section.ParentSection"]))
                    newSection.ParentSection = Section.GetSectionById(Convert.ToInt32(Request.Params["Section.ParentSection"]));

                newSection.Create();

                PropertyBag["Section"] = newSection;
            }
            catch (Exception ex)
            {
                Flash["error"] = ex.Message;

                Response.Write(Request.Params["Section.Published"]);
                Response.Write("<br>");
                Response.Write(ex);
                CancelView();
                //RedirectToAction("newsection", Request.Form);
            }
        }

        public void EditSection(int id)
        {
            Section selectedSection = Section.GetSectionById(id);
            if (selectedSection != null)
            {
                ActiveRecordModel model = ActiveRecordModel.GetModel(selectedSection.GetType());
                
                PropertyBag["Section"] = selectedSection;
                PropertyBag["model"] = model;
                PropertyBag["prefix"] = model.Type.Name;
            }
        }

        public void UpdateSection([ARDataBind("Section", AutoLoad = AutoLoadBehavior.Always)] Section section)
        {
            try
            {
                section.Update();

                PropertyBag["Section"] = section;
            }
            catch (Exception ex)
            {
                Flash["error"] = ex.Message;

                RedirectToAction("editsection", Request.Form);
            }
        }

        public void DeleteSection([ARDataBind("Section", AutoLoad = AutoLoadBehavior.Always)] Section section)
        {
            try
            {
                section.Delete();
                RedirectToAction("index");
            }
            catch (Exception ex)
            {
                Flash["error"] = ex.Message;
                Response.Write(ex);
                CancelView();
                //RedirectToAction("editsection", Request.Form);
            }
        }



        public void NewPage()
        {
            Page newPage = new Page();
            ActiveRecordModel model = ActiveRecordModel.GetModel(newPage.GetType());

            PropertyBag["Page"] = newPage;
            PropertyBag["model"] = model;
            PropertyBag["prefix"] = model.Type.Name;
            PropertyBag["AllPages"] = Page.FindAll();
            PropertyBag["AllPageTemplates"] = PageTemplate.FindAll();
        }

        public void CreatePage([DataBind("Page")] Page page)
        {
            try
            {
                page.PageTemplate = PageTemplate.GetById(Convert.ToInt32(Params["Page.PageTemplate"]));
                page.Create();
                if (page.ConnectedPageId > 0)
                    page.SetConnectedPage(Page.GetPageById(page.ConnectedPageId));

                PropertyBag["Page"] = page;
            }
            catch (Exception ex)
            {
                Flash["error"] = ex.Message;

                RedirectToAction("newpage", Request.Form);
            }
        }

        public void EditPage(int id)
        {
            Page selectedPage = Page.GetPageById(id);
            if (selectedPage != null)
            {
                ActiveRecordModel model = ActiveRecordModel.GetModel(selectedPage.GetType());

                PropertyBag["Page"] = selectedPage;
                PropertyBag["model"] = model;
                PropertyBag["prefix"] = model.Type.Name;
                PropertyBag["AllPages"] = Page.FindAll();
                PropertyBag["AllPageTemplates"] = PageTemplate.FindAll();
            }
        }

        public void UpdatePage([ARDataBind("Page", AutoLoad = AutoLoadBehavior.Always)] Page page)
        {
            try
            {
                page.PageTemplate = PageTemplate.GetById(Convert.ToInt32(Params["Page.PageTemplate"]));
                page.Update();
                if (page.ConnectedPageId > 0)
                    page.SetConnectedPage(Page.GetPageById(page.ConnectedPageId));

                PropertyBag["Page"] = page;
            }
            catch (Exception ex)
            {
                Flash["error"] = ex.Message;

                RedirectToAction("editpage", Request.Form);
            }
        }

        public void DeletePage([ARDataBind("Page", AutoLoad = AutoLoadBehavior.Always)] Page page)
        {
            try
            {
                page.Delete();
                RedirectToAction("index");
            }
            catch (Exception ex)
            {
                Flash["error"] = ex.Message;

                RedirectToAction("editpage", Request.Form);
            }
        }
    }
}
