using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafesophia
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            LoadForm(new processsale());

        }
        private void LoadForm(Form form)
        {
            // Clear the panel first
           panelMain.Controls.Clear();

            // Set form properties so it behaves like part of the panel
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Add and show the form inside panelMain
            panelMain.Controls.Add(form);
            form.Show();
        }

  

        private void btnaddnew_Click(object sender, EventArgs e)
        {
            LoadForm(new addnew());
        }

        private void btnsale_Click(object sender, EventArgs e)
        {
            LoadForm(new sale());
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            LoadForm (new report());
        }

        private void btnhistory_Click(object sender, EventArgs e)
        {
            LoadForm(new history());
        }

        private void btnmanagement_Click(object sender, EventArgs e)
        {
            LoadForm (new management());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Environment.Exit (0);
        }

        private void btnprocess_Click(object sender, EventArgs e)
        {
            LoadForm(new processsale());
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
