using CompleteMvcCmsProject1.Areas.NewsPortal.Models;
using CompleteMvcCmsProject1.Areas.PhotoSection.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace CompleteMvcCmsProject1.Areas.NewsPortal.Controllers
{
    public class PersonSearchController : ApiController
    {
        [Route("values/GetAllPhotoUpload")]
        [HttpGet]
        public IHttpActionResult GetAllPhotos()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {                
                var query = "GetAllPhotos";
                var list = db.Query<PhotoUpload>(query, commandType: CommandType.StoredProcedure);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
        }

        [Route("values/GetAllPersonSearch")]
        [HttpGet]
        public IHttpActionResult GetAllPersonSearch()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                var query = "GetAllPerson";
                var list = db.Query<PersonSearch>(query, commandType: CommandType.StoredProcedure);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }
        }
        
        [Route("values/getallproducts")]
        [HttpGet]
        public IEnumerable<PersonSearch> GetAllProducts()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                var query = "GetAllPerson";
                var list = db.Query<PersonSearch>(query, commandType: CommandType.StoredProcedure);
                return list;
            }
        }

        [Route("values/GetPersonById/{id}")]
        [HttpGet]
        public IHttpActionResult SearchById(int id)
        {

            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SearchPersonById";
                var param = new DynamicParameters();
                param.Add("Id", id);
                var list = db.Query<PersonSearch>(query, param, commandType: CommandType.StoredProcedure).First();
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }

        }

        [Route("values/Create")]
        [HttpPost]
        public String Create(summernoteNews Uploader)
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
                    var param = new DynamicParameters();
                    param.Add("subject", Uploader.Subject);
                    param.Add("discussion", Uploader.Discussion);
                    param.Add("publishedDate", Uploader.PublishedDate);
                    //param.Add("photoDetails", Uploader.PhotoDetails);
                    db.Query<summernoteNews>(query, param, commandType: CommandType.StoredProcedure);
                    return ("OK");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }

        [Route("values/SearchByName/{name}")]
        [HttpGet]
        public IHttpActionResult SearchByName(string name)
        {

            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "SearchByName";
                var param = new DynamicParameters();
                param.Add("PartialText", name);
                var list = db.Query<PersonSearch>(query, param, commandType: CommandType.StoredProcedure);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }

        }

        [Route("TypeAhead/getaAllStates")]
        [HttpGet]
        public IHttpActionResult typeAheadStates()
        {

            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "TypeAhead_test_selectAllStates";
                var param = new DynamicParameters();
                var list = db.Query<TypeAheadTable>(query, param, commandType: CommandType.StoredProcedure);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
            }

        }

    }   
}
