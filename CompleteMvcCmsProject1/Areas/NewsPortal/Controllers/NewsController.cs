using CompleteMvcCmsProject1.Areas.NewsPortal.Models;
using CompleteMvcCmsProject1.Areas.NewsPortal.ViewModel;
using CompleteMvcCmsProject1.Areas.PhotoSection.Models;
using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace CompleteMvcCmsProject1.Areas.NewsPortal.Controllers
{
    public class NewsController : Controller
    {
        // GET: NewsPortal/News
        public ActionResult Index()
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    AllNews pageVM = new AllNews();
                    var query = "SelectAllNews";
                    pageVM.Newsz = db.Query<summernoteNews>(query, commandType: CommandType.StoredProcedure);
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
            return View();
        }


        [HttpPost]
        public ActionResult create(summernoteNews Uploader,string datepicker)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    summernoteNews pageVM = new summernoteNews();
                    //Uploader.PhotoName = fileName;
                    //Uploader.ModifiedDate = DateTime.Now;
                    var query = "InsertNews";
                    Uploader.PublishedDate = Convert.ToDateTime(datepicker);
                    var param = new DynamicParameters();
                    param.Add("subject", Uploader.Subject);
                    param.Add("discussion", Uploader.Discussion);
                    param.Add("publishedDate", Uploader.PublishedDate);
                    //param.Add("photoDetails", Uploader.PhotoDetails);
                    db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure);
                    return Redirect("Index");
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult deleteNews(int id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    var query = "DeleteNewsById";
                    var param = new DynamicParameters();
                    param.Add("id", id);
                    var resut = db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure);
                }

                return Redirect("Index");
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
                summernoteNews pageVM = new summernoteNews();
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var query = "SelectNewsById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM = db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure).First();
                }

                return View(pageVM);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult modify(summernoteNews update)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                summernoteNews pageVM = new summernoteNews();
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    //update.ModifiedDate = DateTime.Now;
                    var query = "ModifyNewsById";
                    var param = new DynamicParameters();
                    param.Add("id", update.Id);
                    param.Add("subject", update.Subject);
                    param.Add("discussion", update.Discussion);
                    param.Add("publishedDate", update.PublishedDate);
                    var resut = db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure);
                }
                return Redirect("Index");
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
                    summernoteNews pageVM = new summernoteNews();
                    var query = "SelectNewsById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM = db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure).First();
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult NewsList()
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    AllNews pageVM = new AllNews();
                    var query = "SelectAllNews";
                    pageVM.Newsz = db.Query<summernoteNews>(query, commandType: CommandType.StoredProcedure);
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult NewsDetail(int id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    summernoteNews pageVM = new summernoteNews();
                    var query = "SelectNewsById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM = db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure).First();
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult PersonSearch()
        {
            return View();
        }

        public ActionResult PersonSearched(int id)
        {
            try
            {
                string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    PersonSearch pageVM = new PersonSearch();
                    var query = "SearchPersonById";
                    var param = new DynamicParameters();
                    param.Add("Id", id);
                    pageVM = db.Query<PersonSearch>(query, param, commandType: CommandType.StoredProcedure).First();
                    return View(pageVM);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult multidropdown()
        {

            return View();
        }

        [HttpGet]
        public ActionResult multidropdownTest()
        {

            return View();
        }

        [HttpGet]
        public ActionResult TypeAhead()
        {
            return View();
        }

        [HttpGet]
        public ActionResult typeaheadPrefetch()
        {
            return View();
        }

        [HttpGet]
        public ActionResult typeaheadRemote()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult typeaheadCostumeTemplate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult typeaheadCostumeTemplateTry()
        {
            return View();
        }

        [HttpGet]
        public ActionResult custTempTry()
        {
            return View();
        }

        [HttpGet]
        public ActionResult custtemptryview(int id)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "GetAllPhotos";
                var list = db.Query<PhotoUpload>(query, commandType: CommandType.StoredProcedure).First();
                if (list == null)
                {
                    return View(new PhotoUpload());
                }
                return View(list);
            }
        }

        [HttpGet]
        public ActionResult testtryshow() { return View(); }
    }
}