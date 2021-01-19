using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Sql;
using System.Diagnostics;

namespace moose
{
    internal class User
    {
        private static MySqlConnection dbConn;

        public int Id { get; private set; }

        public String firstname { get; private set; }

        public String lastname { get; private set; }
        public int age { get; private set; }
        private static LoginCred yy = LoginCred.GetInstance();

        private User(int id, String firstname, String lastname, int age)
        {
            Id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.age = age;
        }

        public static void InitializeDB()
        {
            String connString = yy.Creds;

            Debug.WriteLine(connString);

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

        public static User Insert(String firstname, String lastname, int age)
        {
            String query = string.Format("INSERT INTO Mustapha(firstname, lastname,age) VALUES ('{0}', '{1}', '{2}')", firstname, lastname, age);

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            cmd.ExecuteNonQuery();
            int id = (int)cmd.LastInsertedId;

            User user = new User(id, firstname, lastname, age);

            dbConn.Close();

            return user;
        }

        public void Update(string firstname, string lastname, int age)
        {
            MySqlTransaction trans = null;
            try
            {
                String query = string.Format("UPDATE Mustapha SET firstname='{0}', lastname='{1}', age ='{3}' WHERE ID={2}", firstname, lastname, Id, age);

                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                cmd.Transaction = trans;

                dbConn.Open();
                trans = dbConn.BeginTransaction();
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show("Rollback" + ex.ToString());
            }

            dbConn.Close();
        }

        public void Delete()
        {
            MySqlTransaction trans = null;

            try
            {
                String query = string.Format("DELETE FROM Mustapha WHERE id={0}", Id);
                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                cmd.Transaction = trans;

                dbConn.Open();
                trans = dbConn.BeginTransaction();
                cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show("Rollback" + ex.ToString());
            }
            dbConn.Close();
        }
    }
}