using PayCalcPlus.Controllers;
using PayCalcPlus.Models;
using System.Windows.Forms;
using System;

private void button3_Click(object sender, EventArgs e)
{
    var user = new UserModel
    {
        Username = textBox2.Text.Trim(),
        Email = textBox3.Text.Trim(),
        Password = textBox1.Text.Trim()
    };

    if (AuthController.Login(user))
    {
        MessageBox.Show("Login berhasil!");
        new DashboardForm().Show();
        this.Hide();
    }
    else
    {
        MessageBox.Show("Login gagal!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
