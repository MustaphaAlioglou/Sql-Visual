using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class LoginCred
    {
        private LoginCred()
        {
        }

        private static LoginCred _instance;
        public string Creds { get; set; }

        public static LoginCred GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LoginCred();
            }
            return _instance;
        }
    }
}