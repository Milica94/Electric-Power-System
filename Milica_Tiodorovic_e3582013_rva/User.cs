using System;

namespace Milica_Tiodorovic_e3582013_rva
{
    [Serializable]
    public class User
    {
        private static string usnm;
        private static string pass;
        public string Usnm { get; set; }
        public string Pass { get; set; }

        public User()
        {

        }

        public User(string username, string password)
        {
            this.Usnm = username;
            this.Pass = password;
        }
    }
}