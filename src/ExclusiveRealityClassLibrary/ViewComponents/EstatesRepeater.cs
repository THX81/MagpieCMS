using System;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Text.RegularExpressions;

namespace ExclusiveReality.ViewComponents
{
    public class EstatesRepeater : ViewComponent
    {
        private int estateTypeId;
        private int estateOfferTypeId;
        private int saled;
        private int rented;
        private int foreignProperty;

        public EstatesRepeater()
        {
            estateTypeId = 0;
            estateOfferTypeId = 0;
            saled = 0;
            rented = 0;
            foreignProperty = 0;
        }

        public override void Initialize()
        {
            var estateTypeIdTmp = ComponentParams["EstateTypeId"] as string;
            if (!String.IsNullOrEmpty(estateTypeIdTmp) &&
                Regex.IsMatch(estateTypeIdTmp, "^[0-9]+$", RegexOptions.IgnoreCase))
                estateTypeId = int.Parse(estateTypeIdTmp);

            var estateOfferTypeIdTmp = ComponentParams["EstateOfferTypeId"] as string;
            if (!String.IsNullOrEmpty(estateOfferTypeIdTmp) &&
                Regex.IsMatch(estateOfferTypeIdTmp, "^[0-9]+$", RegexOptions.IgnoreCase))
                estateOfferTypeId = int.Parse(estateOfferTypeIdTmp);

            var saledTmp = ComponentParams["Saled"] as string;
            if (!String.IsNullOrEmpty(saledTmp) && Regex.IsMatch(saledTmp, "^[0-9]+$", RegexOptions.IgnoreCase))
                saled = int.Parse(saledTmp);

            var rentedTmp = ComponentParams["Rented"] as string;
            if (!String.IsNullOrEmpty(rentedTmp) && Regex.IsMatch(rentedTmp, "^[0-9]+$", RegexOptions.IgnoreCase))
                rented = int.Parse(rentedTmp);

            var foreignPropertyTmp = ComponentParams["ForeignProperty"] as string;
            if (!String.IsNullOrEmpty(foreignPropertyTmp) && Regex.IsMatch(foreignPropertyTmp, "^[0-9]+$", RegexOptions.IgnoreCase))
                foreignProperty = int.Parse(foreignPropertyTmp);
            base.Initialize();
        }


        public override void Render()
        {
            #region databinding for detail page

            if (String.IsNullOrEmpty(Request.QueryString["detail"]))
            {
                EstateType[] estateTypes =
                    new SimpleQuery<EstateType>("from EstateType s" +
                                                (estateTypeId > 0 ? " where s.Id=" + estateTypeId : "")).Execute();
                PropertyBag["EstateTypes"] = estateTypes;
                EstateOfferType[] estateOfferTypes =
                    new SimpleQuery<EstateOfferType>("from EstateOfferType s" +
                                                     (estateOfferTypeId > 0 ? " where s.Id=" + estateOfferTypeId : "")).
                        Execute();
                PropertyBag["EstateOfferTypes"] = estateOfferTypes;
            }

            #endregion

            Estate[] estates = null;

            #region databinding for estate repeater

            if (Request.QueryString["originalurl"] != "/index.aspx" &&
                Request.QueryString["originalurl"] != "/Index.aspx" && Request.QueryString["originalurl"] != "/" &&
                !String.IsNullOrEmpty(Request.QueryString["originalurl"])
                && Request.QueryString["originalurl"] != "/en/index.aspx" &&
                Request.QueryString["originalurl"] != "/en/Index.aspx" && Request.QueryString["originalurl"] != "/en/")
            {
                var sbEstatesSql = new StringBuilder("from Estate e where e.Publish = 1 and e.Saled=" + saled + " and e.Rented=" + rented + " and e.ForeignProperty=" + foreignProperty + 
                                                (estateTypeId > 0 ? " and e.EstateType=" + estateTypeId : "") +
                                                (estateOfferTypeId > 0 ? " and e.EstateOfferType=" + estateOfferTypeId : ""));
                //sbEstatesSql.Append(" and e.DeveloperProject is null");
                
                ExclusiveReality.Helpers.Logger.Debug(System.Reflection.MethodBase.GetCurrentMethod(), sbEstatesSql.ToString());

                if (!String.IsNullOrEmpty(Request.QueryString["searching"]))
                {
                    #region searching

                    sbEstatesSql =
                        new StringBuilder("from Estate e where e.Publish = 1 and e.Saled=" + saled + " and e.Rented=" + rented + " and e.ForeignProperty=" + foreignProperty);
                    sbEstatesSql.Append((String.IsNullOrEmpty(Request.QueryString["EstateType"]) ? "" : " and e.EstateType=" + Request.QueryString["EstateType"]));
                    sbEstatesSql.Append((String.IsNullOrEmpty(Request.QueryString["EstateOfferType"]) ? "" : " and e.EstateOfferType=" + Request.QueryString["EstateOfferType"]));
                    //sbEstatesSql.Append(" and e.DeveloperProject is null");

                    if (!String.IsNullOrEmpty(Request.QueryString["ps_01"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_02"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_03"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_04"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_05"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_06"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_07"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_08"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_09"])
                        || !String.IsNullOrEmpty(Request.QueryString["ps_10"]))
                    {
                        var sbPSSql = new StringBuilder();

                        if (String.IsNullOrEmpty(Request.QueryString["ps_10"]))
                        {
                            sbPSSql.Append(" and (");
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_01"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%garsoniéra%' or e.EstateProperties.FlatSize like '%garsoniéra%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_02"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%1+kk%' or e.EstateProperties.FlatSize like '%1+kk%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_03"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%1+1%' or e.EstateProperties.FlatSize like '%1+1%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_04"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%2+kk%' or e.EstateProperties.FlatSize like '%2+kk%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_05"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%2+1%' or e.EstateProperties.FlatSize like '%2+1%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_06"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%3+kk%' or e.EstateProperties.FlatSize like '%3+kk%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_07"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%3+1%' or e.EstateProperties.FlatSize like '%3+1%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_08"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%4+kk%' or e.EstateProperties.FlatSize like '%4+kk%'"));
                            sbPSSql.Append((String.IsNullOrEmpty(Request.QueryString["ps_09"])
                                                ? ""
                                                : (sbPSSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateProperties.Disposition like '%4+1%' or e.EstateProperties.FlatSize like '%4+1%'"));
                            sbPSSql.Append(")");
                        }
                        else
                        {
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_01"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%garsoniéra%' and e.EstateProperties.FlatSize not like '%garsoniéra%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_02"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%1+kk%' and e.EstateProperties.FlatSize not like '%1+kk%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_03"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%1+1%' and e.EstateProperties.FlatSize not like '%1+1%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_04"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%2+kk%' and e.EstateProperties.FlatSize not like '%2+kk%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_05"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%2+1%' and e.EstateProperties.FlatSize not like '%2+1%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_06"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%3+kk%' and e.EstateProperties.FlatSize not like '%3+kk%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_07"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%3+1%' and e.EstateProperties.FlatSize not like '%3+1%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_08"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%4+kk%' and e.EstateProperties.FlatSize not like '%4+kk%'"));
                            sbPSSql.Append((!String.IsNullOrEmpty(Request.QueryString["ps_09"])
                                                ? ""
                                                : " and e.EstateProperties.Disposition not like '%4+1%' and e.EstateProperties.FlatSize not like '%4+1%'"));
                        }

                        sbEstatesSql.Append(sbPSSql.ToString());
                    }

                    sbEstatesSql.Append((String.IsNullOrEmpty(Request.QueryString["priceFrom"])
                                             ? ""
                                             : " and e.EstatePriceInfo.PriceValue>=" + Request.QueryString["priceFrom"]));
                    sbEstatesSql.Append((String.IsNullOrEmpty(Request.QueryString["priceTo"])
                                             ? ""
                                             : " and e.EstatePriceInfo.PriceValue<=" + Request.QueryString["priceTo"]));

                    if (!String.IsNullOrEmpty(Request.QueryString["pl_01"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_02"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_03"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_04"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_05"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_06"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_07"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_08"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_09"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_10"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_11"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_12"])
                        || !String.IsNullOrEmpty(Request.QueryString["pl_13"]))
                    {
                        var sbPLSql = new StringBuilder();

                        if (String.IsNullOrEmpty(Request.QueryString["pl_13"]))
                        {
                            sbPLSql.Append(" and (");
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_01"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 1%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_02"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 2%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_03"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 3%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_04"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 4%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_05"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 5%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_06"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 6%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_07"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 7%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_08"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 8%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_09"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 9%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_10"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha 10%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_11"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha východ%'"));
                            sbPLSql.Append((String.IsNullOrEmpty(Request.QueryString["pl_12"])
                                                ? ""
                                                : (sbPLSql.Length <= 6 ? "" : " or ") +
                                                  "e.EstateAddressInfo.City like '%Praha západ%'"));
                            sbPLSql.Append(")");
                        }
                        else
                        {
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_01"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 1%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_02"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 2%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_03"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 3%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_04"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 4%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_05"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 5%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_06"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 6%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_07"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 7%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_08"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 8%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_09"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 9%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_10"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha 10%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_11"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha východ%'"));
                            sbPLSql.Append((!String.IsNullOrEmpty(Request.QueryString["pl_12"])
                                                ? ""
                                                : " and e.EstateAddressInfo.City not like '%Praha západ%'"));
                        }

                        sbEstatesSql.Append(sbPLSql.ToString());
                    }

                    sbEstatesSql.Append((String.IsNullOrEmpty(Request.QueryString["catNum"])
                                             ? ""
                                             : " and UPPER(e.EstateExtendedInfo.OrderNumber) like '%" + Request.QueryString["catNum"].ToUpper().Replace("'", "") + "%'"));

                    #endregion

                    try
                    {
                        PropertyBag["PagerHtml"] = GetPagerHtml(GetEstatesCount(sbEstatesSql.ToString()));

                        // razeni od nejnovejsich
                        sbEstatesSql.Append(" order by e.Created desc");

                        ExclusiveReality.Helpers.Logger.Debug(System.Reflection.MethodBase.GetCurrentMethod(), sbEstatesSql.ToString());

                        estates = GetPagedEstates(sbEstatesSql.ToString());
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex + "<br>");
                    }
                }
                else
                {
                    string cacheKey = "EstatesRepeater_" + estateTypeId + "_" + estateOfferTypeId + "_" + rented + "_" + saled + "_" + foreignProperty + "_" +
                                      Request.QueryString["page"] + "_" +
                                      Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                    estates = Helpers.CacheHelper.Get<Estate[]>(cacheKey);

                    PropertyBag["PagerHtml"] = GetPagerHtml(GetEstatesCount(sbEstatesSql.ToString()));
                    if (estates == null || estates.Length == 0)
                    {
                        try
                        {
                            // razeni od nejnovejsich
                            sbEstatesSql.Append(" order by e.Created desc");

                            estates = GetPagedEstates(sbEstatesSql.ToString());
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex + "<br>");
                        }

                        if (estates != null)
                            if (estates.Length > 0)
                                Helpers.CacheHelper.Set(cacheKey, estates);
                    }
                }
            }

            #endregion

            PropertyBag["Estates"] = estates;


            if (!String.IsNullOrEmpty(Request.QueryString["detail"])
                && Regex.IsMatch(Request.QueryString["detail"], "^[0-9]+$", RegexOptions.IgnoreCase))
            {
                string cacheKey1 = "EstatesRepeater_" + Request.QueryString["detail"] + "_" + estateTypeId + "_" +
                                   estateOfferTypeId + "_" +
                                   Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
                var estate = Helpers.CacheHelper.Get<Estate[]>(cacheKey1);

                if (estate == null || estate.Length == 0)
                {
                    estate =
                        new SimpleQuery<Estate>("from Estate e where e.Publish = 1 and e.Id=" + Request.QueryString["detail"] +
                                                (estateTypeId > 0 ? " and e.EstateType=" + estateTypeId : "") +
                                                (estateOfferTypeId > 0 ? " and e.EstateOfferType=" + estateOfferTypeId : "")).Execute();

                    if (estate.Length > 0)
                        Helpers.CacheHelper.Set(cacheKey1, estate);
                }
                if (estate.Length > 0)
                    PropertyBag["SelectedEstate"] = estate[0];
            }

            base.Render();
        }

        private Estate[] GetPagedEstates(string sql)
        {
            var rowsPage = 0;
            const int maxRows = 8;

            if (!String.IsNullOrEmpty(Request.QueryString["page"]) &&
                Regex.IsMatch(Request.QueryString["page"], "^[0-9]+$", RegexOptions.IgnoreCase))
                rowsPage = int.Parse(Request.QueryString["page"]);

            var sqEstates = new SimpleQuery<Estate>(sql);
            sqEstates.SetQueryRange(rowsPage*maxRows, maxRows);
            return sqEstates.Execute();
        }

        private static int GetEstatesCount(string sql)
        {
            var sqEstates = new SimpleQuery<int>(typeof (Estate), "select e.Id " + sql);
            return sqEstates.Execute().Length;
        }

        private string GetPagerHtml(int totalCount)
        {
            const decimal maxRows = 8;
            int actualPage = (!String.IsNullOrEmpty(Request.QueryString["page"]) &&
                              Regex.IsMatch(Request.QueryString["page"], "^[0-9]+$", RegexOptions.IgnoreCase)
                                  ? int.Parse(Request.QueryString["page"])
                                  : 0);

            decimal pages = (decimal) totalCount/maxRows;

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