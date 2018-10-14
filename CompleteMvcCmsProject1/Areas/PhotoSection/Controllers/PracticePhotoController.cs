using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompleteMvcCmsProject1.Areas.PhotoSection.Models;

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
            return View();

            //string fileName = System.IO.Path.GetFileName(imageData.FileName);
            //string pathToCheck = System.IO.Path.Combine(Server.MapPath("~/resizedImage"), fileName);
            //imageData.SaveAs(pathToCheck);
            //return View();
        }

        [HttpGet]
        public ActionResult Canvasresizing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Canvasresizing(string imageData, HttpPostedFileBase FileName)
        {
            string fileName = "2MyUniqueImageFileName.png";
            string fileNameWitPath = Path.Combine(Server.MapPath("~/resizedImage"), fileName);
            //---
            Random rand = new Random();
            int count = 0;
            string counter = "";
            count = rand.Next(0, 100000);
            counter = count.ToString("d5");
            fileName = counter + System.IO.Path.GetFileName(FileName.FileName);
            fileNameWitPath = Path.Combine(Server.MapPath("~/resizedImage"), fileName);
            string tempfileName = "";
            if (System.IO.File.Exists(fileNameWitPath))
            {

                while (System.IO.File.Exists(fileNameWitPath))
                {
                    //5-digit prefix int in filename
                    count = rand.Next(0, 100000);
                    counter = count.ToString("d5");

                    string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = counter + FileName.FileName;
                    //pathToCheck = savePath + tempfileName;
                    fileNameWitPath = System.IO.Path.Combine(
                                   Server.MapPath("~/Image"), tempfileName);
                }
                fileNameWitPath = tempfileName;
            }

            //---

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
        }

        [HttpGet]
        public ActionResult quilljstry()
        {
            return View(new summernote());
        }

        [HttpPost]
        public ActionResult quilljstry(summernote photo)
        {
            summernote obtained = new summernote();
            obtained.content = photo.content;
            return View(obtained);
        }

        [HttpGet]
        public ActionResult tabletry()
        {
            return View();
        }

        [HttpGet]
        public ActionResult datetimepicker()
        {

            return View();
        }

        [HttpPost]
        public ActionResult datetimepicker(datetimemodel update)
        {        
            var obtainedDateTime = update.TryDate;
            return View();
        }

    }
}