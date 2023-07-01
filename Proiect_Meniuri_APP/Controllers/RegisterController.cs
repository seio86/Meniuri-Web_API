using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Proiect_Meniuri_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        static void sendVerificationMail(string username, string Mail)
        {
            string to, from, password, mail;
            to = Mail;
            //UsernameContGmail
            from = "nume.s__astian@gmail.com";
            //PasswordContGmail
            password = "oyghkjnklnklnklnklnnlknlk";
            //exemplu in API: "TestMail","testmail","nume.s__astian@gmail.com","1"
            mail = "Salut " + username + ".\nTe-ai inregistrat!";
            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = mail;
            message.Subject = "Inregistrare cont";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, password);
            try
            {
                smtp.Send(message);
                Console.WriteLine("Mailul a fost trimis!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [HttpPost]
        public string Post([FromBody] string[] value)
        {
            String username = value[0];
            String pass = value[1];
            String email = value[2];
            bool isAdmin = Convert.ToBoolean(Convert.ToInt32(value[3]));
            //MSSQLSERVER
            //SSMS connection
            string connectionString = "Data Source=SEBASTIANTOSH;Initial Catalog=Backend_Meniuri;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Users(Username,Pass,Email,isAdmin) VALUES (@Username,@Pass,@Email,@isAdmin)", connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Pass", pass);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@isAdmin", isAdmin);
            try
            {
                int recordsAffected = command.ExecuteNonQuery();
                Console.WriteLine("User " + username + "registered!");
                sendVerificationMail(username, "ene.sebastian@gmail.com");
                connection.Close();
                string[] raspuns = { "200" };
                return JsonConvert.SerializeObject(raspuns);
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.ToString());
                connection.Close();
                string[] raspuns = { "401" };
                return JsonConvert.SerializeObject(raspuns);
            }
        }
    }
}
