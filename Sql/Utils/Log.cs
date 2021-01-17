using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sql.Utils
{
    public class Log
    {
        private static MySqlConnection dbConn;

        public int Id { get; private set; }

        public String user { get; private set; }

        public String action { get; private set; }

        public String timeOfQuery { get; private set; }

        public String TableName { get; private set; }

        private static LoginCred yy = LoginCred.GetInstance();

        private Log(int id, String user, String action, string time, string table)
        {
            Id = id;
            this.user = user;
            this.action = action;
            this.timeOfQuery = time;
            this.TableName = table;
        }

        public static void InitializeDB()
        {
            String connString = yy.Creds;

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

        public static List<Log> GetUsers()
        {
            List<Log> users = new List<Log>();

            String query = "SELECT * FROM MustaphaLog";

            MySqlCommand cmd = new MySqlCommand(query, dbConn);

            dbConn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string id = reader["id"].ToString();
                String user = reader["user"].ToString();
                String action = reader["action"].ToString();
                String time = reader["timeOfQuery"].ToString();
                String table = reader["tableName"].ToString();

                Log u = new Log(Convert.ToInt32(id), user, action, time, table);

                users.Add(u);
            }

            reader.Close();

            dbConn.Close();

            return users;
        }
    }
}