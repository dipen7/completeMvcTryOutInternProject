using CompleteMvcCmsProject1.Areas.CmsAdmin.Models;
using CompleteMvcCmsProject1.Areas.CmsAdmin.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.CmsAdmin.Controllers
{
    //[RouteArea("cmsadmin")]
    [Authorize]
    public class AdminCmsController : Controller
    {

        // GET: CmsAdmin/AdminCms
        public ActionResult Index()
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    CustomerPageVM pageVM = new CustomerPageVM();
                    var query = "GetAllStudent";
                    pageVM.OurCustomers = db.Query<Student>(query, commandType: CommandType.StoredProcedure);
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        //[Route("create")]
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }

        //[HttpGet]
        ////[Route("create/{id}")]
        //public ActionResult Create( int id)
        //{
        //    ViewBag.Title = "Modify";
        //    try
        //    {
        //        string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
        //        using (IDbConnection db = new SqlConnection(_connectionString))
        //        {
        //            CustomerPageVM pageVM = new CustomerPageVM();
        //            var query = "GetStudentById";
        //            var param = new DynamicParameters();
        //            param.Add("Id", id);
        //            var list = db.Query<Student>(query, param, commandType: CommandType.StoredProcedure).First();
        //            if (list == null)
        //            {
        //                return View();
        //            }
        //            return View(list);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(ex.Message);
        //    }
        //}

        [HttpPost]
        public ActionResult create(Student Instd)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    CustomerPageVM pageVM = new CustomerPageVM();
                    var query = "InsertStudent";
                    var param = new DynamicParameters();
                    param.Add("id", Instd.Id);
                    param.Add("firstName", Instd.FirstName);
                    param.Add("midName", Instd.MidName);
                    param.Add("lastName", Instd.LastName);
                    param.Add("genderId", Instd.GenderId);
                    param.Add("email", Instd.Email);
                    param.Add("address", Instd.Address);
                    param.Add("quillContent", Instd.QuillContent);
                    param.Add("bloodId", Instd.BloodId);
                    db.Query<Student>(query, param, commandType: CommandType.StoredProcedure);
                    return Redirect("/CmsAdmin/AdminCms/");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Modify(int id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var query = "GetStudentById";
                    var param1 = new DynamicParameters();
                    param1.Add("Id", id);
                    var list = db.Query<Student>(query, param1, commandType: CommandType.StoredProcedure).First();
                    return View(list);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Modify(Student Stdnt)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    CustomerPageVM pageVM = new CustomerPageVM();
                    var query = "ModifyById";
                    var param = new DynamicParameters();
                    param.Add("id", Stdnt.Id);
                    param.Add("firstName", Stdnt.FirstName);
                    param.Add("midName", Stdnt.MidName);
                    param.Add("lastName", Stdnt.LastName);
                    param.Add("genderId", Stdnt.GenderId);
                    param.Add("email", Stdnt.Email);
                    param.Add("address", Stdnt.Address);
                    param.Add("quillContent", Stdnt.QuillContent);
                    param.Add("bloodId", Stdnt.BloodId);
                    db.Query<Student>(query, param, commandType: CommandType.StoredProcedure);

                    query = "GetStudentById";
                    var param1 = new DynamicParameters();
                    param1.Add("Id", Stdnt.Id);
                    var list = db.Query<Student>(query, param1, commandType: CommandType.StoredProcedure).First();
                    return Redirect("/CmsAdmin/AdminCms/");
                    //return View(list);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult delete(int id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    CustomerPageVM pageVM = new CustomerPageVM();
                    var query = "DeleteStudentWithId";
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    db.Query<Student>(query, param, commandType: CommandType.StoredProcedure);
                    return Redirect("/CmsAdmin/AdminCms/");
                }
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
                    CustomerPageVM pageVM = new CustomerPageVM();
                    var query = "GetStudentById";
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    var list=db.Query<Student>(query, param, commandType: CommandType.StoredProcedure).First();
                    return View(list);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}