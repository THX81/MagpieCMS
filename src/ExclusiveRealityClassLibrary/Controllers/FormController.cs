using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using Castle.Components.Common.EmailSender;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models;

namespace ExclusiveReality.Controllers
{
    public class FormController : BaseController
    {
        public void ProcessEstateMessage()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["returnurl"]) &&
                !String.IsNullOrEmpty(Request.QueryString["messagetype"]))
            {
                bool result = false;
                string errMessage = "";
                string redirectUrl = Request.QueryString["returnurl"];
                string redirectUrlPostfix = "";
                try
                {
                    if (Regex.IsMatch(Request.QueryString["messagetype"], "^sendfriend$", RegexOptions.IgnoreCase))
                    {
                        redirectUrl += "?detail=" + Request.QueryString["detail"]
                                       + "&email=" + Request.QueryString["email"]
                                       + "&messagetype=";
                        redirectUrlPostfix = "#bottom";

                        if (ValidationHelper.IsEmail(Request.QueryString["email"]))
                        {
                            var sb = new StringBuilder();
                            sb.Append("Zpráva vygenerována ze stránek www.company.cz" + Environment.NewLine);
                            sb.Append("" + Environment.NewLine);
                            sb.Append("http://www.company.cz" + Request.QueryString["returnurl"] + "?detail=" +
                                      Request.QueryString["detail"] + Environment.NewLine);

                            var mm = new Message(
                                ConfigurationManager.AppSettings["OutEmail"],
                                Request.QueryString["email"],
                                "Zpráva vygenerována ze stránek www.company.cz",
                                sb.ToString()) {Format = Format.Text, Encoding = Encoding.UTF8};
                            DeliverEmail(mm);


                            result = true;
                        }
                        else
                        {
                            errMessage = "email";
                        }
                    }
                    else if (Regex.IsMatch(Request.QueryString["messagetype"], "^contactus$", RegexOptions.IgnoreCase))
                    {
                        redirectUrl += "?detail=" + Request.QueryString["detail"]
                                       + "&email=" + Request.QueryString["email"]
                                       + "&name=" + Request.QueryString["name"]
                                       + "&phone=" + Request.QueryString["phone"]
                                       + "&comment=" + System.Web.HttpContext.Current.Server.UrlEncode(Request.QueryString["comment"])
                                       + "&messagetype=";
                        redirectUrlPostfix = "#bottom";

                        if (ValidationHelper.IsEmail(Request.QueryString["email"])
                            || !String.IsNullOrEmpty(Request.QueryString["phone"]))
                        {
                            var sb = new StringBuilder();
                            sb.Append("Zpráva vygenerována ze stránek www.company.cz" + Environment.NewLine);
                            sb.Append("" + Environment.NewLine);
                            sb.Append("Cislo nemovitosti: #" + Request.QueryString["ordernumber"] +
                                      " http://www.company.cz" + Request.QueryString["returnurl"] + "?detail=" +
                                      Request.QueryString["detail"] + Environment.NewLine);
                            sb.Append("Kontakt:" + Environment.NewLine);
                            sb.Append(Request.QueryString["name"] + Environment.NewLine);
                            sb.Append(Request.QueryString["phone"] + Environment.NewLine);
                            sb.Append(Request.QueryString["email"] + Environment.NewLine);
                            sb.Append("Poznámka: "+ Environment.NewLine + Request.QueryString["comment"] + Environment.NewLine);


                            var mm = new Message(
                                ConfigurationManager.AppSettings["OutEmail"],
                                ConfigurationManager.AppSettings["EstateMessagesDeliveryEmail"],
                                "Zajem o nemovitost - zpráva ze stránek www.company.cz",
                                sb.ToString()) { Format = Format.Text, Encoding = Encoding.UTF8 };
                            DeliverEmail(mm);


                            result = true;
                        }
                        else if (String.IsNullOrEmpty(Request.QueryString["email"]) &&
                                 String.IsNullOrEmpty(Request.QueryString["phone"]))
                        {
                            errMessage = "email_phone_empty";
                        }
                        else if (!ValidationHelper.IsEmail(Request.QueryString["email"]))
                        {
                            errMessage = "email";
                        }
                    }
                    else if (Regex.IsMatch(Request.QueryString["messagetype"], "^contactusproject$", RegexOptions.IgnoreCase))
                    {
                        redirectUrl += "?detail=" + Request.QueryString["detail"]
                                       + "&email=" + Request.QueryString["email"]
                                       + "&name=" + Request.QueryString["name"]
                                       + "&phone=" + Request.QueryString["phone"]
                                       + "&comment=" + System.Web.HttpContext.Current.Server.UrlEncode(Request.QueryString["comment"])
                                       + "&messagetype=";
                        redirectUrlPostfix = "#bottom";

                        if (ValidationHelper.IsEmail(Request.QueryString["email"])
                            || !String.IsNullOrEmpty(Request.QueryString["phone"]))
                        {
                            var sb = new StringBuilder();
                            sb.Append("Zpráva vygenerována ze stránek www.company.cz" + Environment.NewLine);
                            sb.Append("" + Environment.NewLine);
                            sb.Append("Název projektu: #" + Request.QueryString["projectname"] +
                                      " http://www.company.cz" + Request.QueryString["returnurl"] + "?detail=" +
                                      Request.QueryString["detail"] + Environment.NewLine);
                            sb.Append("Kontakt:" + Environment.NewLine);
                            sb.Append(Request.QueryString["name"] + Environment.NewLine);
                            sb.Append(Request.QueryString["phone"] + Environment.NewLine);
                            sb.Append(Request.QueryString["email"] + Environment.NewLine);
                            sb.Append("Poznámka: "+ Environment.NewLine + Request.QueryString["comment"] + Environment.NewLine);


                            var mm = new Message(
                                ConfigurationManager.AppSettings["OutEmail"],
                                ConfigurationManager.AppSettings["EstateMessagesDeliveryEmail"],
                                "Zajem o projekt - zpráva ze stránek www.company.cz",
                                sb.ToString()) { Format = Format.Text, Encoding = Encoding.UTF8 };
                            DeliverEmail(mm);


                            result = true;
                        }
                        else if (String.IsNullOrEmpty(Request.QueryString["email"]) &&
                                 String.IsNullOrEmpty(Request.QueryString["phone"]))
                        {
                            errMessage = "email_phone_empty";
                        }
                        else if (!ValidationHelper.IsEmail(Request.QueryString["email"]))
                        {
                            errMessage = "email";
                        }
                    }
                    else if (Regex.IsMatch(Request.QueryString["messagetype"], "^insertoffer$", RegexOptions.IgnoreCase)
                             ||
                             Regex.IsMatch(Request.QueryString["messagetype"], "^insertdemand$", RegexOptions.IgnoreCase))
                    {
                        redirectUrl += "?messagetype=";

                        var message = new EstateMessage();
                        if (Request.QueryString["messagetype"] == "insertoffer") message.MessageType = "Nabídka";
                        else if (Request.QueryString["messagetype"] == "insertdemand") message.MessageType = "Poptávka";
                        message.EstateType = Request.QueryString["estatetype"];
                        message.EstateOfferType = Request.QueryString["estateoffertype"];
                        for (int x = 0; x <= 10; x++)
                            if (!String.IsNullOrEmpty(Request.QueryString["ps" + x]))
                                message.Ps += Request.QueryString["ps" + x] + ", ";
                        message.PriceFrom = Request.QueryString["pricefrom"];
                        message.PriceTo = Request.QueryString["priceto"];
                        message.Description = Request.QueryString["description"];
                        message.FirstName = Request.QueryString["firstname"];
                        message.LastName = Request.QueryString["lastname"];
                        message.Phone = Request.QueryString["phone"];
                        message.Email = (Regex.IsMatch(Request.QueryString["email"],
                                                       @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
                                                       RegexOptions.IgnoreCase)
                                             ? Request.QueryString["email"]
                                             : "info@company.cz");

                        message.Create();


                        var sb = new StringBuilder();
                        sb.Append(message.MessageType + " vygenerována ze stránek www.company.cz" + Environment.NewLine);
                        sb.Append("" + Environment.NewLine);
                        sb.Append("== " + message.MessageType + " nemovitosti ==" + Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Typ nemovitosti: " + message.EstateType + "/" + message.EstateOfferType + "" +
                                  Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Velikost nemovitosti: " + message.Ps + "" + Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Cena nemovitosti:" + message.PriceFrom + " - " + message.PriceTo +
                                  Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Popis nemovitosti:" + Environment.NewLine);
                        sb.Append(message.Description + Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("" + Environment.NewLine);
                        sb.Append("== Kontaktní adresa: ==" + Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Jméno a pøíjmení: " + message.FirstName + " " + message.LastName + "" +
                                  Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Telefon: " + message.Phone + "" + Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);
                        sb.Append("Mail: " + message.Email + "" + Environment.NewLine);
                        sb.Append("----------------------------------------" + Environment.NewLine);

                        var mm = new Message(message.Email,
                                             ConfigurationManager.AppSettings["EstateMessagesDeliveryEmail"],
                                             message.MessageType + " vygenerována ze stránek www.company.cz",
                                             sb.ToString()) {Format = Format.Text, Encoding = Encoding.UTF8};
                        DeliverEmail(mm);

                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    errMessage = ex.ToString();
                }

                CancelView();

                redirectUrl = redirectUrl + Request.QueryString["messagetype"] + "&result=" +
                              result.ToString().ToLower() + "&error=" + errMessage + redirectUrlPostfix;

                Response.Write("<html><head><meta http-equiv=\"refresh\" content=\"0;URL=" + redirectUrl + "\"></head>");
                Response.Write("<body>");
                Response.Write("<script type=\"text/javascript\">window.location=\"" + redirectUrl + "\";</script>");
                Response.Write("</body></html>");
                Response.Redirect(redirectUrl, true);
            }
            else
                Redirect("/index.aspx");
        }
    }
}
