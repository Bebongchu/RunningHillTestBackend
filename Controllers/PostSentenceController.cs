using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Backend2.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PostSentenceController : Controller
    {
        private readonly IConfiguration _config;
        public PostSentenceController(IConfiguration config)
        {
            _config = config;

        }

        [HttpPost]
        public string Index(string sentence)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["RunninHillDBConnect"]))
                {
                    using (SqlCommand command = new SqlCommand(_config["postSentence"]+"("+ sentence+")", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           


                        }
                    }

                }
                return "sucess";

            }
            catch (SqlException e)
            {
                return "failed";
            }
        }


    }
}
