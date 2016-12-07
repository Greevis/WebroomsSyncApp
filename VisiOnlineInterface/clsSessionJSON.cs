using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisiOnlineInterface
{
    public class clsSessionJSON
    {
        //public SessionJSON()
        //{
        //    username = "sym";
        //    password = "sym";
        //}

        public clsSessionJSON(string Username, string Password)
        {
            username = Username;
            password = Password;
        }

        public string username { get; set; }
        public string password { get; set; }
    }
}
