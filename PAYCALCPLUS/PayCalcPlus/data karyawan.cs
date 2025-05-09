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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PayCalcPlus
{
    public partial class data_karyawan : Form
    {
        private string connString = "Server=localhost;Database=paycalcplus;Uid=root;Pwd=;";

        public data_karyawan()
        {
            InitializeComponent();
        }

        private void data_karyawan_Load(object sender, EventArgs e)
        {
            LoadJabatan();
            LoadData(); // pastikan tetap memuat data saat form dibuka
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



        private void LoadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM karyawan";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ClearTextBoxes()
        {
            textBox2.Clear();
            textBox3.Clear();
            comboBoxJabatan.SelectedIndex = 0;
        }


        private void UpdateData()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diupdate!");
                return;
            }

            if (
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(comboBoxJabatan.Text))
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            // 🔧 Perubahan di sini
            string KodeKaryawan = dataGridView1.SelectedRows[0].Cells["KodeKaryawan"].Value.ToString();
            string NIP = textBox2.Text.Trim();
            string NamaKaryawan = textBox3.Text.Trim();
            string Jabatan = comboBoxJabatan.SelectedItem.ToString();


            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = @"UPDATE karyawan 
                         SET NIP = @NIP, NamaKaryawan = @NamaKaryawan, Jabatan = @Jabatan 
                         WHERE KodeKaryawan = @KodeKaryawan";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NIP", NIP);
                cmd.Parameters.AddWithValue("@NamaKaryawan", NamaKaryawan);
                cmd.Parameters.AddWithValue("@Jabatan", Jabatan);
                cmd.Parameters.AddWithValue("@KodeKaryawan", KodeKaryawan);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil diupdate!");
                LoadData();
                ClearTextBoxes();
            }
        }





        private void DeleteData()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string kodeKaryawan = dataGridView1.SelectedRows[0].Cells["KodeKaryawan"].Value.ToString();

                DialogResult result = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM karyawan WHERE KodeKaryawan = @KodeKaryawan";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@KodeKaryawan", kodeKaryawan);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data berhasil dihapus!");
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Terjadi kesalahan. Silakan coba lagi.");
                            // Log ex.ToString() for developer review
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih baris data yang ingin dihapus!");
            }
        }



        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboard formdashboard = new dashboard();
            formdashboard.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            data_gaji formgaji = new data_gaji();
            formgaji.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Apakah Anda yakin ingin logout?",
            "Konfirmasi Logout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
             );

            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close(); // Menutup form dashboard
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            create formcreate = new create();
            formcreate.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Mengisi TextBox dengan NIP dan NamaKaryawan
                textBox2.Text = row.Cells["NIP"].Value?.ToString();
                textBox3.Text = row.Cells["NamaKaryawan"].Value?.ToString();

                // Mengisi ComboBox dengan Jabatan
                string jabatanValue = row.Cells["Jabatan"].Value?.ToString();
                if (comboBoxJabatan.Items.Contains(jabatanValue))
                {
                    comboBoxJabatan.SelectedItem = jabatanValue;
                }
                else
                {
                    comboBoxJabatan.SelectedIndex = 0; // fallback jika jabatan tidak ada di list
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
