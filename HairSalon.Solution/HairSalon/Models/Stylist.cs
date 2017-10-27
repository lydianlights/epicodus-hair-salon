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
        public Stylist(int id, string name, string phone, string email) : this(name, phone, email)
        {
            Id = id;
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
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"SELECT * FROM {SqlTable};";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string phone = rdr.GetString(2);
                string email = rdr.GetString(3);
                Stylist stylist = new Stylist(id, name, phone, email);
                output.Add(stylist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return output;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"DELETE FROM {SqlTable};";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
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
