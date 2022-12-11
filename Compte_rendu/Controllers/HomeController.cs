using CompteRendu.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Diagnostics;

namespace CompteRendu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Debug.WriteLine("openning connexion to the database");
            SQLiteConnection connection = new SQLiteConnection("Data Source=C:\\GL3\\C#\\2022 GL3 .NET Framework TP3 - SQLite database.db;");
            try
            {
                connection.Open();
                Debug.WriteLine("connexion opened");
                using (connection)
                {
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info", connection);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    using (reader)
                    {
                        Debug.WriteLine("reader returned"+ reader.FieldCount);
                        while (reader.Read())
                        {                  
                            int id = (int)reader["id"];
                            string first_name = (string)reader["first_name"];
                            string last_name = (string)reader["last_name"];
                            string email = (string)reader["email"];
                            //DateOnly date_birth = (DateOnly)reader["date_birth"];
                            string image = (string)reader["image"];
                            string country = (string)reader["country"];
                            Debug.WriteLine("{0} - {1} {2} - {3} - {4} - {5}",
                                id, first_name, last_name,  country, email, image);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("exception caught"+ ex.Message);
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        [Route("/Person/all")]
        public IActionResult PersonsData()
        {
            Personal_info personal = new Personal_info();
            List<Person> list = new List<Person>();
            list = personal.GetAllPerson();
            return View(list);
        }

        [HttpGet]
        [Route("/Person/{id}")]
        public IActionResult GetPerson(int id) {
            Personal_info personal = new Personal_info();
            Person person= personal.GetPerson(id);
            return View(person);
                }


        [HttpPost]
        [Route("/Person/search")]
        public IActionResult SearchPerson(string first_name, string country)
        {
            Personal_info personal_info = new Personal_info();
            List<Person> personal = personal_info.GetAllPerson();
            foreach (Person person in personal)
            {
                if (person.first_name == first_name && person.country == country)
                {
                    return Redirect("/Person/"+person.id);
                }
            }
            ViewBag.notFound = true;
            return View();
        }
    }
}