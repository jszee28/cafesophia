namespace cafesophia
{
    partial class history
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
            this.components = new System.ComponentModel.Container();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchSale = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnRefreshHistory = new Guna.UI2.WinForms.Guna2Button();
            this.lblHistoryStatus = new System.Windows.Forms.Label();
            this.guna2DateTimePicker1 = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnfilter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2ColorTransition1 = new Guna.UI2.WinForms.Guna2ColorTransition(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(30, 170);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersWidth = 62;
            this.dgvHistory.RowTemplate.Height = 28;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(1713, 815);
            this.dgvHistory.TabIndex = 0;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.label5);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(62)))), ((int)(((byte)(38)))));
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1755, 94);
            this.guna2Panel1.TabIndex = 82;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(701, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(390, 54);
            this.label5.TabIndex = 22;
            this.label5.Text = "Transaction History";
            // 
            // txtSearchSale
            // 
            this.txtSearchSale.AutoRoundedCorners = true;
            this.txtSearchSale.BorderRadius = 29;
            this.txtSearchSale.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchSale.DefaultText = "";
            this.txtSearchSale.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearchSale.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearchSale.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearchSale.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearchSale.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearchSale.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchSale.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearchSale.Location = new System.Drawing.Point(1168, 104);
            this.txtSearchSale.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchSale.Name = "txtSearchSale";
            this.txtSearchSale.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtSearchSale.PlaceholderText = "Search";
            this.txtSearchSale.SelectedText = "";
            this.txtSearchSale.Size = new System.Drawing.Size(332, 60);
            this.txtSearchSale.TabIndex = 83;
            this.txtSearchSale.TextChanged += new System.EventHandler(this.txtSearchSale_TextChanged);
            // 
            // btnRefreshHistory
            // 
            this.btnRefreshHistory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRefreshHistory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRefreshHistory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRefreshHistory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRefreshHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshHistory.ForeColor = System.Drawing.Color.White;
            this.btnRefreshHistory.Location = new System.Drawing.Point(1524, 104);
            this.btnRefreshHistory.Name = "btnRefreshHistory";
            this.btnRefreshHistory.Size = new System.Drawing.Size(203, 60);
            this.btnRefreshHistory.TabIndex = 84;
            this.btnRefreshHistory.Text = "Refresh";
            // 
            // lblHistoryStatus
            // 
            this.lblHistoryStatus.AutoSize = true;
            this.lblHistoryStatus.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistoryStatus.Location = new System.Drawing.Point(54, 122);
            this.lblHistoryStatus.Name = "lblHistoryStatus";
            this.lblHistoryStatus.Size = new System.Drawing.Size(98, 28);
            this.lblHistoryStatus.TabIndex = 86;
            this.lblHistoryStatus.Text = "Status :";
            this.lblHistoryStatus.Click += new System.EventHandler(this.lblHistoryStatus_Click);
            // 
            // guna2DateTimePicker1
            // 
            this.guna2DateTimePicker1.Checked = true;
            this.guna2DateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.guna2DateTimePicker1.Location = new System.Drawing.Point(520, 122);
            this.guna2DateTimePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.guna2DateTimePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.guna2DateTimePicker1.Name = "guna2DateTimePicker1";
            this.guna2DateTimePicker1.Size = new System.Drawing.Size(200, 36);
            this.guna2DateTimePicker1.TabIndex = 88;
            this.guna2DateTimePicker1.Value = new System.DateTime(2025, 11, 27, 23, 52, 1, 114);
            // 
            // btnfilter
            // 
            this.btnfilter.BackColor = System.Drawing.Color.Transparent;
            this.btnfilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.btnfilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.btnfilter.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnfilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnfilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnfilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.btnfilter.ItemHeight = 30;
            this.btnfilter.Location = new System.Drawing.Point(746, 122);
            this.btnfilter.Name = "btnfilter";
            this.btnfilter.Size = new System.Drawing.Size(156, 36);
            this.btnfilter.TabIndex = 89;
            // 
            // guna2ColorTransition1
            // 
            this.guna2ColorTransition1.ColorArray = new System.Drawing.Color[] {
        System.Drawing.Color.Red,
        System.Drawing.Color.Blue,
        System.Drawing.Color.Orange};
            // 
            // history
            // 
            this.AcceptButton = this.btnRefreshHistory;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1755, 1444);
            this.ControlBox = false;
            this.Controls.Add(this.btnfilter);
            this.Controls.Add(this.guna2DateTimePicker1);
            this.Controls.Add(this.lblHistoryStatus);
            this.Controls.Add(this.btnRefreshHistory);
            this.Controls.Add(this.txtSearchSale);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.dgvHistory);
            this.Name = "history";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistory;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox txtSearchSale;
        private Guna.UI2.WinForms.Guna2Button btnRefreshHistory;
        private System.Windows.Forms.Label lblHistoryStatus;
        private Guna.UI2.WinForms.Guna2DateTimePicker guna2DateTimePicker1;
        private Guna.UI2.WinForms.Guna2ComboBox btnfilter;
        private Guna.UI2.WinForms.Guna2ColorTransition guna2ColorTransition1;
    }
}