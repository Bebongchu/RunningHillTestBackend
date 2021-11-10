using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetWordsController : Controller
    {
       
        private readonly IConfiguration _config;
        private readonly List<string> _word_types = new List<string>();
        // GET: GetwordsController
        public GetWordsController(IConfiguration config)
        {
            _config = config;
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
                                _word_types.Add(reader.GetString(0));

                            }
                        }
                    }

                }
            
            }
            catch (SqlException e)
            {
              
            }
        }
        [HttpGet("{word_type}")]
        public string Index(string word_type)
        {
           
            if (_word_types.Contains(word_type))
            {
                try
                {

                    string result = "";
                    using (SqlConnection connection = new SqlConnection(_config["RunninHillDBConnect"]))
                    {
                        using (SqlCommand command = new SqlCommand(_config["getWordsQuery"]+ word_type+@"'", connection))
                        {
                          
                            connection.Open();
                           
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                               
                                while (reader.Read())
                                {
                                    if (result == "")
                                    {
                                        result += reader.GetString(0);
                                    }
                                    else
                                    {
                                        result += "," + reader.GetString(0);
                                    }

                                }
                            }
                        }

                    }
                    return result;
                }
                catch (SqlException e)
                {
                    return e.Message;
                }
            }
            else
            {
                return "";
            }
           
     
        }

       
    }
}
