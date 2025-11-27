namespace cafesophia
{
    partial class sale
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
            this.dgvlistofproduct = new System.Windows.Forms.DataGridView();
            this.dataGridViewCart = new System.Windows.Forms.DataGridView();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnProcessSale = new Guna.UI2.WinForms.Guna2Button();
            this.lblChangeValue = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.txtAmountPaid = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.btnAddToCart = new Guna.UI2.WinForms.Guna2Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.txtRemarks = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbMovementType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.itemchosen = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlistofproduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCart)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvlistofproduct
            // 
            this.dgvlistofproduct.AllowUserToAddRows = false;
            this.dgvlistofproduct.AllowUserToDeleteRows = false;
            this.dgvlistofproduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvlistofproduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlistofproduct.Location = new System.Drawing.Point(0, 433);
            this.dgvlistofproduct.Name = "dgvlistofproduct";
            this.dgvlistofproduct.ReadOnly = true;
            this.dgvlistofproduct.RowHeadersWidth = 62;
            this.dgvlistofproduct.RowTemplate.Height = 28;
            this.dgvlistofproduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvlistofproduct.Size = new System.Drawing.Size(1091, 390);
            this.dgvlistofproduct.TabIndex = 80;
            // 
            // dataGridViewCart
            // 
            this.dataGridViewCart.AllowUserToAddRows = false;
            this.dataGridViewCart.AllowUserToDeleteRows = false;
            this.dataGridViewCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCart.Location = new System.Drawing.Point(2, 957);
            this.dataGridViewCart.Name = "dataGridViewCart";
            this.dataGridViewCart.ReadOnly = true;
            this.dataGridViewCart.RowHeadersWidth = 62;
            this.dataGridViewCart.RowTemplate.Height = 28;
            this.dataGridViewCart.Size = new System.Drawing.Size(1089, 250);
            this.dataGridViewCart.TabIndex = 79;
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1137, 614);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(221, 58);
            this.btnCancel.TabIndex = 70;
            this.btnCancel.Text = "CANCEL";
            // 
            // btnProcessSale
            // 
            this.btnProcessSale.BorderRadius = 8;
            this.btnProcessSale.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnProcessSale.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnProcessSale.ForeColor = System.Drawing.Color.White;
            this.btnProcessSale.Location = new System.Drawing.Point(1137, 549);
            this.btnProcessSale.Name = "btnProcessSale";
            this.btnProcessSale.Size = new System.Drawing.Size(221, 59);
            this.btnProcessSale.TabIndex = 69;
            this.btnProcessSale.Text = "PROCESS SALE";
            this.btnProcessSale.Click += new System.EventHandler(this.btnProcessSale_Click_1);
            // 
            // lblChangeValue
            // 
            this.lblChangeValue.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold);
            this.lblChangeValue.Location = new System.Drawing.Point(1133, 453);
            this.lblChangeValue.Name = "lblChangeValue";
            this.lblChangeValue.Size = new System.Drawing.Size(105, 31);
            this.lblChangeValue.TabIndex = 71;
            this.lblChangeValue.Text = "0.00";
            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold);
            this.lblChange.Location = new System.Drawing.Point(1133, 428);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(160, 25);
            this.lblChange.TabIndex = 72;
            this.lblChange.Text = "CHANGE:";
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.BorderRadius = 5;
            this.txtAmountPaid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAmountPaid.DefaultText = "";
            this.txtAmountPaid.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAmountPaid.Location = new System.Drawing.Point(1138, 362);
            this.txtAmountPaid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.PlaceholderText = "";
            this.txtAmountPaid.SelectedText = "";
            this.txtAmountPaid.Size = new System.Drawing.Size(220, 36);
            this.txtAmountPaid.TabIndex = 73;
            // 
            // lblAmountPaid
            // 
            this.lblAmountPaid.Font = new System.Drawing.Font("Century", 10F, System.Drawing.FontStyle.Bold);
            this.lblAmountPaid.Location = new System.Drawing.Point(1138, 337);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(100, 23);
            this.lblAmountPaid.TabIndex = 74;
            this.lblAmountPaid.Text = "Amount Paid (Cash):";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Century", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.Location = new System.Drawing.Point(1132, 245);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(75, 33);
            this.lblTotalValue.TabIndex = 75;
            this.lblTotalValue.Text = "0.00";
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(1132, 220);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(202, 25);
            this.lblTotal.TabIndex = 76;
            this.lblTotal.Text = "TOTAL DUE:";
            // 
            // lblSubtotalValue
            // 
            this.lblSubtotalValue.AutoSize = true;
            this.lblSubtotalValue.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold);
            this.lblSubtotalValue.Location = new System.Drawing.Point(1133, 124);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(62, 28);
            this.lblSubtotalValue.TabIndex = 77;
            this.lblSubtotalValue.Text = "0.00";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Century", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.Location = new System.Drawing.Point(1133, 99);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(104, 23);
            this.lblSubtotal.TabIndex = 78;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BorderRadius = 8;
            this.btnAddToCart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(846, 208);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(178, 57);
            this.btnAddToCart.TabIndex = 68;
            this.btnAddToCart.Text = "Add to Cart";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Century", 11F);
            this.dtpTo.Location = new System.Drawing.Point(981, 20);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(407, 34);
            this.dtpTo.TabIndex = 66;
            // 
            // txtRemarks
            // 
            this.txtRemarks.BorderColor = System.Drawing.Color.Black;
            this.txtRemarks.BorderRadius = 5;
            this.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRemarks.DefaultText = "";
            this.txtRemarks.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtRemarks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRemarks.Location = new System.Drawing.Point(8, 225);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.PlaceholderText = "Note / Remarks";
            this.txtRemarks.SelectedText = "";
            this.txtRemarks.Size = new System.Drawing.Size(790, 36);
            this.txtRemarks.TabIndex = 64;
            // 
            // cmbMovementType
            // 
            this.cmbMovementType.BackColor = System.Drawing.Color.Transparent;
            this.cmbMovementType.BorderColor = System.Drawing.Color.Black;
            this.cmbMovementType.BorderRadius = 5;
            this.cmbMovementType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMovementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMovementType.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbMovementType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbMovementType.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.cmbMovementType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbMovementType.ItemHeight = 30;
            this.cmbMovementType.Items.AddRange(new object[] {
            "Take Out",
            "Dine in"});
            this.cmbMovementType.Location = new System.Drawing.Point(561, 143);
            this.cmbMovementType.Name = "cmbMovementType";
            this.cmbMovementType.Size = new System.Drawing.Size(240, 36);
            this.cmbMovementType.TabIndex = 62;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.label5);
            this.guna2Panel1.Controls.Add(this.dtpTo);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(62)))), ((int)(((byte)(38)))));
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1559, 64);
            this.guna2Panel1.TabIndex = 81;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 45);
            this.label5.TabIndex = 22;
            this.label5.Text = "POS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 28);
            this.label6.TabIndex = 82;
            this.label6.Text = "Items";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(567, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 28);
            this.label1.TabIndex = 83;
            this.label1.Text = "Movement";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(278, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 28);
            this.label2.TabIndex = 84;
            this.label2.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 28);
            this.label3.TabIndex = 85;
            this.label3.Text = "Note";
            // 
            // itemchosen
            // 
            this.itemchosen.AutoSize = true;
            this.itemchosen.Location = new System.Drawing.Point(17, 19);
            this.itemchosen.Name = "itemchosen";
            this.itemchosen.Size = new System.Drawing.Size(0, 20);
            this.itemchosen.TabIndex = 86;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.itemchosen);
            this.panel1.Location = new System.Drawing.Point(69, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 55);
            this.panel1.TabIndex = 87;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(283, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(156, 55);
            this.panel2.TabIndex = 88;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 20);
            this.label4.TabIndex = 86;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 401);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 28);
            this.label7.TabIndex = 90;
            this.label7.Text = "List of items\r\n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 926);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(202, 28);
            this.label8.TabIndex = 91;
            this.label8.Text = "Customers Cart";
            // 
            // sale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1559, 1255);
            this.ControlBox = false;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.dgvlistofproduct);
            this.Controls.Add(this.dataGridViewCart);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnProcessSale);
            this.Controls.Add(this.lblChangeValue);
            this.Controls.Add(this.lblChange);
            this.Controls.Add(this.txtAmountPaid);
            this.Controls.Add(this.lblAmountPaid);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblSubtotalValue);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.cmbMovementType);
            this.Name = "sale";
            this.Load += new System.EventHandler(this.sale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlistofproduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCart)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvlistofproduct;
        private System.Windows.Forms.DataGridView dataGridViewCart;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnProcessSale;
        private System.Windows.Forms.Label lblChangeValue;
        private System.Windows.Forms.Label lblChange;
        private Guna.UI2.WinForms.Guna2TextBox txtAmountPaid;
        private System.Windows.Forms.Label lblAmountPaid;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Label lblSubtotal;
        private Guna.UI2.WinForms.Guna2Button btnAddToCart;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private Guna.UI2.WinForms.Guna2TextBox txtRemarks;
        private Guna.UI2.WinForms.Guna2ComboBox cmbMovementType;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label itemchosen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}