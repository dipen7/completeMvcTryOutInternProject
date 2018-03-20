using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.PhotoSection.Controllers
{
    public class PracticePhotoController : Controller
    {
        // GET: PhotoSection/PracticePhoto
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Canvas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Canvas(string imageData)
        {
            string fileName = "2MyUniqueImageFileName.png";
            string fileNameWitPath = Path.Combine(Server.MapPath("~/resizedImage"), fileName);

            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }
            return View("Canvasresizing");

            //string fileName = System.IO.Path.GetFileName(imageData.FileName);
            //string pathToCheck = System.IO.Path.Combine(Server.MapPath("~/resizedImage"), fileName);
            //imageData.SaveAs(pathToCheck);
            //return View();
        }

        public ActionResult Canvasresizing()
        {
            return View();
        }


    }
}