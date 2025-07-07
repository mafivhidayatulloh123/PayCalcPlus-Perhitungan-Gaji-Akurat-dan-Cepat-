using MySql.Data.MySqlClient;
using System;
using PayCalcPlus.Models;

namespace PayCalcPlus.Controllers

{
    public class LoginController
    {
        public bool Authenticate(User user)
        {
            using (MySqlConnection conn = Koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username AND email = @email AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool Register(User user, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                message = "Mohon lengkapi semua data.";
                return false;
            }

            using (MySqlConnection conn = Koneksi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Cek apakah username atau email sudah terdaftar
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", user.Username);
                    checkCmd.Parameters.AddWithValue("@email", user.Email);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        message = "Username atau Email sudah terdaftar.";
                        return false;
                    }

                    // Insert data baru
                    string insertQuery = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    message = "Error saat registrasi: " + ex.Message;
                    return false;
                }
            }
        }

    }
}
