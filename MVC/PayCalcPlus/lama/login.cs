using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PayCalcPlus
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox1.Text.Trim();

            if (email == "" || username == "" || password == "")
            {
                MessageBox.Show("Mohon lengkapi semua data.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = koneksi.GetConnection())
            {
                try
                {

                    conn.Open();

                    // Cek apakah data sudah ada
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    checkCmd.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Username atau Email sudah terdaftar.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Jika data belum ada, lakukan registrasi
                    string insertQuery = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
                    MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kembali ke login
                    Form1 loginForm = new Form1();
                    loginForm.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat registrasi: " + ex.Message);
                }
            }            // Menutup form register
        }

    }
}
