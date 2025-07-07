using PayCalcPlus.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace PayCalcPlus.Controllers
{
    public class GajiController
    {
        public static DataTable GetAllGaji()
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                var dt = new DataTable();
                string query = "SELECT * FROM gaji_jabatan";
                var adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dt);
                return dt;
            }
        }

        public static void InsertGaji(GajiModel model)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string insert = "INSERT INTO gaji_jabatan (Jabatan, GajiPokok, Tunjangan) VALUES (@j, @gp, @t)";
                var cmd = new MySqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@j", model.Jabatan);
                cmd.Parameters.AddWithValue("@gp", model.GajiPokok);
                cmd.Parameters.AddWithValue("@t", model.Tunjangan);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateGaji(GajiModel model)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string update = "UPDATE gaji_jabatan SET GajiPokok=@gp, Tunjangan=@t WHERE Jabatan=@j";
                var cmd = new MySqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@j", model.Jabatan);
                cmd.Parameters.AddWithValue("@gp", model.GajiPokok);
                cmd.Parameters.AddWithValue("@t", model.Tunjangan);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteGaji(string jabatan)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string delete1 = "DELETE FROM gaji_jabatan WHERE Jabatan=@j";
                var cmd = new MySqlCommand(delete1, conn);
                cmd.Parameters.AddWithValue("@j", jabatan);
                cmd.ExecuteNonQuery();

                string delete2 = "DELETE FROM jabatan WHERE Jabatan=@j";
                var cmd2 = new MySqlCommand(delete2, conn);
                cmd2.Parameters.AddWithValue("@j", jabatan);
                cmd2.ExecuteNonQuery();
            }
        }
    }
}
