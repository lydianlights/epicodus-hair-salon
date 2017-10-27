using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        public int? Id {get; private set;}
        public string Name {get; private set;}
        public string Phone {get; private set;}
        public int StylistId {get; private set;}

        public const string SqlTable = "clients";

        public Client(string name, string phone, int stylistId)
        {
            Name = name;
            Phone = phone;
            StylistId = stylistId;
            Id = null;
        }
        public Client(int id, string name, string phone, int stylistId) : this(name, phone, stylistId)
        {
            Id = id;
        }

        public override bool Equals(Object other)
        {
            if (!(other is Client))
            {
                return false;
            }
            else
            {
                Client otherClient = (Client)other;
                return (
                    this.Name == otherClient.Name &&
                    this.Phone == otherClient.Phone &&
                    this.StylistId == otherClient.StylistId
                );
            }
        }
        public override int GetHashCode()
        {
            return
                this.Name.GetHashCode() +
                this.Phone.GetHashCode() +
                this.StylistId.GetHashCode();
        }

        public static List<Client> GetAll()
        {
            List<Client> output = new List<Client> {};
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"SELECT * FROM {SqlTable};";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                string phone = rdr.GetString(2);
                int stylistId = rdr.GetInt32(3);
                Client client = new Client(id, name, phone, stylistId);
                output.Add(client);
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
            MySqlConnection conn = DB.Connection;
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

        public static Client FindById(int id)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"SELECT * FROM {SqlTable} WHERE id = @Id;";
            cmd.Parameters.Add(new MySqlParameter("@Id", id));

            string name = null;
            string phone = null;
            int stylistId = 0;
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            if(rdr.Read())
            {
                name = rdr.GetString(1);
                phone = rdr.GetString(2);
                stylistId = rdr.GetInt32(3);
            }
            else
            {
                throw new InvalidOperationException($"No entry exists in table '{SqlTable}' with id '{id}'");
            }
            Client output = new Client(id, name, phone, stylistId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return output;
        }

        public static void UpdateAtId(int id, Client newClientData)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"UPDATE {SqlTable} SET name = @Name, phone = @Phone WHERE id = @Id;";
            cmd.Parameters.Add(new MySqlParameter("@Name", newClientData.Name));
            cmd.Parameters.Add(new MySqlParameter("@Phone", newClientData.Phone));
            cmd.Parameters.Add(new MySqlParameter("@Id", id));
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void RemoveAtId(int id)
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"DELETE FROM {SqlTable} WHERE id = @Id;";
            cmd.Parameters.Add(new MySqlParameter("@Id", id));
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection;
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $@"INSERT INTO {SqlTable} (name, phone, stylist_id) VALUES (@Name, @Phone, @StylistId)";
            cmd.Parameters.Add(new MySqlParameter("@Name", Name));
            cmd.Parameters.Add(new MySqlParameter("@Phone", Phone));
            cmd.Parameters.Add(new MySqlParameter("@StylistId", StylistId));
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
