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
    public partial class data_gaji : Form
    {

        public data_gaji()
        {
            InitializeComponent();
        }

        private void LoadGajiJabatan()
        {
            using (MySqlConnection conn = koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Jabatan, GajiPokok, Tunjangan, GajiBersih FROM gaji_jabatan";

                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                    MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Optional: mengubah nama header kolom agar lebih mudah dibaca
                    dataGridView1.Columns["Jabatan"].HeaderText = "Jabatan";
                    dataGridView1.Columns["GajiPokok"].HeaderText = "Gaji Pokok";
                    dataGridView1.Columns["Tunjangan"].HeaderText = "Tunjangan";
                    dataGridView1.Columns["GajiBersih"].HeaderText = "Gaji Bersih";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data gaji jabatan: " + ex.Message);
                }
            }
        }


        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dashboard formdashboard = new dashboard();
            formdashboard.Show();
            this.Hide();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            data_karyawan formKaryawan = new data_karyawan();
            formKaryawan.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void data_gaji_Load(object sender, EventArgs e)
        {
            LoadGajiJabatan();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox3.Text = row.Cells["GajiPokok"].Value?.ToString();
                textBox1.Text = row.Cells["Tunjangan"].Value?.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            create_gaji formgaji = new create_gaji();
            formgaji.Show();
            this.Hide();
        }

        private void DeleteGajiJabatan()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!");
                return;
            }

            string jabatan = dataGridView1.SelectedRows[0].Cells["Jabatan"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Yakin ingin menghapus data jabatan '{jabatan}' dari sistem?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            using (MySqlConnection conn = koneksi.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Hapus dari tabel gaji_jabatan
                    string deleteGajiQuery = "DELETE FROM gaji_jabatan WHERE Jabatan = @Jabatan";
                    MySqlCommand cmd1 = new MySqlCommand(deleteGajiQuery, conn);
                    cmd1.Parameters.AddWithValue("@Jabatan", jabatan);
                    cmd1.ExecuteNonQuery();

                    // Hapus dari tabel jabatan (jika ada)
                    string deleteJabatanQuery = "DELETE FROM jabatan WHERE Jabatan = @Jabatan";
                    MySqlCommand cmd2 = new MySqlCommand(deleteJabatanQuery, conn);
                    cmd2.Parameters.AddWithValue("@Jabatan", jabatan);
                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Data jabatan berhasil dihapus dari sistem.");
                    LoadGajiJabatan();
                    ClearTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat menghapus: " + ex.Message);
                }
            }
        }


        private void ClearTextBoxes()
        {
            textBox3.Clear();
            textBox1.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diupdate!");
                return;
            }

            decimal GajiPokok;
            if (!decimal.TryParse(textBox3.Text.Trim(), out GajiPokok))
            {
                MessageBox.Show("Gaji Pokok harus berupa angka yang valid.");
                return;
            }

            decimal Tunjangan;
            if (!decimal.TryParse(textBox1.Text.Trim(), out Tunjangan))
            {
                MessageBox.Show("Tunjangan harus berupa angka yang valid.");
                return;
            }

            string jabatan = dataGridView1.SelectedRows[0].Cells["Jabatan"].Value.ToString();

            using (MySqlConnection conn = koneksi.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                UPDATE gaji_jabatan 
                SET GajiPokok = @GajiPokok, Tunjangan = @Tunjangan 
                WHERE Jabatan = @Jabatan";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@GajiPokok", GajiPokok);
                    cmd.Parameters.AddWithValue("@Tunjangan", Tunjangan);
                    cmd.Parameters.AddWithValue("@Jabatan", jabatan); // ← Tambahan penting

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data berhasil diupdate!");
                    LoadGajiJabatan();
                    ClearTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            DeleteGajiJabatan();
        }
    }
}
