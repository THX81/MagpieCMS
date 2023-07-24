using System;
using System.IO;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.Models
{
    [ActiveRecord]
    public class Page : ActiveRecordBase<Page>, ISiteNode
    {
        private String name;
        private String title;

        public Page()
        {
            this.Created = DateTime.Now;
        }

        public Page(Section section, String title, String name, String contents)
            : this()
        {
            this.Section = section;
            this.title = title;
            this.name = name;
            this.Contents = contents;
        }

        [PropertyLocalization("cs", "Klíèová slova")]
        [PropertyLocalization("en", "Keywords")]
        [Property("Keywords")]
        public String Keywords { get; set; }

        [PropertyLocalization("cs", "Popis")]
        [PropertyLocalization("en", "Description")]
        [Property("Description")]
        public String Description { get; set; }

        public String FileName
        {
            get { return this.name + ".aspx"; }
        }

        [PropertyLocalization("cs", "Obsah")]
        [PropertyLocalization("en", "Content")]
        [FormGeneratorBehavior(FormControlType.WYSIWYG)]
        [Property(ColumnType = "StringClob", SqlType = "nvarchar(MAX)")]
        public String Contents { get; set; }

        [PropertyLocalization("cs", "Sekce")]
        [PropertyLocalization("en", "Section")]
        [BelongsTo("SectionId")]
        public Section Section { get; set; }

        [PropertyLocalization("cs", "Propojení na jinou stránku")]
        [PropertyLocalization("en", "Connection to another page")]
        [Property("ConnectedPageId")]
        public Int32 ConnectedPageId { get; set; }

        [PropertyLocalization("cs", "Šablona stránky")]
        [PropertyLocalization("en", "Page template")]
        [BelongsTo("PageTemplateId")]
        public PageTemplate PageTemplate { get; set; }

        #region ISiteNode Members

        [PropertyLocalization("cs", "Id")]
        [PrimaryKey]
        public int Id { get; set; }

        [PropertyLocalization("cs", "Titulek stránky")]
        [Property("Title")]
        public String Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        [PropertyLocalization("cs", "Název v url")]
        [Property("Name")]
        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        [PropertyLocalization("cs", "Kultura (lokalizace) stránky")]
        [BelongsTo("CultureId")]
        public Culture Culture { get; set; }

        [PropertyLocalization("cs", "Priorita øazení")]
        [Property("OrderPriority")]
        public Int32 OrderPriority { get; set; }

        [PropertyLocalization("cs", "Datum vytvoøení")]
        [Property("Created")]
        public DateTime Created { get; set; }

        [PropertyLocalization("cs", "Publikovat")]
        [Property("Published")]
        public bool Published { get; set; }

        public String GetUrl(bool cached)
        {
            return GetUrl(this, cached);
        }

        #endregion

        public Page GetConnectedPage(bool cached)
        {
            String cacheKeyData = "Page.GetConnectedPage" + this.Id + "_" + this.ConnectedPageId + "_cached_data";
            Page result = null;

            if (cached && CacheHelper.Get<Page>(cacheKeyData) != null)
            {
                result = CacheHelper.Get<Page>(cacheKeyData);
            }

            if (result == null)
            {
                result = FindByPrimaryKey(this.ConnectedPageId, false);

                if (result != null && cached)
                {
                    CacheHelper.Set(cacheKeyData, result);
                }
            }
            return result;
        }

        public void SetConnectedPage(Page page)
        {
            if (page != null)
            {
                this.ConnectedPageId = page.Id;
                this.Save();

                page.ConnectedPageId = this.Id;
                page.Save();
            }
        }

        public Culture GetEnsuredCulture(bool cached)
        {
            String cacheKeyData = "Page.GetEnsuredCulture" + this.Id + "_cached_data";
            Culture result = null;
            if (cached && CacheHelper.Get<Culture>(cacheKeyData) != null)
            {
                result = CacheHelper.Get<Culture>(cacheKeyData);
            }

            if (result == null)
            {
                result = this.Culture;
                if (result == null && this.Section != null)
                {
                    result = this.Section.GetEnsuredCulture(cached);
                }
                if (result == null)
                {
                    result = Culture.GetDefaultCulture();
                }

                if (result != null && cached)
                {
                    CacheHelper.Set(cacheKeyData, result);
                }
            }

            return result;
        }

        public static String GetUrl(Page page, bool cached)
        {
            if (page.Section == null)
            {
                return ("/" + page.FileName);
            }
            else
            {
                return (page.Section.GetUrl(cached) + "/" + page.FileName);
            }
        }

        public static Page GetPageById(int id)
        {
            return FindByPrimaryKey(id, false);
        }

        public static Page GetPageByUrl(String url, bool cached)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }

            String cacheKeyData = "Page.GetPageByUrl" + url + "_" + cached + "_cached_data";
            Page result = null;
            if (cached && CacheHelper.Get<Page>(cacheKeyData) != null)
            {
                result = CacheHelper.Get<Page>(cacheKeyData);
            }

            if (result == null)
            {
                Section section = Section.GetSectionByUrl(url, cached);
                string[] parsedUrl = url.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                string pagename = url.Substring(url.LastIndexOf('/') + 1);

                if (section != null)
                {
                    foreach (Page p in section.Pages)
                    {
                        if (p.Published && p.Name.ToLower() == Path.GetFileNameWithoutExtension(pagename).ToLower())
                        {
                            result = p;
                            break;
                        }
                    }
                }
                else if (parsedUrl.Length == 1)
                {
                    Page[] pages =
                        new SimpleQuery<Page>(
                            "from Page p where p.Section is null and p.Published = 1 and p.Name like ?",
                            Path.GetFileNameWithoutExtension(pagename)).Execute();
                    if (pages.Length > 0)
                    {
                        result = pages[0];
                    }
                }


                if (result != null && cached)
                {
                    CacheHelper.Set(cacheKeyData, result);
                }
            }

            return result;
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.title))
            {
                return this.title;
            }

            return base.ToString();
        }

        public override void Create()
        {
            base.Create();
            this.EnsureFile();
        }

        public override void CreateAndFlush()
        {
            base.CreateAndFlush();
            this.EnsureFile();
        }

        public override void Save()
        {
            base.Save();
            this.EnsureFile();
        }

        public override void SaveAndFlush()
        {
            base.SaveAndFlush();
            this.EnsureFile();
        }

        public override void Delete()
        {
            base.Delete();
            this.DeleteFile();
        }

        public override void DeleteAndFlush()
        {
            base.DeleteAndFlush();
            this.DeleteFile();
        }


        private void EnsureFile()
        {
            string url = this.GetUrl(false);
            if (String.IsNullOrEmpty(url))
            {
                return;
            }

            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(url);
                if (File.Exists(fullPath))
                {
                    return;
                }

                StreamWriter writer = File.CreateText(fullPath);
                writer.Write("<!-- aby IIS nehlasilo 404 -->");
                writer.Close();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex);
            }
        }

        private void DeleteFile()
        {
            string url = this.GetUrl(false);
            if (String.IsNullOrEmpty(url))
            {
                return;
            }

            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(url);
                if (!File.Exists(fullPath))
                {
                    return;
                }

                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex);
            }
        }
    }
}