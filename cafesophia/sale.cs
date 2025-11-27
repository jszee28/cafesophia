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
using Microsoft.VisualBasic; // for Interaction.InputBox

namespace cafesophia
{
    public partial class sale : Form
    {
        // cart backing table
        private DataTable cartDt;
        // selected product details from product list
        private int selectedProductId = -1;
        private string selectedProductName = "";
        private decimal selectedProductPrice = 0m;
        private int selectedProductStock = 0;

        public sale()
        {
            InitializeComponent();

            // wire buttons and fields
            btnAddToCart.Click += BtnAddToCart_Click;
            btnProcessSale.Click += BtnProcessSale_Click;
            btnCancel.Click += BtnCancel_Click;
            txtAmountPaid.TextChanged += TxtAmountPaid_TextChanged;
            dgvlistofproduct.CellDoubleClick += Dgvlistofproduct_CellDoubleClick;

            InitCart();
        }

        private void sale_Load(object sender, EventArgs e)
        {
            LoadProducts();
            if (cmbMovementType.Items.Count > 0)
                cmbMovementType.SelectedIndex = 0;
            UpdateCartTotals();
        }

        // -------------------------
        // Products list (uses tbl_inventory_items)
        // -------------------------
        private void LoadProducts()
        {
            try
            {
                DBConnection.Open();
                string sql = @"SELECT item_id, item_name, selling_price AS price, IFNULL(current_stock,0) AS current_stock
                               FROM tbl_inventory_items
                               ORDER BY item_name";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, DBConnection.connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvlistofproduct.DataSource = dt;

                // friendly column headers
                if (dgvlistofproduct.Columns["item_id"] != null)
                    dgvlistofproduct.Columns["item_id"].HeaderText = "ID";
                if (dgvlistofproduct.Columns["item_name"] != null)
                    dgvlistofproduct.Columns["item_name"].HeaderText = "Product";
                if (dgvlistofproduct.Columns["price"] != null)
                    dgvlistofproduct.Columns["price"].HeaderText = "Price";
                if (dgvlistofproduct.Columns["current_stock"] != null)
                    dgvlistofproduct.Columns["current_stock"].HeaderText = "Stock";
            }
            catch (MySqlException mex)
            {
                if (mex.Number == 1146) // table doesn't exist
                {
                    MessageBox.Show("Table 'tbl_inventory_items' not found. Expected columns: item_id, item_name, selling_price, current_stock.\n\n" +
                                    "Create or point code to your inventory table.", "Missing table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Error loading products: " + mex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // double-click product to select
        private void Dgvlistofproduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvlistofproduct.Rows[e.RowIndex];
            if (row.Cells["item_id"].Value == null) return;

            selectedProductId = Convert.ToInt32(row.Cells["item_id"].Value);
            selectedProductName = row.Cells["item_name"].Value?.ToString() ?? "";
            selectedProductPrice = row.Cells["price"].Value != null ? Convert.ToDecimal(row.Cells["price"].Value) : 0m;
            selectedProductStock = row.Cells["current_stock"].Value != null ? Convert.ToInt32(row.Cells["current_stock"].Value) : 0;

            // show selected in UI
            itemchosen.Text = selectedProductName;
            label4.Text = selectedProductStock.ToString(); // using existing label4 as stock display
        }

        // -------------------------
        // Cart operations
        // -------------------------
        private void InitCart()
        {
            cartDt = new DataTable();
            cartDt.Columns.Add("product_id", typeof(int));
            cartDt.Columns.Add("product_name", typeof(string));
            cartDt.Columns.Add("price", typeof(decimal));
            cartDt.Columns.Add("quantity", typeof(int));
            cartDt.Columns.Add("line_total", typeof(decimal), "price * quantity");

            dataGridViewCart.DataSource = cartDt;
            // hide id column if present
            if (dataGridViewCart.Columns["product_id"] != null)
                dataGridViewCart.Columns["product_id"].Visible = false;
            if (dataGridViewCart.Columns["price"] != null)
                dataGridViewCart.Columns["price"].DefaultCellStyle.Format = "N2";
            if (dataGridViewCart.Columns["line_total"] != null)
                dataGridViewCart.Columns["line_total"].DefaultCellStyle.Format = "N2";
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            // Validate selection
            if (selectedProductId <= 0)
            {
                MessageBox.Show("Select a product (double-click product row) before adding to cart.", "No product selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ask quantity (quick approach; replace with NumericUpDown if preferred)
            string input = Interaction.InputBox($"Enter quantity for '{selectedProductName}':", "Quantity", "1");
            if (string.IsNullOrWhiteSpace(input)) return;

            if (!int.TryParse(input.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Quantity must be a positive integer.", "Invalid quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check stock warning (inventory may be 0 for services)
            if (selectedProductStock >= 0 && qty > selectedProductStock)
            {
                var ok = MessageBox.Show($"Requested quantity ({qty}) exceeds current stock ({selectedProductStock}). Continue anyway?", "Low stock", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ok != DialogResult.Yes) return;
            }

            AddOrUpdateCartItem(selectedProductId, selectedProductName, selectedProductPrice, qty);

            UpdateCartTotals();
        }

        private void AddOrUpdateCartItem(int productId, string productName, decimal price, int qty)
        {
            // if already in cart, increment
            DataRow[] found = cartDt.Select($"product_id = {productId}");
            DataRow existing = found.Length > 0 ? found[0] : null;
            if (existing != null)
            {
                int current = Convert.ToInt32(existing["quantity"]);
                existing["quantity"] = current + qty;
            }
            else
            {
                DataRow r = cartDt.NewRow();
                r["product_id"] = productId;
                r["product_name"] = productName;
                r["price"] = price;
                r["quantity"] = qty;
                cartDt.Rows.Add(r);
            }
        }

        private void UpdateCartTotals()
        {
            decimal subtotal = 0m;
            foreach (DataRow r in cartDt.Rows)
            {
                subtotal += Convert.ToDecimal(r["line_total"]);
            }

            lblSubtotalValue.Text = $"{subtotal:N2}";

            // For now subtotal == total; you can add taxes/fees later
            lblTotalValue.Text = $"{subtotal:N2}";

            // Update change if amount paid entered
            ComputeChange();
        }

        // -------------------------
        // Payment & change
        // -------------------------
        private void TxtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            ComputeChange();
        }

        private void ComputeChange()
        {
            decimal paid = 0m;
            decimal total = 0m;
            decimal.TryParse(lblTotalValue.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out total);
            if (!decimal.TryParse(txtAmountPaid.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out paid))
            {
                lblChangeValue.Text = "0.00";
                return;
            }

            decimal change = paid - total;
            lblChangeValue.Text = $"{change:N2}";
        }

        // -------------------------
        // Process / Save Sale
        // -------------------------
        private void BtnProcessSale_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate cart
                if (cartDt.Rows.Count == 0)
                {
                    MessageBox.Show("Cart is empty. Add items before processing sale.", "Empty cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate amount paid
                if (!decimal.TryParse(lblTotalValue.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out decimal totalAmount))
                {
                    MessageBox.Show("Invalid total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtAmountPaid.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out decimal paid))
                {
                    MessageBox.Show("Enter a valid numeric payment amount.", "Invalid payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (paid < totalAmount)
                {
                    MessageBox.Show("Amount paid is less than total due.", "Insufficient payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save sale and sale lines and update stock inside a single transaction
                long saleId = 0;
                try
                {
                    DBConnection.Open();
                    using (var tr = DBConnection.connection.BeginTransaction())
                    {
                        // insert sale
                        string insertSale = @"INSERT INTO tbl_sales (sale_date, total_amount, payment_method, notes)
                                              VALUES (@date, @total, @payment_method, @notes)";
                        using (MySqlCommand cmd = new MySqlCommand(insertSale, DBConnection.connection, tr))
                        {
                            cmd.Parameters.AddWithValue("@date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@total", totalAmount);
                            cmd.Parameters.AddWithValue("@payment_method", cmbMovementType.Text ?? "");
                            cmd.Parameters.AddWithValue("@notes", txtRemarks.Text ?? "");
                            cmd.ExecuteNonQuery();
                            saleId = cmd.LastInsertedId;
                        }

                        // attempt to insert sale lines into tbl_sale_items if exists; always update stock
                        string insertItemSql = @"INSERT INTO tbl_sale_items (sale_id, item_id, item_name, price, quantity, line_total)
                                                 VALUES (@sale_id, @item_id, @item_name, @price, @qty, @line_total)";
                        using (MySqlCommand itemCmd = new MySqlCommand(insertItemSql, DBConnection.connection, tr))
                        {
                            itemCmd.Parameters.Add("@sale_id", MySqlDbType.Int64).Value = saleId;
                            itemCmd.Parameters.Add("@item_id", MySqlDbType.Int32);
                            itemCmd.Parameters.Add("@item_name", MySqlDbType.VarChar);
                            itemCmd.Parameters.Add("@price", MySqlDbType.Decimal);
                            itemCmd.Parameters.Add("@qty", MySqlDbType.Int32);
                            itemCmd.Parameters.Add("@line_total", MySqlDbType.Decimal);

                            foreach (DataRow r in cartDt.Rows)
                            {
                                int itemId = Convert.ToInt32(r["product_id"]);
                                string name = r["product_name"].ToString();
                                decimal price = Convert.ToDecimal(r["price"]);
                                int qty = Convert.ToInt32(r["quantity"]);
                                decimal line = Convert.ToDecimal(r["line_total"]);

                                // update stock
                                string updateStockSql = "UPDATE tbl_inventory_items SET current_stock = current_stock - @qty WHERE item_id = @id";
                                using (MySqlCommand us = new MySqlCommand(updateStockSql, DBConnection.connection, tr))
                                {
                                    us.Parameters.AddWithValue("@qty", qty);
                                    us.Parameters.AddWithValue("@id", itemId);
                                    us.ExecuteNonQuery();
                                }

                                // try insert sale item; if table missing, catch and continue (stock already updated)
                                try
                                {
                                    itemCmd.Parameters["@item_id"].Value = itemId;
                                    itemCmd.Parameters["@item_name"].Value = name;
                                    itemCmd.Parameters["@price"].Value = price;
                                    itemCmd.Parameters["@qty"].Value = qty;
                                    itemCmd.Parameters["@line_total"].Value = line;
                                    itemCmd.ExecuteNonQuery();
                                }
                                catch (MySqlException mex)
                                {
                                    if (mex.Number == 1146)
                                    {
                                        // missing tbl_sale_items; skip inserting lines but continue
                                        // show once
                                        MessageBox.Show("Optional table 'tbl_sale_items' not found. Sale saved without detailed lines.", "Missing table", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        // remove catch spam by breaking from further attempts to insert lines
                                        break;
                                    }
                                    else
                                    {
                                        throw;
                                    }
                                }
                            }
                        }

                        tr.Commit();
                    }
                }
                finally
                {
                    DBConnection.Close();
                }

                if (saleId <= 0)
                {
                    MessageBox.Show("Saving sale failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Generate receipt text and ask to save / print
                string receipt = BuildReceiptText(saleId, totalAmount, paid, paid - totalAmount);
                var sfd = new SaveFileDialog();
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FileName = $"receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, receipt, Encoding.UTF8);
                    MessageBox.Show("Sale processed and receipt saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // still inform success even if user cancelled saving receipt
                    MessageBox.Show("Sale processed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Clear form
                ClearSaleForm();
                // reload products to reflect stock changes
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing sale: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------
        // Receipt building
        // -------------------------
        private string BuildReceiptText(long saleId, decimal total, decimal paid, decimal change)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Sophia's Cafe");
            sb.AppendLine("Sales Receipt");
            sb.AppendLine($"Sale ID: {saleId}");
            sb.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine("Item                          Qty   Price     Total");
            sb.AppendLine(new string('-', 40));
            foreach (DataRow r in cartDt.Rows)
            {
                string name = r["product_name"].ToString();
                int qty = Convert.ToInt32(r["quantity"]);
                decimal price = Convert.ToDecimal(r["price"]);
                decimal line = Convert.ToDecimal(r["line_total"]);
                sb.AppendLine($"{name.PadRight(30).Substring(0, Math.Min(30, name.Length))} {qty.ToString().PadLeft(3)} {price.ToString("N2").PadLeft(8)} {line.ToString("N2").PadLeft(9)}");
            }
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"TOTAL: {total:N2}");
            sb.AppendLine($"PAID:  {paid:N2}");
            sb.AppendLine($"CHANGE:{change:N2}");
            sb.AppendLine(new string('-', 40));
            sb.AppendLine($"Remarks: {txtRemarks.Text}");
            sb.AppendLine($"Movement: {cmbMovementType.Text}");
            sb.AppendLine();
            sb.AppendLine("Thank you for your purchase!");
            return sb.ToString();
        }

        // -------------------------
        // Cancel / Clear
        // -------------------------
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var ans = MessageBox.Show("Clear the current sale?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans != DialogResult.Yes) return;
            ClearSaleForm();
        }

        private void ClearSaleForm()
        {
            cartDt.Rows.Clear();
            txtAmountPaid.Clear();
            txtRemarks.Clear();
            itemchosen.Text = "";
            label4.Text = "";
            selectedProductId = -1;
            UpdateCartTotals();
        }

        private void btnProcessSale_Click_1(object sender, EventArgs e)
        {

        }
    }
}
