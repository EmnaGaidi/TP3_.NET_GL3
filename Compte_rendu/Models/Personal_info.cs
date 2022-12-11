using System.Data.SQLite;

namespace CompteRendu.Models
{
    public class Personal_info
    {
        public List<Person> GetAllPerson()
        {
            var persons = new List<Person>();
            SQLiteConnection connection = new SQLiteConnection("Data Source=C:\\GL3\\C#\\2022 GL3 .NET Framework TP3 - SQLite database.db;");
            connection.Open();
            using(connection) { 
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
                using(reader) {
                    while (reader.Read())
            {
                
                int id = (int)reader["id"];
                string first_name = (string)reader["first_name"];
                string last_name = (string)reader["last_name"];
                string email = (string)reader["email"];
                //DateOnly date_birth = (DateOnly)reader["date_birth"];
                string image = (string)reader["image"];
                string country = (string)reader["country"];
                var person = new Person(id,first_name,last_name,email,image,country);
                persons.Add(person);
            } 
                }
            }
            return persons;
        }
        public Person GetPerson(int id)
        {
            List<Person> persons = GetAllPerson();
            for(int i=0; i<persons.Count; i++)
            {
                if (persons[i].id==id)
                    return persons[i];
            }
            return null;
        }
        
    }
}
