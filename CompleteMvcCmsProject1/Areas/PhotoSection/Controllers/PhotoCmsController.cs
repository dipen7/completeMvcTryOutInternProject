using CompleteMvcCmsProject1.Areas.PhotoSection.Models;
using CompleteMvcCmsProject1.Areas.PhotoSection.ViewModel;
using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;

namespace CompleteMvcCmsProject1.Areas.PhotoSection.Controllers
{
    [Authorize]
    public class PhotoCmsController : Controller
    {
        // GET: PhotoSection/PhotoCms
        public ActionResult Index()
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    ViewPhotoVM pageVM = new ViewPhotoVM();
                    var query = "GetAllPhotos";
                    pageVM.PhotoList = db.Query<PhotoUpload>(query, commandType: CommandType.StoredProcedure);
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult create()
        {
            ViewBag.adminName = User.Identity.Name;
            return View();
        }

        [HttpPost]
        public ActionResult create(PhotoUpload Uploader, HttpPostedFileBase ImageData, string ImageString)
        {

            //HttpPostedFileBase file = Request.Files["ImageData"];
            try
            {
                if (ImageData != null)
                {
                    //....
                    //using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                    //{
                    //    using (BinaryWriter bw = new BinaryWriter(fs))
                    //    {
                    //        byte[] data = Convert.FromBase64String(ImageString);
                    //        bw.Write(data);
                    //        bw.Close();
                    //    }
                    //    fs.Close();
                    //}
                    //...

                    // Specify the path to save the uploaded file to.
                    // Get the name of the file to upload.
                    var supportedTypes = new[] { "JPG", "BMP", "PDF", "PNG", "GIF", "JPEG" };
                    var fileExt = System.IO.Path.GetExtension(ImageData.FileName).Substring(1);
                    fileExt = fileExt.ToUpper();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ViewBag.Status = "File Extension Is InValid - Only Upload JPG/PDF/PNG/GIF/JPEG File";
                    }
                    else
                    {
                        //5digit counter starting
                        Random rand = new Random();
                        int count = 0;
                        string counter = "";
                        count = rand.Next(0, 100000);
                        counter = count.ToString("d5");
                        
                        string fileName = counter + System.IO.Path.GetFileName(ImageData.FileName);
                        //.
                        string pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), fileName);
                        string tempfileName = "";
                        if (System.IO.File.Exists(pathToCheck))
                        {
                                                        
                            while (System.IO.File.Exists(pathToCheck))
                            {
                                //5-digit prefix int in filename
                                count = rand.Next(0, 100000);
                                counter = count.ToString("d5");

                                string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName = counter + FileNameWithoutExt + "." + fileExt;
                                //pathToCheck = savePath + tempfileName;
                                pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), tempfileName);
                            }
                            pathToCheck = tempfileName;
                            fileName = tempfileName;

                            // Notify the user that the file name was changed.
                            ViewBag.Status = "A file with the same name already exists." +
                                "<br />Your file was saved as " + fileName;
                        }
                        else
                        {
                            // file was saved.
                            ViewBag.Status = "Your file was uploaded successfully.";
                        }
                        //extra step. for confirmation
                        pathToCheck = System.IO.Path.Combine(
                                           Server.MapPath("~/Image"), fileName);
                        //.
                        using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                byte[] data = Convert.FromBase64String(ImageString);
                                bw.Write(data);
                                bw.Close();
                            }
                            fs.Close();
                        }

                        string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                        using (IDbConnection db = new SqlConnection(_connectionString))
                        {
                            CreateVM pageVM = new CreateVM();
                            //
                            Uploader.PhotoName = fileName;
                            Uploader.ModifiedDate = DateTime.Now;
                            var query = "InsertPhoto";
                            var param = new DynamicParameters();
                            param.Add("photoName", Uploader.PhotoName);
                            param.Add("adminName", User.Identity.Name);
                            param.Add("modifiedDate", Uploader.ModifiedDate);
                            param.Add("photoDetails",Uploader.PhotoDetails);
                            db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure);
                            return Redirect("/PhotoSection/PhotoCms/Index");
                        }
                    }

                    //string fileName = System.IO.Path.GetFileName(ImageData.FileName);
                    //string pathToCheck = System.IO.Path.Combine(
                    //                       Server.MapPath("~/Image"), fileName);


                    ////// Create the path and file name to check for duplicates.
                    ////string pathToCheck = savePath + fileName;

                    //// Create a temporary file name to use for checking duplicates.
                    //string tempfileName = "";

                    //// Check to see if a file already exists with the
                    //// same name as the file to upload.        
                    //if (System.IO.File.Exists(pathToCheck))
                    //{
                    //    int counter = 2;
                    //    while (System.IO.File.Exists(pathToCheck))
                    //    {
                    //        // if a file with this name already exists,
                    //        // prefix the filename with a number.
                    //        tempfileName = counter.ToString() + fileName;
                    //        //pathToCheck = savePath + tempfileName;
                    //        pathToCheck = System.IO.Path.Combine(
                    //                       Server.MapPath("~/Image"), tempfileName);
                    //        counter++;
                    //    }

                    //    fileName = tempfileName;

                    //    // Notify the user that the file name was changed.
                    //    ViewBag.Status = "A file with the same name already exists." +
                    //        "<br />Your file was saved as " + fileName;
                    //}
                    //else
                    //{
                    //    // Notify the user that the file was saved successfully.
                    //    ViewBag.Status = "Your file was uploaded successfully.";
                    //}

                    //// Append the name of the file to upload to the path.
                    ////savePath += fileName;
                    //pathToCheck = System.IO.Path.Combine(
                    //                       Server.MapPath("~/Image"), fileName);

                    //// Call the SaveAs method to save the uploaded
                    //// file to the specified directory.
                    //ImageData.SaveAs(pathToCheck);

                    ////string pic = System.IO.Path.GetFileName(file.FileName);
                    ////string path = System.IO.Path.Combine(
                    ////                       Server.MapPath("~/Image"), pic);
                    ////// file is uploaded
                    ////file.SaveAs(path);

                    ////// save the image path path to the database or you can send image 
                    ////// directly to database
                    ////// in-case if you want to store byte[] ie. for DB

                }
                // after successfully uploading redirect the user
                return Redirect("/PhotoSection/PhotoCms/Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult deletePhoto(int id)
        {
            try
            {
                CreateVM pageVM = new CreateVM();
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    var query = "GetPhotoById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM.Uploader = db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure).First();
                }
                if (pageVM.Uploader.PhotoName != null)
                {
                    string pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), pageVM.Uploader.PhotoName);
                    if (System.IO.File.Exists(pathToCheck))
                    {
                        System.IO.File.Delete(pathToCheck);
                        //Session["DeleteSuccess"] = "Yes";
                    }
                }
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    var query = "DeletePhotoUpload";
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    var resut = db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure);
                }

                return Redirect("/PhotoSection/PhotoCms/Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult modify(int id)
        {
            try
            {
                CreateVM pageVM = new CreateVM();
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    var query = "GetPhotoById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM.Uploader = db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure).First();
                }

                return View(pageVM);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult modify(PhotoUpload update, HttpPostedFileBase ImageData , string ImageString)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                CreateVM pageVM = new CreateVM();
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    var query = "GetPhotoById";
                    var param = new DynamicParameters();
                    param.Add("Id", update.Id);
                    pageVM.Uploader = db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure).First();
                }
                if (pageVM.Uploader.PhotoName != null)
                {
                    string pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), pageVM.Uploader.PhotoName);
                    if (System.IO.File.Exists(pathToCheck))
                    {
                        System.IO.File.Delete(pathToCheck);
                        //Session["DeleteSuccess"] = "Yes";
                    }
                }
                if (ImageData != null)
                {
                    // Specify the path to save the uploaded file to.
                    // Get the name of the file to upload.
                    var supportedTypes = new[] { "JPG", "BMP", "PDF", "PNG", "GIF", "JPEG" };
                    var fileExt = System.IO.Path.GetExtension(ImageData.FileName).Substring(1);
                    fileExt = fileExt.ToUpper();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ViewBag.Status = "File Extension Is InValid - Only Upload JPG/PDF/PNG/GIF/JPEG File";
                        return View();
                    }
                    else
                    {
                        //5digit counter starting
                        Random rand = new Random();
                        int count = 0;
                        string counter = "";
                        count = rand.Next(0, 100000);
                        counter = count.ToString("d5");

                        string fileName = counter + System.IO.Path.GetFileName(ImageData.FileName);
                        //string fileName = System.IO.Path.GetFileName(ImageData.FileName);
                        //.

                        string pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), fileName);
                        string tempfileName = "";
                        if (System.IO.File.Exists(pathToCheck))
                        {
                            while (System.IO.File.Exists(pathToCheck))
                            {
                                //5-digit prefix int in filename
                                count = rand.Next(0, 100000);
                                counter = count.ToString("d5");

                                string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName = counter + FileNameWithoutExt + "." + fileExt;
                                //pathToCheck = savePath + tempfileName;
                                pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), tempfileName);
                            }
                            pathToCheck = tempfileName;
                            fileName = tempfileName;

                            // Notify the user that the file name was changed.
                            ViewBag.Status = "A file with the same name already exists." +
                                "<br />Your file was saved as " + fileName;
                        }
                        else
                        {
                            // Notify the user that the file was saved successfully.
                            ViewBag.Status = "Your file was uploaded successfully.";
                        }
                        pathToCheck = System.IO.Path.Combine(
                                           Server.MapPath("~/Image"), fileName);
                        //ImageData.SaveAs(pathToCheck);
                        using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                byte[] data = Convert.FromBase64String(ImageString);
                                bw.Write(data);
                                bw.Close();
                            }
                            fs.Close();
                        }

                        //..
                        using (IDbConnection db = new SqlConnection(_connectionString))
                        {
                            update.PhotoName = fileName;
                            update.ModifiedDate = DateTime.Now;
                            var query = "ModifyPhotoUploadById";
                            var param = new DynamicParameters();
                            param.Add("id", update.Id);
                            param.Add("photoName", update.PhotoName);
                            param.Add("adminName", User.Identity.Name);
                            param.Add("modifiedDate", update.ModifiedDate);
                            param.Add("photoDetails",update.PhotoDetails);
                            var resut = db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure);
                        }

                        return Redirect("/PhotoSection/PhotoCms/Index");
                    }
                }
                return Redirect("/PhotoSection/PhotoCms/Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult details(int id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    CreateVM pageVM = new CreateVM();
                    var query = "GetPhotoById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM.Uploader = db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure).First();
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult try1()
        {
            return View();
        }

        [HttpGet]
        public ActionResult try2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult try3()
        {
            return View();
        }

        [HttpGet]
        public ActionResult gallary()
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    ViewPhotoVM pageVM = new ViewPhotoVM();
                    var query = "GetAllPhotos";
                    pageVM.PhotoList = db.Query<PhotoUpload>(query, commandType: CommandType.StoredProcedure);
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult canvasheight()
        {
            ViewBag.adminName = User.Identity.Name;            
            return View();
        }

        [HttpPost]
        public ActionResult canvasheight(PhotoUpload Uploader, HttpPostedFileBase ImageData, string ImageString)
        {

            //HttpPostedFileBase file = Request.Files["ImageData"];
            try
            {
                if (ImageData != null)
                {
                    //....
                    //using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                    //{
                    //    using (BinaryWriter bw = new BinaryWriter(fs))
                    //    {
                    //        byte[] data = Convert.FromBase64String(ImageString);
                    //        bw.Write(data);
                    //        bw.Close();
                    //    }
                    //    fs.Close();
                    //}
                    //...

                    // Specify the path to save the uploaded file to.
                    // Get the name of the file to upload.
                    var supportedTypes = new[] { "JPG", "BMP", "PDF", "PNG", "GIF", "JPEG" };
                    var fileExt = System.IO.Path.GetExtension(ImageData.FileName).Substring(1);
                    fileExt = fileExt.ToUpper();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ViewBag.Status = "File Extension Is InValid - Only Upload JPG/PDF/PNG/GIF/JPEG File";
                    }
                    else
                    {
                        //5digit counter starting
                        Random rand = new Random();
                        int count = 0;
                        string counter = "";
                        count = rand.Next(0, 100000);
                        counter = count.ToString("d5");

                        string fileName = counter + System.IO.Path.GetFileName(ImageData.FileName);
                        //.
                        string pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), fileName);
                        string tempfileName = "";
                        if (System.IO.File.Exists(pathToCheck))
                        {

                            while (System.IO.File.Exists(pathToCheck))
                            {
                                //5-digit prefix int in filename
                                count = rand.Next(0, 100000);
                                counter = count.ToString("d5");

                                string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                tempfileName = counter + FileNameWithoutExt + "." + fileExt;
                                //pathToCheck = savePath + tempfileName;
                                pathToCheck = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), tempfileName);
                            }
                            pathToCheck = tempfileName;
                            fileName = tempfileName;

                            // Notify the user that the file name was changed.
                            ViewBag.Status = "A file with the same name already exists." +
                                "<br />Your file was saved as " + fileName;
                        }
                        else
                        {
                            // file was saved.
                            ViewBag.Status = "Your file was uploaded successfully.";
                        }
                        //extra step. for confirmation
                        pathToCheck = System.IO.Path.Combine(
                                           Server.MapPath("~/Image"), fileName);
                        //.
                        using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                        {
                            using (BinaryWriter bw = new BinaryWriter(fs))
                            {
                                byte[] data = Convert.FromBase64String(ImageString);
                                bw.Write(data);
                                bw.Close();
                            }
                            fs.Close();
                        }

                        string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                        using (IDbConnection db = new SqlConnection(_connectionString))
                        {
                            CreateVM pageVM = new CreateVM();
                            //
                            Uploader.PhotoName = fileName;
                            Uploader.ModifiedDate = DateTime.Now;
                            var query = "InsertPhoto";
                            var param = new DynamicParameters();
                            param.Add("photoName", Uploader.PhotoName);
                            param.Add("adminName", User.Identity.Name);
                            param.Add("modifiedDate", Uploader.ModifiedDate);
                            param.Add("photoDetails",Uploader.PhotoDetails);
                            db.Query<PhotoUpload>(query, param, commandType: CommandType.StoredProcedure);
                            return Redirect("/PhotoSection/PhotoCms/Index");
                        }
                    }

                    //string fileName = System.IO.Path.GetFileName(ImageData.FileName);
                    //string pathToCheck = System.IO.Path.Combine(
                    //                       Server.MapPath("~/Image"), fileName);


                    ////// Create the path and file name to check for duplicates.
                    ////string pathToCheck = savePath + fileName;

                    //// Create a temporary file name to use for checking duplicates.
                    //string tempfileName = "";

                    //// Check to see if a file already exists with the
                    //// same name as the file to upload.        
                    //if (System.IO.File.Exists(pathToCheck))
                    //{
                    //    int counter = 2;
                    //    while (System.IO.File.Exists(pathToCheck))
                    //    {
                    //        // if a file with this name already exists,
                    //        // prefix the filename with a number.
                    //        tempfileName = counter.ToString() + fileName;
                    //        //pathToCheck = savePath + tempfileName;
                    //        pathToCheck = System.IO.Path.Combine(
                    //                       Server.MapPath("~/Image"), tempfileName);
                    //        counter++;
                    //    }

                    //    fileName = tempfileName;

                    //    // Notify the user that the file name was changed.
                    //    ViewBag.Status = "A file with the same name already exists." +
                    //        "<br />Your file was saved as " + fileName;
                    //}
                    //else
                    //{
                    //    // Notify the user that the file was saved successfully.
                    //    ViewBag.Status = "Your file was uploaded successfully.";
                    //}

                    //// Append the name of the file to upload to the path.
                    ////savePath += fileName;
                    //pathToCheck = System.IO.Path.Combine(
                    //                       Server.MapPath("~/Image"), fileName);

                    //// Call the SaveAs method to save the uploaded
                    //// file to the specified directory.
                    //ImageData.SaveAs(pathToCheck);

                    ////string pic = System.IO.Path.GetFileName(file.FileName);
                    ////string path = System.IO.Path.Combine(
                    ////                       Server.MapPath("~/Image"), pic);
                    ////// file is uploaded
                    ////file.SaveAs(path);

                    ////// save the image path path to the database or you can send image 
                    ////// directly to database
                    ////// in-case if you want to store byte[] ie. for DB

                }
                // after successfully uploading redirect the user
                return Redirect("/PhotoSection/PhotoCms/Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult TypeAheadCustomeTemplate()
        {
            return View();
        }

    }
}