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

namespace cafesophia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection.Open();

                string query = "SELECT * FROM tbl_users WHERE username=@username AND password=@password";
                MySqlCommand cmd = new MySqlCommand(query, DBConnection.connection);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Open main form here
                     dashboard form = new dashboard();
                    this.Hide();
                    form.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    reader.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        private void cbshowpass_Click(object sender, EventArgs e)
        {
            if (cbshowpass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;  // show plain text
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;   // hide password
            }
        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
                }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}
