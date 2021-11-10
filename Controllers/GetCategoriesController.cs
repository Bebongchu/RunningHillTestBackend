using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Backend2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GetCategoriesController : ControllerBase
    {
        private readonly IConfiguration _config;
        public GetCategoriesController(IConfiguration config)
        {
            _config = config;

        }

        [HttpGet]
        public string Index()
        {
            string cats = "";
            try
            {              
                using (SqlConnection connection = new SqlConnection(_config["RunninHillDBConnect"]))
                {
                    using (SqlCommand command = new SqlCommand(_config["getCatsQuery"], connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                
                                if (cats == "")
                                {
                                    cats += reader.GetString(0);
                                }
                                else
                                {
                                    cats += "," + reader.GetString(0);
                                }

                            }
                        }
                    }

                }
                return cats;

            }
            catch (SqlException e)
            {
                return cats;
            }

        }
    }
}
