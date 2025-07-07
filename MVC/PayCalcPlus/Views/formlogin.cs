using PayCalcPlus.Models;
using PayCalcPlus.Controllers;
using System.Windows.Forms;
using System;

namespace PayCalcPlus.Views
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                Username = textBoxUsername.Text.Trim(),
                Email = textBoxEmail.Text.Trim(),
                Password = textBoxPassword.Text.Trim()
            };

            LoginController controller = new LoginController();
            if (controller.Authenticate(user))
            {
                MessageBox.Show("Login berhasil!");
                dashboard dashboardForm = new dashboard();
                dashboardForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username, email, atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
