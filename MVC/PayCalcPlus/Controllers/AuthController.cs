using PayCalcPlus.Models;
using MySql.Data.MySqlClient;
using System;

namespace PayCalcPlus.Controllers
{
    public class AuthController
    {
        public static bool Login(UserModel user)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM users WHERE username = @username AND email = @email AND password = @password";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public static bool Register(UserModel user)
        {
            using (var conn = koneksi.GetConnection())
            {
                conn.Open();
                string check = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
                var checkCmd = new MySqlCommand(check, conn);
                checkCmd.Parameters.AddWithValue("@username", user.Username);
                checkCmd.Parameters.AddWithValue("@email", user.Email);
                if ((long)checkCmd.ExecuteScalar() > 0) return false;

                string insert = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
                var cmd = new MySqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
