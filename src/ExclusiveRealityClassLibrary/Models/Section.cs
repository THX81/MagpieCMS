using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using ExclusiveReality.Helpers;
using ExclusiveReality.Models.Attributes;
using ExclusiveReality.Models.Base;
using NHibernate.Expression;

namespace ExclusiveReality.Models
{
    [ActiveRecord]
    public class Section : ActiveRecordBase<Section>, ISiteNode
    {
        private bool createIndexPage;
        private IList<Page> pages = new List<Page>();
        private IList<Section> sections = new List<Section>();
        private String title;

        public Section()
        {
            this.Created = DateTime.Now;
            this.createIndexPage = true;
        }

        public Section(Section parentSection, String title, String name)
            : this()
        {
            this.ParentSection = parentSection;
            this.title = title;
            this.Name = name;
        }

        [PropertyLocalization("cs", "Nadøazená sekce")]
        [PropertyLocalization("en", "Parent section")]
        [BelongsTo("ParentSectionId")]
        public Section ParentSection { get; set; }

        [PropertyLocalization("cs", "Pod-sekce")]
        [PropertyLocalization("en", "Child section")]
        [HasMany(
            Table = "Sections", ColumnKey = "ParentSectionId", OrderBy = "OrderPriority ASC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<Section> Sections
        {
            get { return this.sections; }
            set { this.sections = value; }
        }

        [PropertyLocalization("cs", "Stránky")]
        [PropertyLocalization("en", "Pages")]
        [HasMany(
            Table = "Pages", ColumnKey = "SectionId", OrderBy = "OrderPriority ASC",
            Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
        public IList<Page> Pages
        {
            get { return this.pages; }
            set { this.pages = value; }
        }

        internal bool CreateIndexPage
        {
            get { return this.createIndexPage; }
            set { this.createIndexPage = value; }
        }

        #region ISiteNode Members

        [PropertyLocalization("cs", "Id")]
        [PrimaryKey]
        public int Id { get; set; }

        [PropertyLocalization("cs", "Titulek sekce")]
        [Property("Title")]
        public String Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        [PropertyLocalization("cs", "Název v url")]
        [Property("Name")]
        public String Name { get; set; }

        [PropertyLocalization("cs", "Kultura (lokalizace) stránky")]
        [BelongsTo("CultureId")]
        public Culture Culture { get; set; }

        [PropertyLocalization("cs", "Priorita øazení")]
        [Property("OrderPriority")]
        public Int32 OrderPriority { get; set; }

        [PropertyLocalization("cs", "Datum vytvoøení")]
        [Property("Created")]
        public DateTime Created { get; set; }

        [PropertyLocalization("cs", "Publikovat (nepouzivat)")]
        [Property("Published")]
        public bool Published { get; set; }

        public String GetUrl(bool cached)
        {
            return GetUrl(this, cached);
        }

        #endregion

        public Culture GetEnsuredCulture(bool cached)
        {
            String cacheKeyData = "Section.GetEnsuredCulture" + this.Id + "_cached_data";
            Culture result = null;
            if (cached && CacheHelper.Get<Culture>(cacheKeyData) != null)
            {
                result = CacheHelper.Get<Culture>(cacheKeyData);
            }

            if (result == null)
            {
                result = this.Culture;
                if (result == null && this.ParentSection != null)
                {
                    result = this.ParentSection.GetEnsuredCulture(cached);
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

        public static String GetUrl(Section section, bool cached)
        {
            if (section == null)
            {
                return String.Empty;
            }

            if (HttpContext.Current == null)
            {
                return GetUrlInternal(section);
            }
            else
            {
                String cacheKeyData = "Section.GetUrl" + section.Id + "_cached_data";
                String result = String.Empty;
                if (cached && CacheHelper.Get<String>(cacheKeyData) != null)
                {
                    result = CacheHelper.Get<String>(cacheKeyData);
                }

                if (result == null || String.IsNullOrEmpty(result))
                {
                    result = GetUrlInternal(section);

                    if (result != null && !String.IsNullOrEmpty(result) && cached)
                    {
                        CacheHelper.Set(cacheKeyData, result);
                    }
                }
                return result;
            }
        }

        private static String GetUrlInternal(Section section)
        {
            String result = String.Empty;
            if (section != null)
            {
                result = (section.ParentSection != null ? GetUrlInternal(section.ParentSection) : String.Empty)
                         + ((String.IsNullOrEmpty(section.Name) ? String.Empty : "/" + section.Name));
            }

            return result;
        }

        public static Section GetSectionByUrl(String url, bool cached)
        {
            if (String.IsNullOrEmpty(url))
            {
                return null;
            }

            String cacheKeyData = "Section.GetSectionByUrl" + url + "_" + cached + "_cached_data";
            Section result = null;
            if (cached && CacheHelper.Get<Section>(cacheKeyData) != null)
            {
                result = CacheHelper.Get<Section>(cacheKeyData);
            }

            if (result == null)
            {
                foreach (String section in url.Split('/'))
                {
                    if (!section.EndsWith(".aspx"))
                    {
                        var s = new Section[0];
                        if (result == null)
                        {
                            s =
                                new SimpleQuery<Section>(
                                    "from Section s where s.ParentSection is null and s.Name like ?", section).Execute();
                        }
                        else
                        {
                            s =
                                new SimpleQuery<Section>("from Section s where s.ParentSection = ? and s.Name like ?",
                                                         result, section).Execute();
                        }

                        if (s.Length > 0)
                        {
                            result = s[0];
                        }

                        if (result == null)
                        {
                            break;
                        }
                    }
                }

                if (result != null && cached)
                {
                    CacheHelper.Set(cacheKeyData, result);
                }
            }
            return result;
        }

        public static Section GetSectionById(int id)
        {
            return FindByPrimaryKey(id, false);
        }

        public List<ISiteNode> GetSectionNodes(bool cached)
        {
            String cacheKeyData = "Section.GetSectionNodes" + this.Id + "_cached_data";
            List<ISiteNode> result = null;
            if (cached && CacheHelper.Get<List<ISiteNode>>(cacheKeyData) != null)
            {
                result = CacheHelper.Get<List<ISiteNode>>(cacheKeyData);
            }

            if (result == null)
            {
                IList<Page> pages = this.Pages;
                IList<Section> sections = this.Sections;
                result = new List<ISiteNode>(pages.Count + sections.Count);
                foreach (Section item in sections)
                {
                    result.Add(item);
                }
                foreach (Page item in pages)
                {
                    result.Add(item);
                }

                result.Sort(
                    delegate(ISiteNode s1, ISiteNode s2) { return s1.OrderPriority.CompareTo(s2.OrderPriority); });

                if (result != null && cached)
                {
                    if (result.Count > 0)
                    {
                        CacheHelper.Set(cacheKeyData, result);
                    }
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

            this.EnsureDirectory();

            if (this.createIndexPage)
            {
                this.EnsureIndexPage();
            }
        }

        public override void CreateAndFlush()
        {
            base.CreateAndFlush();

            this.EnsureDirectory();

            if (this.createIndexPage)
            {
                this.EnsureIndexPage();
            }
        }

        public override void Save()
        {
            base.Save();
            this.EnsureDirectory();
        }

        public override void SaveAndFlush()
        {
            base.SaveAndFlush();
            this.EnsureDirectory();
        }

        public override void Delete()
        {
            base.Delete();
            this.DeleteDirectory();
        }

        public override void DeleteAndFlush()
        {
            base.DeleteAndFlush();
            this.DeleteDirectory();
        }


        private void EnsureDirectory()
        {
            string url = this.GetUrl(false);
            if (String.IsNullOrEmpty(url))
            {
                return;
            }

            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(url);
                if (Directory.Exists(fullPath))
                {
                    return;
                }

                Directory.CreateDirectory(fullPath);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex);
            }
        }

        private void DeleteDirectory()
        {
            string url = this.GetUrl(false);
            if (String.IsNullOrEmpty(url))
            {
                return;
            }

            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(url);
                if (!Directory.Exists(fullPath))
                {
                    return;
                }

                Directory.Delete(fullPath);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex);
            }
        }

        private void EnsureIndexPage()
        {
            var indexPage = new Page(this, this.Title, "index", String.Empty);
            indexPage.Published = true;
            indexPage.OrderPriority = 0;
            indexPage.PageTemplate = ActiveRecordBase<PageTemplate>.FindOne(Expression.Like("Name", "indexpage"));
            indexPage.Create();
        }
    }
}