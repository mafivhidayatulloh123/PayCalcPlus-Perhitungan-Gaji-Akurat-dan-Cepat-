using System;
using System.Windows.Forms;
using PayCalcPlus.Models;
using PayCalcPlus.Controllers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PayCalcPlus.Views
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            User newUser = new User()
            {
                Username = textBox3.Text.Trim(),
                Email = textBox2.Text.Trim(),
                Password = textBox1.Text.Trim()
            };

            LoginController controller = new LoginController();
            if (controller.Register(newUser, out string message))
            {
                MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Registrasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
