using PayCalcPlus.Controllers;
using PayCalcPlus.Models;
using System.Windows.Forms;
using System;

private void button1_Click(object sender, EventArgs e)
{
    var user = new UserModel
    {
        Username = textBox3.Text.Trim(),
        Email = textBox2.Text.Trim(),
        Password = textBox1.Text.Trim()
    };

    if (AuthController.Register(user))
    {
        MessageBox.Show("Registrasi berhasil!");
        new LoginForm().Show();
        this.Close();
    }
    else
    {
        MessageBox.Show("Username atau email sudah terdaftar.");
    }
}
