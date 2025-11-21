using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Erronka1.Database;
using static Erronka1.MainWindow;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Erronka1
{
    internal class Database
    {
        MySqlConnection connection;
        MySqlDataReader reader;
        MySqlCommand command;

        public void ConnectToDatabase()
        {
            string connectionString = "Server=localhost;Port=3306;Database=Erronka_xaguinagama;Uid=root;Pwd=zubiri123;";
            connection = new MySqlConnection(connectionString);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        public List<Articulos> getArtikuluak()
        {
            string query = "SELECT * FROM articulos";
            List<Articulos> artikuluak = new List<Articulos>();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id1 = (int)reader["id"];
                    string articulo1 = reader["articulo"].ToString();
                    int precio1 = (int)reader["precio"];
                    string tipo1 = reader["tipo"].ToString();
                    int stock1 = (int)reader["stock"];
                    // MessageBox.Show($"ID: {id1}, Name: {articulo1}, precio: {precio1}, tipo: {tipo1}");
                    artikuluak.Add(new Articulos() { Id = id1, Articulo = articulo1, Precio = precio1, Tipo = tipo1, Stock=stock1});
                }
                reader.Close();
                return artikuluak;
            }
        }
        public List<string> getTipos()
        {
            string query = "SELECT DISTINCT tipo FROM articulos";
            List<string> tipoak = new List<string>();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tipoak.Add(reader["tipo"].ToString());
                }
                reader.Close();
                return tipoak;
            }
        }
        public int getHighId()
        {
            string query = "SELECT max(id) FROM usuarios";
            int i = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    i = (int)reader["max(id)"];
                }
                reader.Close();
                return i;
            }
        }
        public Erabiltzaile getKontua(string izen, string pasahitz)
        {
            string query = "SELECT * FROM usuarios WHERE nombre="+ '"'+izen + '"' + " AND contraseña=" +'"'+pasahitz +'"';
            int i = 0;
            Erabiltzaile userra = new Erabiltzaile() { Id = -1, Nombre = "Ez", Contraseña = "Ez", Rol = "Ez" };
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    userra = new Erabiltzaile() { Id = (int)reader["id"], Nombre = reader["nombre"].ToString(), Contraseña = reader["contraseña"].ToString(), Rol = reader["rol"].ToString() };
                }
                reader.Close();
                return userra;
            }
        }
        public List<Erabiltzaile> getKontuak()
        {
            string query = "SELECT * FROM usuarios";
            List<Erabiltzaile> erabiltzaileak = new List<Erabiltzaile>();
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id1 = (int)reader["id"];
                    string nombre1 = reader["nombre"].ToString();
                    string contraseña1 = reader["contraseña"].ToString();
                    string rol1 = reader["rol"].ToString();
                    erabiltzaileak.Add(new Erabiltzaile() { Id = id1, Nombre = nombre1, Contraseña = contraseña1, Rol = rol1 });
                }
                reader.Close();
                return erabiltzaileak;
            }
        }
        public Erabiltzaile getKontuaId(int id)
        {
            string query = "SELECT * FROM usuarios WHERE id=" +id;
            int i = 0;
            Erabiltzaile erabiltzailea = new Erabiltzaile() { Id = -1, Nombre = "Ez", Contraseña = "Ez", Rol = "Ez" };
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    erabiltzailea = new Erabiltzaile() { Id = (int)reader["id"], Nombre = reader["nombre"].ToString(), Contraseña = reader["contraseña"].ToString(), Rol = reader["rol"].ToString() };
                }
                reader.Close();
                return erabiltzailea;
            }
        }
        public int updateStock(int id, int stock)
        {
            string query = "UPDATE articulos SET stock=" + '"' + stock + '"' + " WHERE id=" + '"' + id + '"';
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public int updateUser(Erabiltzaile erabiltzailea)
        {
            string query = "UPDATE usuarios SET nombre=" + '"' + erabiltzailea.Nombre + '"' + ", contraseña=" + '"' + erabiltzailea.Contraseña + '"'+", rol=" +'"'+ erabiltzailea.Rol +'"' + " WHERE id=" + erabiltzailea.Id;
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public int sortuUser(Erabiltzaile erabiltzailea)
        {
            string query = "INSERT INTO usuarios VALUES (" +(getHighId()+1)+ ", "+'"' + erabiltzailea.Nombre + '"' + ", " + '"' + erabiltzailea.Contraseña + '"' + ", " + '"' + erabiltzailea.Rol + '"'+")";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public int deleteUser(int id)
        {
            string query = "DELETE FROM usuarios WHERE id="+id;
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public void itxiKonekxioa()
        {
            connection.Close();
        }
        public class Erabiltzaile
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public string Contraseña { get; set; }

            public string Rol { get; set; }
        }

    }
}
