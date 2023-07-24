using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using ExclusiveReality.Models;
using ExclusiveReality.Models.Base;

namespace ExclusiveReality.HttpHandlers
{
    internal class EstateImageHttpHandler : IHttpHandler
    {
        #region IHttpHandler Members

        bool IHttpHandler.IsReusable
        {
            get { return false; }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            context.Response.Clear();

            string source = context.Request.QueryString["source"];
            string sizeTmp = context.Request.QueryString["size"];
            int size = (!String.IsNullOrEmpty(sizeTmp) && Regex.IsMatch(sizeTmp, "^[0-9]+$", RegexOptions.IgnoreCase)
                            ? int.Parse(context.Request.QueryString["size"])
                            : 1);
            if (size > 4)
            {
                size = 4;
            }
            if (size < 1)
            {
                size = 1;
            }
            string id = context.Request.QueryString["id"];
            if (!String.IsNullOrEmpty(id) && Regex.IsMatch(id, "^[0-9]+$", RegexOptions.IgnoreCase))
            {
                string fullFilePath = "";

                if (source == "developerproject")
                {
                    var image = BusinessObjectBase<DeveloperProjectImage>.GetById(int.Parse(id));
                    fullFilePath = image.FilePath;
                }
                else
                {
                    var image = BusinessObjectBase<EstateImage>.GetById(int.Parse(id));
                    fullFilePath = image.FilePath;
                }

                if (!String.IsNullOrEmpty(fullFilePath))
                {
                    fullFilePath = context.Server.MapPath(fullFilePath);
                    var data = new byte[0];
                    if (File.Exists(fullFilePath))
                    {
                        data = File.ReadAllBytes(fullFilePath);


                        if (data.Length > 0)
                        {
                            switch (size)
                            {
                                case 2:
                                    data = GetResizedImage(data, 220, 165);
                                    break;
                                case 3:
                                    data = GetResizedImage(data, 105, 79);
                                    break;
                                case 4:
                                    data = GetResizedImage(data, 120, 90);
                                    break;
                                case 5:
                                    data = GetResizedImage(data, 240, 180);
                                    break;
                                case 1:
                                default:
                                    data = GetResizedImage(data, 640, 480);
                                    break;
                            }


                            context.Response.ContentType = "Image/jpeg";
                            context.Response.OutputStream.Write(data, 0, data.Length);
                            context.Response.End();
                        }
                    }
                }
            }
        }

        #endregion

        public static byte[] GetResizedImage(byte[] data, int width, int height)
        {
            var ms = new MemoryStream(data);
            var imgIn = new Bitmap(ms);
            double y = imgIn.Height;
            double x = imgIn.Width;
            double factor = 1;
            if (width > 0)
            {
                factor = width/x;
            }
            else if (height > 0)
            {
                factor = height/y;
            }
            var outStream = new MemoryStream();
            var imgOut = new Bitmap((int) (x*factor), (int) (y*factor));
            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int) (factor*x), (int) (factor*y)),
                        new Rectangle(0, 0, (int) x, (int) y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, ImageFormat.Jpeg);
            return outStream.ToArray();
        }


        public static byte[] ApplyWatermark(byte[] data)
        {
            var ms = new MemoryStream(data);
            var imgIn = new Bitmap(ms);
            var outStream = new MemoryStream();

            Bitmap bmPhoto = null;
            Graphics grWatermark = null;
            Image imgWatermark = null;


            try
            {
                int orgWidth = imgIn.Width;
                int orgHeight = imgIn.Height;

                bmPhoto = new Bitmap(orgWidth, orgHeight, PixelFormat.Format24bppRgb);


                grWatermark = Graphics.FromImage(bmPhoto);

                grWatermark.DrawImage(imgIn, 0, 0, orgWidth, orgHeight);

                var imageAttributes = new ImageAttributes();
                var colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = {colorMap};

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);


                float[][] colorMatrixElements = {
                                                    new[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                                    new[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                                    new[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                                    new[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
                                                    new[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                                };

                var wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


                imgWatermark = new Bitmap(HttpContext.Current.Server.MapPath("/gfx/company_watermark.gif"));
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;

                int xPosOfWm = ((wmWidth - wmWidth) + 210);
                int yPosOfWm = 150;


                grWatermark.DrawImage(imgWatermark,
                                      new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),
                                      0,
                                      0,
                                      wmWidth,
                                      wmHeight,
                                      GraphicsUnit.Pixel,
                                      imageAttributes);


                bmPhoto.Save(outStream, ImageFormat.Jpeg);
                return outStream.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (grWatermark != null)
                {
                    grWatermark.Dispose();
                }
                if (imgIn != null)
                {
                    imgIn.Dispose();
                }
                if (imgWatermark != null)
                {
                    imgWatermark.Dispose();
                }
            }
        }


        //string getContentType(String path)
        //{
        //    switch (Path.GetExtension(path))
        //    {
        //        case ".bmp": return "Image/bmp";
        //        case ".gif": return "Image/gif";
        //        case ".jpg": return "Image/jpeg";
        //        case ".png": return "Image/png";
        //        default: break;
        //    }
        //    return "";
        //}
        //private static ImageFormat getImageFormat(String path)
        //{
        //    switch (Path.GetExtension(path))
        //    {
        //        case ".bmp": return ImageFormat.Bmp;
        //        case ".gif": return ImageFormat.Gif;
        //        case ".jpg": return ImageFormat.Jpeg;
        //        case ".png": return ImageFormat.Png;
        //        default: break;
        //    }
        //    return ImageFormat.Jpeg;
        //}
    }
}
