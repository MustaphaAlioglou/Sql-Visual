using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace moose
{
    internal class User
    {   //database stuff
        private const String SERVER = "dspai.kastoria.teiwm.gr";

        private const String DATABASE = "world";
        private const String UID = "k702760";
        private const String PASSWORD = "K@$t0r1@";
        private static MySqlConnection dbConn;

        // User class stuff
        public int Id { get; private set; }

        public String firstname { get; private set; }

        public String lastname { get; private set; }
        public int age { get; private set; }

        private User(int id, String u, String p, int a)
        {
            Id = id;
            firstname = u;
            lastname = p;
            age = a;
        }

        public static void InitializeDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = SERVER;
            builder.UserID = UID;
            builder.Password = PASSWORD;
            builder.Database = DATABASE;

            String connString = builder.ToString();

            builder = null;

            Console.WriteLine(connString);

            dbConn = new MySqlConnection(connString);

            Application.ApplicationExit += (sender, args) =>
            {
                if (dbConn != null)
                {
                    dbConn.Dispose();
                    dbConn = null;
                }
            };
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();

            String query = "SELECT * FROM Mustapha";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["id"].ToString();
                String firstname = reader["firstname"].ToString();
                String lastname = reader["lastname"].ToString();
                int age = (int)reader["age"];
                User u = new User(Convert.ToInt32(id), firstname, lastname, age);

                users.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return users;
        }

        public static User Insert(String u, String p, int a)
        {
            String query = string.Format("INSERT INTO Mustapha(firstname, lastname,age) VALUES ('{0}', '{1}', '{2}')", u, p, a);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            User user = new User(id, u, p, a);

            dbConn.Close();

            return user;
        }

        public void Update(string u, string p, int a)
        {
            String query = string.Format("UPDATE Mustapha SET firstname='{0}', lastname='{1}', age ='{3}' WHERE ID={2}", u, p, Id, a);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }

        public void Delete()
        {
            String query = string.Format("DELETE FROM Mustapha WHERE id={0}", Id);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();

            dbConn.Close();
        }
    }
}