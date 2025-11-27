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
            btnaddnew.BackColor = Color.FromArgb(240, 245, 248);
            btnaddnew.ForeColor = Color.Black;

            btnreport.BackColor = Color.FromArgb(107, 62, 38);
            btnreport.ForeColor = Color.White;

            btnhistory.BackColor = Color.FromArgb(107, 62, 38);
            btnhistory.ForeColor = Color.White;

            btnmanagement.BackColor = Color.FromArgb(107, 62, 38);
            btnmanagement.ForeColor = Color.White;

            btnprocess.BackColor = Color.FromArgb(107, 62, 38);
            btnprocess.ForeColor = Color.White;
            LoadForm(new addnew());
        }

        private void btnsale_Click(object sender, EventArgs e)
        { // click Event Active color

            btnaddnew.BackColor = Color.FromArgb(240, 245, 248);
            btnaddnew.ForeColor = Color.Black;

            btnreport.BackColor = Color.FromArgb(107, 62, 38);
            btnreport.ForeColor = Color.White;

            btnhistory.BackColor = Color.FromArgb(107, 62, 38);
            btnhistory.ForeColor = Color.White;

            btnmanagement.BackColor = Color.FromArgb(107, 62, 38);
            btnmanagement.ForeColor = Color.White;

            btnprocess.BackColor = Color.FromArgb(107, 62, 38);
            btnprocess.ForeColor = Color.White;
            LoadForm(new sale());
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            btnaddnew.BackColor = Color.FromArgb(107, 62, 38);
            btnaddnew.ForeColor = Color.White;
            // click Event Active color
            btnreport.BackColor = Color.FromArgb(240, 245, 248);
            btnreport.ForeColor = Color.Black;
            
            btnhistory.BackColor = Color.FromArgb(107, 62, 38);
            btnhistory.ForeColor = Color.White;

            btnmanagement.BackColor = Color.FromArgb(107, 62, 38);
            btnmanagement.ForeColor = Color.White;

            btnprocess.BackColor = Color.FromArgb(107, 62, 38);
            btnprocess.ForeColor = Color.White;
            LoadForm (new report());
        }

        private void btnhistory_Click(object sender, EventArgs e)
        {
            btnaddnew.BackColor = Color.FromArgb(107, 62, 38);
            btnaddnew.ForeColor = Color.White;

            btnreport.BackColor = Color.FromArgb(107, 62, 38);
            btnreport.ForeColor = Color.White;
            // click Event Active color
            btnhistory.BackColor = Color.FromArgb(240, 245, 248);
            btnhistory.ForeColor = Color.Black;

            btnmanagement.BackColor = Color.FromArgb(107, 62, 38);
            btnmanagement.ForeColor = Color.White;

            btnprocess.BackColor = Color.FromArgb(107, 62, 38);
            btnprocess.ForeColor = Color.White;
            LoadForm(new history());
        }

        private void btnmanagement_Click(object sender, EventArgs e)
        {
            btnaddnew.BackColor = Color.FromArgb(107, 62, 38);
            btnaddnew.ForeColor = Color.White;

            btnreport.BackColor = Color.FromArgb(107, 62, 38);
            btnreport.ForeColor = Color.White;

            btnhistory.BackColor = Color.FromArgb(107, 62, 38);
            btnhistory.ForeColor = Color.White;
            // click Event Active color
            btnmanagement.BackColor = Color.FromArgb(240, 245, 248);
            btnmanagement.ForeColor = Color.Black;

            btnprocess.BackColor = Color.FromArgb(107, 62, 38);
            btnprocess.ForeColor = Color.White;

            LoadForm (new management());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Environment.Exit (0);
        }

        private void btnprocess_Click(object sender, EventArgs e)
        {
            //Default back
            btnaddnew.BackColor = Color.FromArgb(107, 62, 38);
            btnaddnew.ForeColor = Color.White;

            btnreport.BackColor = Color.FromArgb(107, 62, 38);
            btnreport.ForeColor = Color.White;

            btnhistory.BackColor = Color.FromArgb(107, 62, 38);
            btnhistory.ForeColor = Color.White;

            btnmanagement.BackColor = Color.FromArgb(107, 62, 38);
            btnmanagement.ForeColor = Color.White;

            // click Event Active color
            btnprocess.BackColor = Color.FromArgb(240, 245, 248);
            btnprocess.ForeColor = Color.Black;

            LoadForm(new processsale());

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
