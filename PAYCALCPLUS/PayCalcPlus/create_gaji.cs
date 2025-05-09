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
    public partial class create_gaji : Form
    {
        private string connString = "Server=localhost;Database=paycalcplus;Uid=root;Pwd=;";

        public create_gaji()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jabatan = textBox1.Text.Trim();
            decimal gajiPokok;
            decimal tunjangan;

            // Validasi input
            if (string.IsNullOrEmpty(jabatan) ||
                !decimal.TryParse(textBox2.Text, out gajiPokok) ||
                !decimal.TryParse(textBox3.Text, out tunjangan))
            {
                MessageBox.Show("Pastikan semua input terisi dengan benar.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // Mulai transaksi
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;

                        // Masukkan ke tabel jabatan jika belum ada
                        cmd.CommandText = "INSERT IGNORE INTO jabatan (Jabatan) VALUES (@jabatan)";
                        cmd.Parameters.AddWithValue("@jabatan", jabatan);
                        cmd.ExecuteNonQuery();

                        // Masukkan ke tabel gaji_jabatan
                        cmd.CommandText = @"
                    INSERT INTO gaji_jabatan (Jabatan, GajiPokok, Tunjangan)
                    VALUES (@jabatan, @gajiPokok, @tunjangan)
                ";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@jabatan", jabatan);
                        cmd.Parameters.AddWithValue("@gajiPokok", gajiPokok);
                        cmd.Parameters.AddWithValue("@tunjangan", tunjangan);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data berhasil disimpan ke database.");
                    data_gaji submitform = new data_gaji();
                    submitform.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void create_gaji_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            data_gaji formcancel = new data_gaji();
            formcancel.Show();
            this.Hide();
        }
    }
}
