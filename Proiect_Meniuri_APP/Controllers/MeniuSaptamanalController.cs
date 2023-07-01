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
    public class MeniuSaptamanalController : ControllerBase
    {

        public class Meniu
        {


            public string Felul1 { get; set; }
            public string Felul2 { get; set; }
            public Meniu(string felul1, string felul2)
            {
                this.Felul1 = felul1;
                this.Felul2 = felul2;
            }
        }

        [HttpGet]
        public string Get()
        {
            //SSMS connection
            string connectionString = "Data Source=SEBASTIANTOSH;Initial Catalog=Backend_Meniuri;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(@"SELECT Felul1.Nume as NumeFel1,Felul2.Nume as NumeFel2 from MeniuSaptamanal
                                                INNER JOIN Meniu
                                                ON Meniu.ID=MeniuSaptamanal.ID_Luni
                                                INNER JOIN Felul2
                                                ON Meniu.ID_Felul2=Felul2.ID
                                                INNER JOIN Felul1
                                                ON Meniu.ID_Felul1=Felul1.ID
                                                UNION ALL
                                                SELECT Felul1.Nume,Felul2.Nume from MeniuSaptamanal
                                                INNER JOIN Meniu
                                                ON Meniu.ID=MeniuSaptamanal.ID_Marti
                                                INNER JOIN Felul2
                                                ON Meniu.ID_Felul2=Felul2.ID
                                                INNER JOIN Felul1
                                                ON Meniu.ID_Felul1=Felul1.ID
                                                UNION ALL
                                                SELECT Felul1.Nume,Felul2.Nume from MeniuSaptamanal
                                                INNER JOIN Meniu
                                                ON Meniu.ID=MeniuSaptamanal.ID_Miercuri
                                                INNER JOIN Felul2
                                                ON Meniu.ID_Felul2=Felul2.ID
                                                INNER JOIN Felul1
                                                ON Meniu.ID_Felul1=Felul1.ID
                                                UNION ALL
                                                SELECT Felul1.Nume,Felul2.Nume from MeniuSaptamanal
                                                INNER JOIN Meniu
                                                ON Meniu.ID=MeniuSaptamanal.ID_Joi
                                                INNER JOIN Felul2
                                                ON Meniu.ID_Felul2=Felul2.ID
                                                INNER JOIN Felul1
                                                ON Meniu.ID_Felul1=Felul1.ID
                                                UNION ALL
                                                SELECT Felul1.Nume,Felul2.Nume from MeniuSaptamanal
                                                INNER JOIN Meniu
                                                ON Meniu.ID=MeniuSaptamanal.ID_Vineri
                                                INNER JOIN Felul2
                                                ON Meniu.ID_Felul2=Felul2.ID
                                                INNER JOIN Felul1
                                                ON Meniu.ID_Felul1=Felul1.ID", connection);

            List<Meniu> meniuri = new List<Meniu>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    String Felul1 = (string)reader["NumeFel1"];
                    Console.WriteLine("Felul1 este :" + Felul1);
                    String Felul2 = (string)reader["NumeFel2"];
                    meniuri.Add(new Meniu(Felul1, Felul2));
                }
            }
            Console.WriteLine(meniuri);
            return JsonConvert.SerializeObject(meniuri);
        }
    }
}
