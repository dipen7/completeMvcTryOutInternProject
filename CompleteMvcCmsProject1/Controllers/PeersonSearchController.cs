using CompleteMvcCmsProject1.Areas.NewsPortal.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace CompleteMvcCmsProject1.Controllers
{
    public class PeersonSearchController : ApiController
    {
        //[Route("values/getallproducts")]
        [HttpGet]
        public IEnumerable<PersonSearch> GetAllProducts()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString as string;
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var query = "GetAllPerson";
                var list = db.Query<PersonSearch>(query, commandType: CommandType.StoredProcedure);
                return list;
            }
        }
    }
}
