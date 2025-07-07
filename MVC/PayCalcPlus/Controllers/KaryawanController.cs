using PayCalcPlus.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace PayCalcPlus.Controllers
{
    public class KaryawanController
    {
        public static DataTable GetAllKaryawan()
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                var dt = new DataTable();
                string query = "SELECT * FROM karyawan";
                var adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dt);
                return dt;
            }
        }

        public static void InsertKaryawan(KaryawanModel model)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string insert = "INSERT INTO karyawan (KodeKaryawan, NIP, NamaKaryawan, Jabatan) VALUES (@k, @n, @nm, @j)";
                var cmd = new MySqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@k", model.KodeKaryawan);
                cmd.Parameters.AddWithValue("@n", model.NIP);
                cmd.Parameters.AddWithValue("@nm", model.NamaKaryawan);
                cmd.Parameters.AddWithValue("@j", model.Jabatan);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateKaryawan(KaryawanModel model)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string update = "UPDATE karyawan SET NIP=@n, NamaKaryawan=@nm, Jabatan=@j WHERE KodeKaryawan=@k";
                var cmd = new MySqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@k", model.KodeKaryawan);
                cmd.Parameters.AddWithValue("@n", model.NIP);
                cmd.Parameters.AddWithValue("@nm", model.NamaKaryawan);
                cmd.Parameters.AddWithValue("@j", model.Jabatan);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteKaryawan(string kodeKaryawan)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string delete = "DELETE FROM karyawan WHERE KodeKaryawan = @kode";
                var cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@kode", kodeKaryawan);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
