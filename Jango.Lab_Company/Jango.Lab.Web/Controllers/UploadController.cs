using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace Jango.Lab.Wechat.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        public ActionResult UploadImage()
        {
            var file = System.Web.HttpContext.Current.Request.Files[0];
            string fileFullPath = string.Empty;
            if (file != null)
            {
                try
                {

                    var originFileName = file.FileName;
                    var ext = originFileName.Substring(originFileName.LastIndexOf("."));
                    var length = file.ContentLength;
                    if (maxlenth < length)
                    {
                        return Json(new { success = false, msg = "文件大小超过限制" });
                    }
                    if (!exts.Contains(ext))
                    {
                        return Json(new { success = false, msg = "不允许该文件类型" });
                    }

                    var rootpath = "/upload/";
                    var now = DateTime.Now;
                    rootpath += now.ToString("yyyy-MM-dd");
                    if (!Directory.Exists(rootpath))
                    {
                        Directory.CreateDirectory(rootpath);
                    }
                    var fileName = now.ToString("yyyyMMddHHmmss") + (new Random().Next(1, 100000)) + ext;
                    fileFullPath = rootpath + "/" + fileName;
                    file.SaveAs(AppDomain.CurrentDomain.BaseDirectory + fileFullPath);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, msg = ex.Message });
                }
            }
            ViewBag.fileFullPath = fileFullPath;
            return Json(new { success = true, filePath = fileFullPath });
        }

        private static string[] exts = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bpm" };
        private static int maxlenth = 1000 * 1024;
    }
}