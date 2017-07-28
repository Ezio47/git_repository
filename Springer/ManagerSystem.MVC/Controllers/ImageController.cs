using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetImage()
        {
            Message MS = null;
            //StringBuilder sb = new StringBuilder();
            string NAME = Request.Params["NAME"];//单位
            string TITLE =  Request.Params["TITLE"];//出图名称
            string PEOPLE = "制图人:" + Request.Params["PEOPLE"];
            string IMAGESIZE = Request.Params["IMAGESIZE"];
            //string TIME = "制作时间:" + Convert.ToDateTime(Request.Params["TIME"]).ToString("U");
            string TIME = "制图时间:" + Convert.ToDateTime(Request.Params["TIME"]).ToString("yyyy年MM月dd日 HH时mm分", DateTimeFormatInfo.InvariantInfo);
            
            string rSrcImgPath = "/UploadFile/3DPhoto/OriginalMap/1280.png";//原始图片
            var uploadname = Request.Params["uploadname"];
            //if (Request.Cookies["filename"] != null)
            //{
            //    HttpCookie cookie = Request.Cookies["filename"];
            //    uploadname = Server.HtmlEncode(Request.Cookies["filename"].Value);
            //    cookie.Expires = DateTime.Now.AddDays(-2);
            //    Response.Cookies.Set(cookie);
            //}
            //else {
            //    MS = new Message(false, "请选择上传的图片！", "");
            //    return Json(MS);
            //} 
            //var uploadname = Request.Params["uploadname"];
            string rMarkImgPath = "/UploadFile/3DPhoto/3DMap/" + uploadname;//用户上传的图片
            
            string rMarkText = NAME +','+TITLE+','+PEOPLE+','+TIME;//

            string rDstImgPath = "/UploadFile/3DPhoto/NewMap/";//生成图片
            try
            {
                var src = BuildWatermark(rSrcImgPath, rMarkImgPath, rMarkText, rDstImgPath);
                MS = new Message(true, src, "");
            }
            catch (Exception)
            {
                MS = new Message(false, "生成失败！", "");
                
            }
        
            return Json(MS);

        }


        public JsonResult Upload()
        {
            Message ms = null;
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            if (files.Count == 0) return Json("Faild", JsonRequestBehavior.AllowGet);
            //MD5 md5Hasher = new MD5CryptoServiceProvider();
            ///*计算指定Stream对象的哈希值*/
            //byte[] arrbytHashValue = md5Hasher.ComputeHash(files[0].InputStream);
            ///*由以连字符分隔的十六进制对构成的String，其中每一对表示value中对应的元素；例如“F-2C-4A”*/
            //string strHashData = System.BitConverter.ToString(arrbytHashValue).Replace("-", "");
            string FileEextension = Path.GetExtension(files[0].FileName).ToLower();
            string FileName = Path.GetFileName(files[0].FileName);
            if (string.IsNullOrEmpty(FileName))
            {
                ms =new Message(false, "请选择上传的图片!", "");
                return Json(ms, "text/html", JsonRequestBehavior.AllowGet);
            }
            if (!(FileEextension == ".png" || FileEextension == ".gif" || FileEextension == ".jpg" || FileEextension == ".jpeg" || FileEextension == ".bmp"))
            {
                ms = new Message(false, "上传图片格式不正确!", "");
                return Json(ms, "text/html", JsonRequestBehavior.AllowGet);
                //ms = new Message(true, "图片类型只能为gif,png,jpg,jpeg", "");
            }
            string uploadFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + FileEextension;
            string virtualPath = string.Format("/UploadFile/3DPhoto/3DMap/{0}", uploadFileName);
            string fullFileName = Server.MapPath(virtualPath);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            if (!System.IO.File.Exists(fullFileName))
            {
                files[0].SaveAs(fullFileName);
                //Response.Cookies["filename"].Value = uploadFileName;//cookie保存上传服务器上图片名称
                //Response.Cookies["filename"].Expires = DateTime.Now.AddDays(1);
                ms = new Message(true, uploadFileName, "");
            }
          
            return Json(ms, "text/html", JsonRequestBehavior.AllowGet);
          
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadPhoto(FormCollection form)
        {
            //Message ms = null;
            //HttpPostedFile file = null;
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase uploadFile = Request.Files[0];
                string fileName = uploadFile.FileName;
                int fileSize = uploadFile.ContentLength;
                string fileExt = Path.GetExtension(fileName).ToLower();
                string message = "";
                if (string.IsNullOrEmpty(fileName))
                {
                    return Content(@"<script>alert('请选择上传的图片！');history.go(-1);</script>");
                }
                if (!(fileExt == ".png" || fileExt == ".gif" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".bmp"))
                {
                    return Content(@"<script>alert('上传图片格式不正确！');history.go(-1);</script>");
                    //ms = new Message(true, "图片类型只能为gif,png,jpg,jpeg", "");
                }
                else
                {
                    if (fileSize > (int)(3000 * 1024))
                    {
                        return Content(@"<script>alert('图片大小不能超过3M！');history.go(-1);</script>");
                        //ms = new Message(true, "图片大小不能超过500KB", "");
                    }
                    else
                    {
                        //Random r = new Random();
                        //CookieModel cookieInfo = SystemCls.getCookieInfo();
                        //var model = T_SYSSEC_IPSUSERCls.getModel(new T_SYSSEC_IPSUSER_SW { USERID = cookieInfo.UID });
                        string uploadFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt;
                        //string uploadFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + r.Next(100000, 999999) + fileExt;
                        try
                        {
                            string directoryPath = Server.MapPath("/UploadFile/3DPhoto/3DMap/");
                            if (!Directory.Exists(directoryPath))//不存在这个文件夹就创建这个文件夹 
                            {
                                Directory.CreateDirectory(Server.MapPath("/UploadFile/3DPhoto/3DMap/"));
                            }
                            uploadFile.SaveAs(Server.MapPath("/UploadFile/3DPhoto/3DMap/") + uploadFileName);
                            //message = uploadFileName;
                            //ms = new Message(true, "上传成功", "");
                            Response.Cookies["filename"].Value = uploadFileName;//cookie保存上传服务器上图片名称
                            Response.Cookies["filename"].Expires = DateTime.Now.AddDays(1);
                            
                            return Content("<script>alert('上传成功！');window.location.href=document.referrer;</script>");
                        }
                        catch (Exception ex)
                        {
                            message = ex.Message;
                            return Content(@"<script>alert('上传失败，请确认后再上传！');history.go(-1);</script>");
                            //ms = new Message(true, ex.Message, "");
                        }                        
                    }
                }
                
            }
            else {
                return Content(@"<script>alert('请选择需要上传的图片');history.go(-1);</script>");        
            }
            

            //return Content("<script>alert('上传成功！');history.go(-1);</script>");
           
            //return Json(ms);
        }

        /// <summary>    
        /// Creating a Watermarked Photograph with GDI+ for .NET    
        /// </summary>    
        /// <param name="rSrcImgPath">原始图片的物理路径</param>    
        /// <param name="rMarkImgPath">水印图片的物理路径</param>    
        /// <param name="rMarkText">水印文字（不显示水印文字设为空串）</param>    
        /// <param name="rDstImgPath">输出合成后的图片的物理路径</param>    
        public string BuildWatermark(string rSrcImgPath, string rMarkImgPath, string rMarkText, string rDstImgPath)
        {
            rSrcImgPath = Server.MapPath(rSrcImgPath);//原始图片
            rMarkImgPath = Server.MapPath(rMarkImgPath);//水印图片
            string rLogo = Server.MapPath("/UploadFile/3DPhoto/OriginalMap/Logo.png");
            string rExample = Server.MapPath("/UploadFile/3DPhoto/OriginalMap/Example.png");
            var arrText = rMarkText.Split(',');
            string rPath = rDstImgPath;
            rDstImgPath = Server.MapPath(rDstImgPath);//生成图片


            //以下（代码）从一个指定文件创建了一个Image 对象，然后为它的 Width 和 Height定义变量。    
            //这些长度待会被用来建立一个以24 bits 每像素的格式作为颜色数据的Bitmap对象。    
            Image imgPhoto = Image.FromFile(rSrcImgPath);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //这个代码载入水印图片，水印图片已经被保存为一个BMP文件，以绿色(A=0,R=0,G=255,B=0)作为背景颜色。    
            //再一次，会为它的Width 和Height定义一个变量。    
            Image imgWatermark = new Bitmap(rMarkImgPath);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            Image imgLogo = new Bitmap(rLogo);
            int LogoWidth = imgLogo.Width;
            int LogoHeight = imgLogo.Height;

            Image imgExample = new Bitmap(rExample);
            int ExpWidth = imgExample.Width;
            int ExpHeight = imgExample.Height;

            //这个代码以100%它的原始大小绘制imgPhoto 到Graphics 对象的（x=0,y=0）位置。    
            //以后所有的绘图都将发生在原来照片的顶部。    
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(
                 imgPhoto,
                 new Rectangle(0, 0, phWidth, phHeight),
                 0,
                 0,
                 phWidth,
                 phHeight,
                 GraphicsUnit.Pixel);
            //为了最大化版权信息的大小，我们将<a href="http://lib.csdn.net/base/softwaretest" class='replace_word' title="软件测试知识库" target='_blank' style='color:#df3434; font-weight:bold;'>测试</a>7种不同的字体大小来决定我们能为我们的照片宽度使用的可能的最大大小。    
            //为了有效地完成这个，我们将定义一个整型数组，接着遍历这些整型值测量不同大小的版权字符串。    
            //一旦我们决定了可能的最大大小，我们就退出循环，绘制文本    
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = new Font("arial", 14,FontStyle.Bold);
            //SizeF crSize = new SizeF();
            //for (int i = 0; i < 7; i++)
            //{
            //    crFont = new Font("arial", sizes[i],
            //          FontStyle.Bold);
            //    crSize = grPhoto.MeasureString(rMarkText,
            //          crFont);
            //    if ((ushort)crSize.Width < (ushort)phWidth)
            //        break;
            //}
            //因为所有的照片都有各种各样的高度，所以就决定了从图象底部开始的5%的位置开始。    
            //使用rMarkText字符串的高度来决定绘制字符串合适的Y坐标轴。    
            //通过计算图像的中心来决定X轴，然后定义一个StringFormat 对象，设置StringAlignment 为Center。    
            //int yPixlesFromBottom = (int)(phHeight * .05);
            //float yPosFromBottom = ((phHeight -
            //     yPixlesFromBottom) - (crSize.Height / 2));
            //float xCenterOfImg = (phWidth / 2);
            //StringFormat StrFormat = new StringFormat();
            //StrFormat.Alignment = StringAlignment.Center;
            //现在我们已经有了所有所需的位置坐标来使用60%黑色的一个Color(alpha值153)创建一个SolidBrush 。    
            //在偏离右边1像素，底部1像素的合适位置绘制版权字符串。        
            SolidBrush semiTransBrush2 =
                 new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            grPhoto.DrawString(arrText[0],
                 crFont,
                 semiTransBrush2,
                 new PointF(20,220)
                 );

            //根据前面修改后的照片创建一个Bitmap。把这个Bitmap载入到一个新的Graphic对象。    
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grWatermark =
                 Graphics.FromImage(bmWatermark);
  
            //我们会偏离10像素到底部，10像素到左边。    
            int markWidth;
            int markHeight;
            //mark比原来的图宽    
            if (phWidth <= wmWidth)
            {
                markWidth = phWidth - 10;
                markHeight = (markWidth * wmHeight) / wmWidth;
            }
            else if (phHeight <= wmHeight)
            {
                markHeight = phHeight - 10;
                markWidth = (markHeight * wmWidth) / wmHeight;
            }
            else
            {
                markWidth = wmWidth;
                markHeight = 768;
            }
            int xPosOfWm = ((phWidth - markWidth) - 20);
            int yPosOfWm = 10;
            grWatermark.DrawImage(imgWatermark,
                 new Rectangle(xPosOfWm, yPosOfWm, markWidth,
                 markHeight),
                 0,
                 0,
                 wmWidth,
                 580,
                 GraphicsUnit.Pixel
                 );


            //绘制logo
            Bitmap bmLogo = new Bitmap(bmWatermark);
            bmLogo.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grLogo =
                 Graphics.FromImage(bmLogo);
 
            grLogo.DrawImage(imgLogo,
                 new Rectangle(20, 10, 200,
                 200),
                 0,
                 0,
                 LogoWidth,
                 LogoHeight,
                 GraphicsUnit.Pixel
                 );

            //绘制图例
            Bitmap bmExample = new Bitmap(bmLogo);
            bmLogo.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grExample =
                 Graphics.FromImage(bmExample);

            grExample.DrawImage(imgExample,
                 new Rectangle(20, 250, 200,
                 518),
                 0,
                 0,
                 ExpWidth,
                 ExpHeight,
                 GraphicsUnit.Pixel
                 );

            //绘制名称
            Bitmap bmText = new Bitmap(bmExample);
            bmLogo.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grText =
                 Graphics.FromImage(bmText);
            Font crFont1 = new Font("arial", 14, FontStyle.Bold);
            grText.DrawString(arrText[1],
                 crFont1,
                 semiTransBrush2,
                 new PointF(554, 720)
                 );

            //绘制制图人
            Bitmap bmText1 = new Bitmap(bmText);
            bmLogo.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grText1 =
                 Graphics.FromImage(bmText1);
            Font crFont2 = new Font("arial", 10, FontStyle.Bold);
            grText1.DrawString(arrText[2],
                 crFont2,
                 semiTransBrush2,
                 new PointF(940, 700)
                 );

            //绘制制图时间
            Bitmap bmText2 = new Bitmap(bmText1);
            bmLogo.SetResolution(
                 imgPhoto.HorizontalResolution,
                 imgPhoto.VerticalResolution);
            Graphics grText2 =
                 Graphics.FromImage(bmText2);

            grText2.DrawString(arrText[3],
                 crFont2,
                 semiTransBrush2,
                 new PointF(940, 730)
                 );

            //最后的步骤将是使用新的Bitmap取代原来的Image。 销毁Graphic对象，然后把Image 保存到文件系统。    
            imgPhoto = bmText2;
            grPhoto.Dispose();
            grLogo.Dispose();
            grExample.Dispose();
            grText.Dispose();
            grText1.Dispose();
            grText2.Dispose();
            grWatermark.Dispose();
            //Random r = new Random();
            string PhotoName = DateTime.Now.ToString("yyyyMMddhhmmss")+".png";//+ r.Next(100000, 999999);
            imgPhoto.Save(rDstImgPath + PhotoName, ImageFormat.Png);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
            return rPath + PhotoName;//返回图片的地址
        }
    }
}
