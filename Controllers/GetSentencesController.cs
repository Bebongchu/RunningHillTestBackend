using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Backend2.Controllers
{

    [ApiController]
    [Route("[controller]")]
  
    public class GetSentencesController : Controller
    {
        private readonly IConfiguration _config;
      
        public GetSentencesController(IConfiguration config)
        {
            _config = config;
        }

        public string Index()
        {
            string sentences = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["RunninHillDBConnect"]))
                {
                    using (SqlCommand command = new SqlCommand(_config["getSentencesQuery"], connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                if (sentences == "")
                                {
                                    sentences += reader.GetString(0);
                                }
                                else
                                {
                                    sentences += "," + reader.GetString(0);
                                }
                                
                          
                            }
                        }
                    }

                }
                return sentences;

            }
            catch (SqlException e)
            {
                return sentences;
            }
        }

    }
}
