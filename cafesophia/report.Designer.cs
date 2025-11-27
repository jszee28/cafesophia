namespace cafesophia
{
    partial class report
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSummary = new System.Windows.Forms.DataGridView();
            this.btnLoadSummary = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoadLowStock = new Guna.UI2.WinForms.Guna2Button();
            this.btnlowstock = new Guna.UI2.WinForms.Guna2Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.label5);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(62)))), ((int)(((byte)(38)))));
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1560, 59);
            this.guna2Panel1.TabIndex = 83;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(671, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 37);
            this.label5.TabIndex = 22;
            this.label5.Text = "Report";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Linen;
            this.guna2Panel2.Controls.Add(this.lblTotalValue);
            this.guna2Panel2.Controls.Add(this.label4);
            this.guna2Panel2.Location = new System.Drawing.Point(1042, 105);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(412, 146);
            this.guna2Panel2.TabIndex = 87;
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Century Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalValue.Location = new System.Drawing.Point(26, 68);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(48, 55);
            this.lblTotalValue.TabIndex = 27;
            this.lblTotalValue.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(132, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 37);
            this.label4.TabIndex = 29;
            this.label4.Text = "Total Value";
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.BackColor = System.Drawing.Color.SaddleBrown;
            this.guna2Panel5.Controls.Add(this.lblTotalItems);
            this.guna2Panel5.Controls.Add(this.label1);
            this.guna2Panel5.ForeColor = System.Drawing.Color.White;
            this.guna2Panel5.Location = new System.Drawing.Point(461, 105);
            this.guna2Panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(316, 146);
            this.guna2Panel5.TabIndex = 84;
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Century Gothic", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalItems.ForeColor = System.Drawing.Color.Black;
            this.lblTotalItems.Location = new System.Drawing.Point(137, 71);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(48, 55);
            this.lblTotalItems.TabIndex = 27;
            this.lblTotalItems.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(70, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 37);
            this.label1.TabIndex = 28;
            this.label1.Text = "Total Items";
            // 
            // dgvSummary
            // 
            this.dgvSummary.AllowUserToAddRows = false;
            this.dgvSummary.AllowUserToDeleteRows = false;
            this.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSummary.Location = new System.Drawing.Point(48, 945);
            this.dgvSummary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSummary.Name = "dgvSummary";
            this.dgvSummary.ReadOnly = true;
            this.dgvSummary.RowHeadersWidth = 62;
            this.dgvSummary.RowTemplate.Height = 28;
            this.dgvSummary.Size = new System.Drawing.Size(1378, 219);
            this.dgvSummary.TabIndex = 90;
            // 
            // btnLoadSummary
            // 
            this.btnLoadSummary.AutoRoundedCorners = true;
            this.btnLoadSummary.BorderThickness = 2;
            this.btnLoadSummary.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadSummary.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadSummary.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoadSummary.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoadSummary.FillColor = System.Drawing.Color.Tan;
            this.btnLoadSummary.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnLoadSummary.ForeColor = System.Drawing.Color.White;
            this.btnLoadSummary.Location = new System.Drawing.Point(111, 875);
            this.btnLoadSummary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadSummary.Name = "btnLoadSummary";
            this.btnLoadSummary.Size = new System.Drawing.Size(324, 58);
            this.btnLoadSummary.TabIndex = 89;
            this.btnLoadSummary.Text = "SHOW LIST";
            this.btnLoadSummary.Click += new System.EventHandler(this.btnLoadSummary_Click);
            // 
            // btnLoadLowStock
            // 
            this.btnLoadLowStock.BorderRadius = 25;
            this.btnLoadLowStock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadLowStock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoadLowStock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoadLowStock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoadLowStock.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLoadLowStock.ForeColor = System.Drawing.Color.White;
            this.btnLoadLowStock.Location = new System.Drawing.Point(69, 774);
            this.btnLoadLowStock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadLowStock.Name = "btnLoadLowStock";
            this.btnLoadLowStock.Size = new System.Drawing.Size(0, 0);
            this.btnLoadLowStock.TabIndex = 88;
            this.btnLoadLowStock.Text = "Low Stock";
            this.btnLoadLowStock.Click += new System.EventHandler(this.btnLoadLowStock_Click);
            // 
            // btnlowstock
            // 
            this.btnlowstock.AutoRoundedCorners = true;
            this.btnlowstock.BorderThickness = 2;
            this.btnlowstock.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnlowstock.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnlowstock.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnlowstock.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnlowstock.FillColor = System.Drawing.Color.Tan;
            this.btnlowstock.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnlowstock.ForeColor = System.Drawing.Color.White;
            this.btnlowstock.Location = new System.Drawing.Point(453, 875);
            this.btnlowstock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnlowstock.Name = "btnlowstock";
            this.btnlowstock.Size = new System.Drawing.Size(324, 58);
            this.btnlowstock.TabIndex = 93;
            this.btnlowstock.Text = "LOW STOCK";
            this.btnlowstock.Click += new System.EventHandler(this.btnlowstock_Click);
            // 
            // chart1
            // 
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(111, 333);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(1343, 506);
            this.chart1.TabIndex = 94;
            this.chart1.Text = "chart1";
            // 
            // report
            // 
            this.AcceptButton = this.btnLoadLowStock;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1560, 995);
            this.ControlBox = false;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.btnlowstock);
            this.Controls.Add(this.dgvSummary);
            this.Controls.Add(this.btnLoadSummary);
            this.Controls.Add(this.btnLoadLowStock);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel5);
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "report";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel5.ResumeLayout(false);
            this.guna2Panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSummary;
        private Guna.UI2.WinForms.Guna2Button btnLoadSummary;
        private Guna.UI2.WinForms.Guna2Button btnLoadLowStock;
        private Guna.UI2.WinForms.Guna2Button btnlowstock;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}