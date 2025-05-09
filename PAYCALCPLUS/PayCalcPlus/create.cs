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
    public partial class create : Form
    {
        private string connString = "Server=localhost;Database=paycalcplus;Uid=root;Pwd=;";

        public create()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string KodeKaryawan = textBox1.Text.Trim();
            string NIP = textBox2.Text.Trim();
            string NamaKaryawan = textBox3.Text.Trim();
            string Jabatan = comboBoxJabatan.SelectedItem.ToString();

            if (KodeKaryawan == "" || NIP == "" || NamaKaryawan == "" || Jabatan == "")
            {
                MessageBox.Show("Mohon lengkapi semua data.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {

                    conn.Open();

                    // Cek apakah data dengan kombinasi yang sama sudah ada
                    string checkQuery = @"
                    SELECT COUNT(*) FROM karyawan 
                    WHERE KodeKaryawan = @KodeKaryawan 
                       OR NIP = @NIP 
                       OR NamaKaryawan = @NamaKaryawan 
                       OR Jabatan = @Jabatan";

                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@KodeKaryawan", KodeKaryawan);
                    checkCmd.Parameters.AddWithValue("@NIP", NIP);
                    checkCmd.Parameters.AddWithValue("@NamaKaryawan", NamaKaryawan);
                    checkCmd.Parameters.AddWithValue("@Jabatan", Jabatan);
                    

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Data dengan KodeKaryawan, NIP, Nama, atau Jabatan tersebut sudah terdaftar.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Jika belum ada, lakukan insert
                    string insertQuery = @"
                    INSERT INTO karyawan (KodeKaryawan, NIP, NamaKaryawan, Jabatan) 
                    VALUES (@KodeKaryawan, @NIP, @NamaKaryawan, @Jabatan)";

                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@KodeKaryawan", KodeKaryawan);
                    insertCmd.Parameters.AddWithValue("@NIP", NIP);
                    insertCmd.Parameters.AddWithValue("@NamaKaryawan", NamaKaryawan);
                    insertCmd.Parameters.AddWithValue("@Jabatan", Jabatan);

                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Data karyawan berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    data_karyawan cancel = new data_karyawan();
                    cancel.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void create_Load(object sender, EventArgs e)
        {
            LoadJabatan();
        }

        private void LoadJabatan()
        {
            comboBoxJabatan.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Jabatan FROM jabatan";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string jabatan = reader["Jabatan"].ToString();
                        comboBoxJabatan.Items.Add(jabatan);
                    }

                    if (comboBoxJabatan.Items.Count > 0)
                        comboBoxJabatan.SelectedIndex = 0; // default pilih item pertama
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data jabatan: " + ex.Message);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            data_karyawan formcancel = new data_karyawan();
            formcancel.Show();
            this.Hide();
        }
    }
}
