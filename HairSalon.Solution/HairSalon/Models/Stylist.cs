using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        public int? Id {get; private set;}
        public string Name {get; private set;}
        public string Phone {get; private set;}
        public string Email {get; private set;}

        public const string SqlTable = "stylists";

        public Stylist(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Id = null;
        }

        public override bool Equals(Object other)
        {
            if (!(other is Stylist))
            {
                return false;
            }
            else
            {
                Stylist otherStylist = (Stylist)other;
                return (
                    this.Name == otherStylist.Name &&
                    this.Phone == otherStylist.Phone &&
                    this.Email == otherStylist.Email
                );
            }
        }

        public override int GetHashCode()
        {
            return
                this.Name.GetHashCode() +
                this.Phone.GetHashCode() +
                this.Email.GetHashCode();
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> output = new List<Stylist> {};
            return output;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"INSERT INTO {SqlTable} (name, phone, email) VALUES (@Name, @Phone, @Email)";
            cmd.Parameters.Add(new MySqlParameter("@Name", Name));
            cmd.Parameters.Add(new MySqlParameter("@Phone", Phone));
            cmd.Parameters.Add(new MySqlParameter("@Email", Email));
            cmd.ExecuteNonQuery();
            this.Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
