namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using Crud;
    using Models;
    using System.Web;
	using System.IO;
    using System.Collections;

    [ControllerDetails(Area = "admin_exclusivereal/estates")]
    [Crud(typeof(DeveloperProject))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class DeveloperProjectsController : AdminARSmartDispatcherController
	{
        public void AddDeveloperProjectImage()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.Form["Id"]))
                {
                    int developerProjectId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];

                    if (Request.Files.Count > 0)
                    {
                        foreach (object key in Request.Files.Keys)
                        {
                            var file = Request.Files[key] as HttpPostedFile;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filePath = "/img/developerprojects/" + developerProjectId + "_" + Path.GetFileName(file.FileName);

                                var buffer = new byte[file.ContentLength];
                                file.InputStream.Read(buffer, 0, file.ContentLength);
                                buffer = ExclusiveReality.HttpHandlers.EstateImageHttpHandler.GetResizedImage(buffer, 640, 480);

                                buffer = ExclusiveReality.HttpHandlers.EstateImageHttpHandler.ApplyWatermark(buffer);

                                File.WriteAllBytes(Context.Server.MapPath(filePath), buffer);

                                var image = new DeveloperProjectImage(DeveloperProject.GetById(developerProjectId), filePath, descripiton_cs, descripiton_en);
                                image.Create();
                            }
                            break;
                        }
                    }
                    var pars = new Hashtable
                               {
                                   {"id", developerProjectId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void UpdateImageDescription()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.Form["Id"]) && !String.IsNullOrEmpty(Request.Form["DeveloperProjectImageId"]))
                {
                    int estateId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];
                    int developerProjectImageId = int.Parse(Request.Form["DeveloperProjectImageId"]);


                    DeveloperProjectImage image = DeveloperProjectImage.GetById(developerProjectImageId);
                    if (image != null)
                    {
                        image.Description_cs = descripiton_cs;
                        image.Description_en = descripiton_en;
                        image.Update();
                    }


                    var pars = new Hashtable
                               {
                                   {"id", estateId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void SetDeveloperProjectImageAsMain()
        {
            try
            {
                CancelView();

                if (!String.IsNullOrEmpty(Request.QueryString["Id"]) && !String.IsNullOrEmpty(Request.QueryString["DeveloperProjectId"]))
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    int developerProjectId = int.Parse(Request.QueryString["DeveloperProjectId"]);

                    DeveloperProject es = DeveloperProject.GetById(developerProjectId);
                    if (es != null)
                        foreach (DeveloperProjectImage i in es.DeveloperProjectImages)
                        {
                            i.IsMain = false;
                            i.Update();
                        }

                    DeveloperProjectImage img = DeveloperProjectImage.GetById(id);
                    img.IsMain = true;
                    img.UpdateAndFlush();


                    var pars = new Hashtable
                               {
                                   {"id", developerProjectId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void DeleteDeveloperProjectImage()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.QueryString["Id"]) && !String.IsNullOrEmpty(Request.QueryString["DeveloperProjectId"]))
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    int developerProjectdId = int.Parse(Request.QueryString["DeveloperProjectId"]);

                    DeveloperProjectImage img = DeveloperProjectImage.GetById(id);
                    img.Delete();
                    img.DeleteFile();

                    var pars = new Hashtable
                               {
                                   {"id", developerProjectdId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }




        public void AddDeveloperProjectAttachment()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.Form["Id"]))
                {
                    int developerProjectId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];

                    if (Request.Files.Count > 0)
                    {
                        foreach (object key in Request.Files.Keys)
                        {
                            var file = Request.Files[key] as HttpPostedFile;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filePath = "/doccache/" + developerProjectId + "_" + Path.GetFileName(file.FileName);

                                var buffer = new byte[file.ContentLength];
                                file.InputStream.Read(buffer, 0, file.ContentLength);

                                File.WriteAllBytes(Context.Server.MapPath(filePath), buffer);

                                var attachment = new DeveloperProjectAttachment(DeveloperProject.GetById(developerProjectId), filePath, buffer.LongLength, descripiton_cs, descripiton_en);
                                attachment.Create();
                            }
                            break;
                        }
                    }
                    var pars = new Hashtable
                               {
                                   {"id", developerProjectId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void UpdateAttachmentDescription()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.Form["Id"]) && !String.IsNullOrEmpty(Request.Form["DeveloperProjectAttachmentId"]))
                {
                    int developerProjectId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];
                    int developerProjectAttachmentId = int.Parse(Request.Form["DeveloperProjectAttachmentId"]);


                    DeveloperProjectAttachment attachment = DeveloperProjectAttachment.GetById(developerProjectAttachmentId);
                    if (attachment != null)
                    {
                        attachment.Description_cs = descripiton_cs;
                        attachment.Description_en = descripiton_en;
                        attachment.Update();
                    }


                    var pars = new Hashtable
                               {
                                   {"id", developerProjectId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void DeleteDeveloperProjectAttachment()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.QueryString["Id"]) && !String.IsNullOrEmpty(Request.QueryString["DeveloperProjectId"]))
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    int developerProjectId = int.Parse(Request.QueryString["DeveloperProjectId"]);

                    DeveloperProjectAttachment attachment = DeveloperProjectAttachment.GetById(id);
                    attachment.Delete();
                    attachment.DeleteFile();

                    var pars = new Hashtable
                               {
                                   {"id", developerProjectId.ToString()},
                                   {"CurrentStep", "2"},
                                   {"OriginalAction", Request.Form["OriginalAction"]}
                               };
                    Redirect(Name, "edit", pars);
                }
                else
                {
                    Redirect(Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }
    }
}
