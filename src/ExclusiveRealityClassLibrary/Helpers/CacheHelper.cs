using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Configuration;
using DiskOutputCache;

namespace ExclusiveReality.Helpers
{
    public static class CacheHelper
    {
        public static void Set(string cacheKey, object value)
        {
            int defaultMinutes = 10;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheHelper.CacheExpiration"]))
            {
                defaultMinutes = int.Parse(ConfigurationManager.AppSettings["CacheHelper.CacheExpiration"]);
            }

            HttpRuntime.Cache.Insert(cacheKey, value, null, DateTime.Now.AddMinutes(defaultMinutes), TimeSpan.Zero);
        }

        public static T Get<T>(string cacheKey)
        {
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                return (T) HttpRuntime.Cache[cacheKey];
            }
            
            return default(T);
        }


        public static void SaveImage(byte[] data, string fileName)
        {
            string path = HttpContext.Current.Server.MapPath("/imgcache");
            FileStream file = null;

            var interval = new TimeSpan(0, 10, 0);
            var config = (DiskOutputCacheSettingsSection)WebConfigurationManager.GetWebApplicationSection("diskOutputCacheSettings");
            if (config != null)
            {
                interval = config.ImagesCacheDuration;
            }

            try
            {
                file = File.Create(Path.Combine(path, fileName));
                file.Write(data, 0, data.Length);

                // vycistime stare obrazky
                foreach (string file1 in Directory.GetFiles(path))
                {
                    if (File.GetCreationTime(file1).Add(interval) < DateTime.Now)
                    {
                        File.Delete(file1);
                    }
                }
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file.Dispose();
                }
            }
        }

        public static byte[] GetImage(string fileName)
        {
            string path = HttpContext.Current.Server.MapPath("/imgcache");

            var interval = new TimeSpan(0, 10, 0);
            var config = (DiskOutputCacheSettingsSection)WebConfigurationManager.GetWebApplicationSection("diskOutputCacheSettings");
            if (config != null)
            {
                interval = config.ImagesCacheDuration;
            }


            foreach (string file in Directory.GetFiles(path))
            {
                if (Path.GetFileName(file) == fileName)
                {
                    try
                    {
                        if (File.GetCreationTime(file).Add(interval) > DateTime.Now)
                        {
                            return File.ReadAllBytes(file);
                        }
                    }
                    catch(Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
                }
            }
            return null;
        }
    }
}