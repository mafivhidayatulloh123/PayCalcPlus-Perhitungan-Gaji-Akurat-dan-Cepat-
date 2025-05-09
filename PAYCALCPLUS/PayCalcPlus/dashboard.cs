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
    public partial class dashboard : Form
    {
        private string connString = "Server=localhost;Database=paycalcplus;Uid=root;Pwd=;";

        public dashboard()
        {
            InitializeComponent();
        }

        private void LoadDashboardData()
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    k.KodeKaryawan, 
                    k.NamaKaryawan, 
                    k.Jabatan, 
                    g.GajiPokok,
                    g.Tunjangan,
                    g.GajiBersih
                FROM karyawan k
                INNER JOIN gaji_jabatan g ON k.Jabatan = g.Jabatan
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Optional: ubah header kolom
                    dataGridView1.Columns["KodeKaryawan"].HeaderText = "Kode Karyawan";
                    dataGridView1.Columns["NamaKaryawan"].HeaderText = "Nama Karyawan";
                    dataGridView1.Columns["Jabatan"].HeaderText = "Jabatan";
                    dataGridView1.Columns["GajiPokok"].HeaderText = "Gaji Pokok";
                    dataGridView1.Columns["Tunjangan"].HeaderText = "Tunjangan";
                    dataGridView1.Columns["GajiBersih"].HeaderText = "Gaji Bersih";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data: " + ex.Message);
                }
            }
        }


        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            data_gaji formGaji = new data_gaji();
            formGaji.Show();
            this.Hide();
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

        private void dashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
