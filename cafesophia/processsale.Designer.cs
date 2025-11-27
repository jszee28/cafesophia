namespace cafesophia
{
    partial class processsale
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtsearchitem = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblchange = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtamountpaid = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbltotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btncancel = new Guna.UI2.WinForms.Guna2Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.btnbayad = new Guna.UI2.WinForms.Guna2Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 1132);
            this.panel1.TabIndex = 0;
            // 
            // txtsearchitem
            // 
            this.txtsearchitem.AutoRoundedCorners = true;
            this.txtsearchitem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtsearchitem.DefaultText = "";
            this.txtsearchitem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtsearchitem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtsearchitem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtsearchitem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtsearchitem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtsearchitem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtsearchitem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtsearchitem.Location = new System.Drawing.Point(4, 5);
            this.txtsearchitem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtsearchitem.Name = "txtsearchitem";
            this.txtsearchitem.PlaceholderText = "Search Item Here..";
            this.txtsearchitem.SelectedText = "";
            this.txtsearchitem.Size = new System.Drawing.Size(240, 61);
            this.txtsearchitem.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtsearchitem);
            this.panel2.Controls.Add(this.lblchange);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtamountpaid);
            this.panel2.Controls.Add(this.lbltotal);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(876, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(683, 1132);
            this.panel2.TabIndex = 1;
            // 
            // lblchange
            // 
            this.lblchange.AutoSize = true;
            this.lblchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblchange.Location = new System.Drawing.Point(545, 1088);
            this.lblchange.Name = "lblchange";
            this.lblchange.Size = new System.Drawing.Size(134, 32);
            this.lblchange.TabIndex = 10;
            this.lblchange.Text = "(change)";
            this.lblchange.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 1088);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 32);
            this.label3.TabIndex = 9;
            this.label3.Text = "Change";
            // 
            // txtamountpaid
            // 
            this.txtamountpaid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtamountpaid.DefaultText = "";
            this.txtamountpaid.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtamountpaid.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtamountpaid.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtamountpaid.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtamountpaid.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtamountpaid.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamountpaid.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtamountpaid.Location = new System.Drawing.Point(2, 999);
            this.txtamountpaid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtamountpaid.Name = "txtamountpaid";
            this.txtamountpaid.PlaceholderText = "Amount Paid";
            this.txtamountpaid.SelectedText = "";
            this.txtamountpaid.Size = new System.Drawing.Size(677, 70);
            this.txtamountpaid.TabIndex = 8;
            // 
            // lbltotal
            // 
            this.lbltotal.AutoSize = true;
            this.lbltotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotal.Location = new System.Drawing.Point(565, 962);
            this.lbltotal.Name = "lbltotal";
            this.lbltotal.Size = new System.Drawing.Size(94, 32);
            this.lbltotal.TabIndex = 7;
            this.lbltotal.Text = "(total)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 962);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(293, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "Checkout";
            // 
            // btncancel
            // 
            this.btncancel.BorderThickness = 2;
            this.btncancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btncancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btncancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btncancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btncancel.FillColor = System.Drawing.Color.Transparent;
            this.btncancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.Red;
            this.btncancel.Location = new System.Drawing.Point(79, 1189);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(198, 86);
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "Cancel Order";
            // 
            // btnbayad
            // 
            this.btnbayad.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnbayad.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnbayad.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnbayad.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnbayad.FillColor = System.Drawing.Color.Lime;
            this.btnbayad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbayad.ForeColor = System.Drawing.Color.Black;
            this.btnbayad.Location = new System.Drawing.Point(340, 1189);
            this.btnbayad.Name = "btnbayad";
            this.btnbayad.Size = new System.Drawing.Size(215, 86);
            this.btnbayad.TabIndex = 5;
            this.btnbayad.Text = "Process Order";
            this.btnbayad.Click += new System.EventHandler(this.btnbayad_Click);
            // 
            // processsale
            // 
            this.AcceptButton = this.btnbayad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1755, 1410);
            this.ControlBox = false;
            this.Controls.Add(this.btnbayad);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "processsale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        //     private Guna.UI2.WinForms.Guna2Button btnPay;
        private Guna.UI2.WinForms.Guna2Button btncancel;
        private Guna.UI2.WinForms.Guna2TextBox txtsearchitem;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtamountpaid;
        private System.Windows.Forms.Label lbltotal;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblchange;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button btnbayad;
    }
}