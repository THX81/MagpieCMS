namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Crud;
    using ExclusiveReality.Models;
    using System.Web;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Collections;

    [ControllerDetails(Area = "admin_exclusivereal/estates")]
    [Crud(typeof(Estate))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class AllEstatesController : AdminARSmartDispatcherController
	{
        public void AddEstateImage()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.Form["Id"]))
                {
                    int estateId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];

                    if (Request.Files.Count > 0)
                    {
                        foreach (object key in Request.Files.Keys)
                        {
                            HttpPostedFile file = Request.Files[key] as HttpPostedFile;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filePath = "/img/estates/" + estateId + "_" + Path.GetFileName(file.FileName);

                                byte[] buffer = new byte[file.ContentLength];
                                file.InputStream.Read(buffer, 0, file.ContentLength);
                                buffer = ExclusiveReality.HttpHandlers.EstateImageHttpHandler.GetResizedImage(buffer, 640, 480);

                                buffer = ExclusiveReality.HttpHandlers.EstateImageHttpHandler.ApplyWatermark(buffer);

                                File.WriteAllBytes(Context.Server.MapPath(filePath), buffer);

                                EstateImage image = new EstateImage(Estate.GetById(estateId), filePath, descripiton_cs, descripiton_en);
                                image.Create();
                            }
                            break;
                        }
                    }
                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
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
                if (!String.IsNullOrEmpty(Request.Form["Id"]) && !String.IsNullOrEmpty(Request.Form["EstateImageId"]))
                {
                    int estateId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];
                    int estateImageId = int.Parse(Request.Form["EstateImageId"]);


                    EstateImage image = EstateImage.GetById(estateImageId);
                    if (image != null)
                    {
                        image.Description_cs = descripiton_cs;
                        image.Description_en = descripiton_en;
                        image.Update();
                    }


                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void SetEstateImageAsMain()
        {
            try
            {
                CancelView();

                if (!String.IsNullOrEmpty(Request.QueryString["Id"]) && !String.IsNullOrEmpty(Request.QueryString["EstateId"]))
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    int estateId = int.Parse(Request.QueryString["EstateId"]);

                    Estate es = Estate.GetById(estateId);
                    if (es != null)
                        foreach (EstateImage i in es.EstateImages)
                        {
                            i.IsMain = false;
                            i.Update();
                        }

                    EstateImage img = EstateImage.GetById(id);
                    img.IsMain = true;
                    img.UpdateAndFlush();


                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void DeleteEstateImage()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.QueryString["Id"]) && !String.IsNullOrEmpty(Request.QueryString["EstateId"]))
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    int estateId = int.Parse(Request.QueryString["EstateId"]);

                    EstateImage img = EstateImage.GetById(id);
                    img.Delete();

                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }




        public void AddEstateAttachment()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.Form["Id"]))
                {
                    int estateId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];

                    if (Request.Files.Count > 0)
                    {
                        foreach (object key in Request.Files.Keys)
                        {
                            HttpPostedFile file = Request.Files[key] as HttpPostedFile;
                            if (file != null && file.ContentLength > 0)
                            {
                                string filePath = "/doccache/" + estateId + "_" + Path.GetFileName(file.FileName);

                                byte[] buffer = new byte[file.ContentLength];
                                file.InputStream.Read(buffer, 0, file.ContentLength);

                                File.WriteAllBytes(Context.Server.MapPath(filePath), buffer);

                                EstateAttachment attachment = new EstateAttachment(Estate.GetById(estateId), filePath, buffer.LongLength, descripiton_cs, descripiton_en);
                                attachment.Create();
                            }
                            break;
                        }
                    }
                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
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
                if (!String.IsNullOrEmpty(Request.Form["Id"]) && !String.IsNullOrEmpty(Request.Form["EstateAttachmentId"]))
                {
                    int estateId = int.Parse(Request.Form["Id"]);
                    string descripiton_cs = Request.Form["Description_cs"];
                    string descripiton_en = Request.Form["Description_en"];
                    int estateAttachmentId = int.Parse(Request.Form["EstateAttachmentId"]);


                    EstateAttachment attachment = EstateAttachment.GetById(estateAttachmentId);
                    if (attachment != null)
                    {
                        attachment.Description_cs = descripiton_cs;
                        attachment.Description_en = descripiton_en;
                        attachment.Update();
                    }


                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }

        public void DeleteEstateAttachment()
        {
            try
            {
                CancelView();
                if (!String.IsNullOrEmpty(Request.QueryString["Id"]) && !String.IsNullOrEmpty(Request.QueryString["EstateId"]))
                {
                    int id = int.Parse(Request.QueryString["Id"]);
                    int estateId = int.Parse(Request.QueryString["EstateId"]);

                    EstateAttachment attachment = EstateAttachment.GetById(id);
                    attachment.Delete();
                    attachment.DeleteFile();

                    Hashtable pars = new Hashtable();
                    pars.Add("id", estateId.ToString());
                    pars.Add("CurrentStep", "4");
                    pars.Add("OriginalAction", Request.Form["OriginalAction"]);
                    Redirect(this.Name, "edit", pars);
                }
                else
                {
                    Redirect(this.Name, "list");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex + "<br>");
            }
        }
    }
}
