using System;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
    public class DB
    {
        public static MySqlConnection Connection
        {
            get
            {
                return new MySqlConnection(DBConfiguration.ConnectionString);
            }
        }
    }
}
