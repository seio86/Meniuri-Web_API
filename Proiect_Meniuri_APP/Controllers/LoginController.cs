using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Meniuri_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public string Post([FromBody] string[] value)
        {
            string username = value[0];
            string pass = value[1];

            //SSMS connection
            string connectionString = "Data Source=SEBASTIANTOSH;Initial Catalog=Backend_Meniuri;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand checkAccount = new SqlCommand("Select count(*) FROM Users WHERE Username='" + username + "' AND Pass='" + pass + "'", connection);
            int userExist = (int)checkAccount.ExecuteScalar();

            if (userExist > 0)
            {
                Console.WriteLine("Utilizatorul " + username + " s-a autentificat cu succes");
                connection.Close();
                string[] raspuns = { "200" };
                return JsonConvert.SerializeObject(raspuns);
            }
            else 
            {
                Console.WriteLine("Utilizatorul " + username + " nu s-a autentificat cu succes");
                connection.Close();
                string[] raspuns = { "401" };
                return JsonConvert.SerializeObject(raspuns);
            }
        }
    }
}
