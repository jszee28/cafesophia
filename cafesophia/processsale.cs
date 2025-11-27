using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cafesophia
{
    public partial class processsale : Form
    {
        private readonly CartManager _cartManager = new CartManager();
        private static readonly List<List<CartItem>> _heldOrders = new List<List<CartItem>>();

        private Control txtSearch;
        private Control txtAmountPaid;
        private Label lblTotal;
        private Label lblChange;         // shows customer change
        private Control btnPay;
        private Control btnCancelOrder;
        private Control btnHoldOrder;
        private Control panelProducts;
        private Control panelCheckout;
        private FlowLayoutPanel flowProducts;
        private Panel cartItemsPanel;
        private Label lblSubTotal;
        private Label lblTotalLarge;
        private Label lblCartCount;

        private string _currentCategory = "All";
        private string _currentSearch = "";

        private bool _isProcessingPayment = false; // tracks payment processing state

        public processsale()
        {
            InitializeComponent();
            Load += Processsale_Load;
        }

        private void Processsale_Load(object sender, EventArgs e)
        {
            try
            {
                MapControls();
                // ensure DB schema matches code expectations (safe runtime migration)
                EnsureSalesColumns();

                InitializeUI();
                _cartManager.CartChanged += (s, ev) => UpdateCartDisplay();
                LoadProducts();
                SetFocusToSearch();

                // initialize totals & change
                UpdateTotals();
                if (lblChange != null)
                {
                    lblChange.Text = FormatCurrency(0m);
                    lblChange.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapControls()
        {
            txtSearch = FindControlByNames(new[] { "txtSearch", "txtsearchitem" });
            panelProducts = FindControlByNames(new[] { "panelProducts", "panel1" });
            panelCheckout = FindControlByNames(new[] { "panelCheckout", "panel2" });
            txtAmountPaid = FindControlByNames(new[] { "txtAmountPaid", "txtamountpaid" });
            lblTotal = FindControlByNames(new[] { "lblTotal", "lbltotal" }) as Label;
            // map the designer's label4 (change placeholder) to lblChange
            lblChange = FindControlByNames(new[] { "lblChange", "label4", "lblchange" }) as Label;
            btnPay = FindControlByNames(new[] { "btnPay", "btnbayad" });
            btnCancelOrder = FindControlByNames(new[] { "btnCancelOrder", "btncancel" });
            btnHoldOrder = FindControlByNames(new[] { "btnHoldOrder", "btnhold" });

            flowProducts = panelProducts as FlowLayoutPanel;
            if (flowProducts == null && panelProducts != null)
            {
                flowProducts = panelProducts.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                if (flowProducts == null)
                {
                    flowProducts = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true, Padding = new Padding(8), FlowDirection = FlowDirection.LeftToRight };
                    panelProducts.Controls.Add(flowProducts);
                }
            }
        }

        private Control FindControlByNames(string[] names)
        {
            foreach (var n in names)
            {
                var arr = Controls.Find(n, true);
                if (arr != null && arr.Length > 0) return arr[0];
            }
            return null;
        }
        private string GetText(Control c) => c?.Text ?? string.Empty;
        private void SetFocusToSearch() { try { txtSearch?.Focus(); } catch { } }

        private void InitializeUI()
        {
            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) =>
                {
                    _currentSearch = GetText(txtSearch);
                    LoadProducts(_currentCategory, _currentSearch);
                };
            }

            if (panelCheckout == null) throw new InvalidOperationException("panelCheckout not found in designer.");

            cartItemsPanel = new Panel { AutoScroll = true, Location = new Point(5, 44), Size = new Size(panelCheckout.Width - 10, Math.Max(200, panelCheckout.Height - 380)), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackColor = Color.White };
            panelCheckout.Controls.Add(cartItemsPanel);

            // remove discount UI - no discount input

            lblSubTotal = new Label { Text = "Sub Total: ₱0.00", Location = new Point(8, panelCheckout.Height - 280), AutoSize = true };
            panelCheckout.Controls.Add(lblSubTotal);

            lblTotalLarge = new Label { Text = "TOTAL: ₱0.00", Location = new Point(8, panelCheckout.Height - 210), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold) };
            panelCheckout.Controls.Add(lblTotalLarge);

            lblCartCount = new Label { Text = "Cart: 0", Location = new Point(panelCheckout.Width - 80, 12), AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            panelCheckout.Controls.Add(lblCartCount);

            panelCheckout.SizeChanged += (s, e) =>
            {
                cartItemsPanel.Size = new Size(panelCheckout.Width - 10, Math.Max(200, panelCheckout.Height - 380));
                lblSubTotal.Location = new Point(8, panelCheckout.Height - 280);
                lblTotalLarge.Location = new Point(8, panelCheckout.Height - 210);
                lblCartCount.Location = new Point(panelCheckout.Width - 80, 12);
            };

            // NOTE: btnPay is wired in the designer to btnbayad_Click -> PayOrder().
            // Avoid attaching an extra runtime handler here to prevent double execution.
            // If designer does not wire the button in some builds, uncomment the guarded attach below.
            // if (btnPay != null) { if (!(btnPay is Button b && b.Tag?.ToString() == "payAttached")) { btnPay.Click += (s,e) => PayOrder(); if (btnPay is Button bx) bx.Tag = "payAttached"; } }

            if (btnCancelOrder != null) btnCancelOrder.Click += (s, e) => CancelOrder();
          //  if (btnHoldOrder != null) btnHoldOrder.Click += (s, e) => HoldOrder();

            // Attach amount paid handlers for live change + validation
            if (txtAmountPaid != null)
            {
                // KeyPress to restrict typed chars
                txtAmountPaid.KeyPress += AmountPaid_KeyPress;
                // TextChanged to sanitize pasted text and recalc
                txtAmountPaid.TextChanged += (s, e) =>
                {
                    SanitizeAmountPaidInput();
                    UpdateTotals();
                };
                // Optionally format on leave
                txtAmountPaid.Leave += (s, e) =>
                {
                    // normalize to 2 decimals if parseable
                    var raw = GetText(txtAmountPaid).Trim();
                    if (decimal.TryParse(raw, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var val))
                    {
                        txtAmountPaid.Text = val.ToString("F2", CultureInfo.InvariantCulture);
                    }
                };
            }
            AddCategoryButtons();
        }

        private void AddCategoryButtons()
        {
            if (panelProducts == null) return;
            var existing = panelProducts.Controls.OfType<FlowLayoutPanel>().FirstOrDefault(fl => fl.Name == "categoryPanel");
            if (existing != null) return;

            var categoryPanel = new FlowLayoutPanel { Name = "categoryPanel", Dock = DockStyle.Bottom, Height = 60, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(8), BackColor = Color.Transparent };
            panelProducts.Controls.Add(categoryPanel);
            categoryPanel.BringToFront();

            string[] cats = new[] { "All", "Coffee", "Food", "Milktea" };
            foreach (var c in cats)
            {
                var btn = new Button { Text = c, AutoSize = false, Size = new Size(90, 40), Tag = c, BackColor = c == "All" ? Color.LightBlue : SystemColors.Control };
                btn.Click += (s, e) =>
                {
                    foreach (Button b in categoryPanel.Controls.OfType<Button>()) b.BackColor = SystemColors.Control;
                    btn.BackColor = Color.LightBlue;
                    _currentCategory = c;
                    LoadProducts(_currentCategory, _currentSearch);
                };
                categoryPanel.Controls.Add(btn);
            }
        }

        private void LoadProducts() => LoadProducts("All", "");
        private void LoadProducts(string category, string search = "")
        {
            _currentCategory = category ?? "All";
            _currentSearch = search ?? "";

            if (flowProducts == null && panelProducts != null)
            {
                flowProducts = panelProducts as FlowLayoutPanel;
                if (flowProducts == null)
                {
                    flowProducts = panelProducts.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                    if (flowProducts == null)
                    {
                        flowProducts = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true, Padding = new Padding(8), FlowDirection = FlowDirection.LeftToRight };
                        panelProducts.Controls.Add(flowProducts);
                    }
                }
            }

            flowProducts.SuspendLayout();
            flowProducts.Controls.Clear();

            try
            {
                DBConnection.Open();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = DBConnection.connection;
                    var sql = @"SELECT item_id, item_name, item_type, selling_price, current_stock, item_image 
                                FROM tbl_inventory_items
                                WHERE (status = 'Active' OR status IS NULL) AND current_stock > 0";
                    if (!string.IsNullOrWhiteSpace(category) && category != "All")
                    {
                        sql += " AND item_type = @type";
                        cmd.Parameters.AddWithValue("@type", category);
                    }
                    if (!string.IsNullOrWhiteSpace(search))
                    {
                        sql += " AND item_name LIKE @search";
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    }

                    cmd.CommandText = sql;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int itemId = reader.GetInt32("item_id");
                            string itemName = reader.GetString("item_name");
                            decimal price = reader.GetDecimal("selling_price");
                            int stock = reader.GetInt32("current_stock");
                            string image = reader.IsDBNull(reader.GetOrdinal("item_image")) ? "" : reader.GetString("item_image");

                            var card = ProductCardBuilder.CreateCard(itemId, itemName, price, stock, image, (id, name, pr, st) =>
                            {
                                var existing = _cartManager.Items.FirstOrDefault(x => x.ItemId == id);
                                if (existing != null && existing.Quantity + 1 > existing.Stock)
                                {
                                    MessageBox.Show("Cannot add more. Stock limit reached.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (existing == null && st <= 0)
                                {
                                    MessageBox.Show("Item is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                _cartManager.AddItem(id, name, pr, st);
                            });

                            flowProducts.Controls.Add(card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }

            flowProducts.ResumeLayout();
        }

        private void UpdateCartDisplay()
        {
            cartItemsPanel.Controls.Clear();
            int y = 4;
            int rowHeight = 36;

            foreach (var item in _cartManager.Items)
            {
                var row = CreateCartRow(item, cartItemsPanel.Width, rowHeight);
                row.Location = new Point(4, y);
                cartItemsPanel.Controls.Add(row);
                y += rowHeight + 6;
            }

            UpdateTotals();
            lblCartCount.Text = "Cart: " + _cartManager.Count;
            SetPayEnabled(_cartManager.Count > 0);
        }

        private Panel CreateCartRow(CartItem item, int width, int height)
        {
            var row = new Panel { Size = new Size(width - 28, height), Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top, BackColor = Color.WhiteSmoke };

            var lblName = new Label { Text = item.ItemName, Location = new Point(4, 4), Size = new Size(160, 24), AutoEllipsis = true };
            row.Controls.Add(lblName);

            var btnMinus = CreateButton("-", new Point(170, 4), () => _cartManager.DecreaseQuantity(item.ItemId));
            row.Controls.Add(btnMinus);

            var lblQty = new Label { Text = item.Quantity.ToString(), Location = new Point(202, 4), Size = new Size(36, 24), TextAlign = ContentAlignment.MiddleCenter };
            row.Controls.Add(lblQty);

            var btnPlus = CreateButton("+", new Point(244, 4), () =>
            {
                var existing = _cartManager.Items.FirstOrDefault(x => x.ItemId == item.ItemId);
                if (existing != null && existing.Quantity + 1 > existing.Stock)
                {
                    MessageBox.Show("Cannot increase quantity. Stock limit reached.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _cartManager.IncreaseQuantity(item.ItemId);
            });
            row.Controls.Add(btnPlus);

            var lblPrice = new Label { Text = string.Format("₱{0:N2}", item.UnitPrice), Location = new Point(282, 4), Size = new Size(80, 24), TextAlign = ContentAlignment.MiddleLeft };
            row.Controls.Add(lblPrice);

            var lblSubtotal = new Label { Text = string.Format("₱{0:N2}", item.Subtotal), Location = new Point(362, 4), Size = new Size(100, 24), TextAlign = ContentAlignment.MiddleLeft };
            row.Controls.Add(lblSubtotal);

            var btnDelete = CreateButton("×", new Point(row.Width - 36, 4), () => _cartManager.RemoveItem(item.ItemId));
            btnDelete.ForeColor = Color.Red;
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            row.Controls.Add(btnDelete);

            return row;
        }

        private Button CreateButton(string text, Point location, Action onClick)
        {
            var b = new Button { Text = text, Location = location, Size = new Size(28, 24) };
            b.Click += (s, e) => onClick?.Invoke();
            return b;
        }

        private void UpdateTotals()
        {
            decimal subtotal = _cartManager.CalculateSubtotal();

            decimal total = subtotal;

            lblSubTotal.Text = "Sub Total: " + FormatCurrency(subtotal);
            lblTotalLarge.Text = "TOTAL: " + FormatCurrency(total);

            if (lblTotal != null) lblTotal.Text = FormatCurrency(total);

            // update change display if amount paid is present
            if (txtAmountPaid != null && lblChange != null)
            {
                var paidText = GetText(txtAmountPaid).Trim();

                if (decimal.TryParse(paidText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var paid))
                {
                    var change = paid - total;
                    lblChange.Text = FormatCurrency(change);
                    lblChange.ForeColor = change < 0 ? Color.Red : Color.Black;
                }
                else
                {
                    // show zero (or negative state) when input invalid/empty
                    lblChange.Text = FormatCurrency(0m);
                    lblChange.ForeColor = Color.Black;
                }
            }
        }

        private void SetPayEnabled(bool enabled) { if (btnPay != null) btnPay.Enabled = enabled; }
        private string FormatCurrency(decimal amount)
        {
            // Always show 2 decimals; negative displayed as -₱X.YY
            if (amount < 0m) return "-" + string.Format("₱{0:N2}", Math.Abs(amount));
            return string.Format("₱{0:N2}", amount);
        }

        private void PayOrder()
        {
            try
            {
                // prevent re-entrancy (double click or duplicate handlers)
                if (_isProcessingPayment) return;
                _isProcessingPayment = true;
                SetPayEnabled(false); // disable pay UI while processing

                if (_cartManager.Count == 0)
                {
                    MessageBox.Show("Cart is empty. Please add items before paying.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtAmountPaid == null)
                {
                    MessageBox.Show("Amount Paid control not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                decimal amountPaid;
                if (!decimal.TryParse(GetText(txtAmountPaid).Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out amountPaid))
                {
                    MessageBox.Show("Invalid Amount Paid. Please enter a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal subtotal = _cartManager.CalculateSubtotal();
                decimal total = subtotal;

                if (amountPaid < total)
                {
                    MessageBox.Show("Amount paid must be equal to or greater than the total.", "Insufficient Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal change = amountPaid - total;
                DBConnection.Open();
                using (var transaction = DBConnection.connection.BeginTransaction())
                {
                    try
                    {
                        long saleId;
                        using (var cmd = DBConnection.connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            // INSERT only columns expected to exist: sale_date, total_amount, amount_paid, change_amount
                            cmd.CommandText = @"INSERT INTO tbl_sales (sale_date, total_amount, amount_paid, change_amount)
                                                VALUES (NOW(), @total, @paid, @change);";
                            cmd.Parameters.AddWithValue("@total", total);
                            cmd.Parameters.AddWithValue("@paid", amountPaid);
                            cmd.Parameters.AddWithValue("@change", change);
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "SELECT LAST_INSERT_ID();";
                            cmd.Parameters.Clear();
                            saleId = Convert.ToInt64(cmd.ExecuteScalar());
                        }

                        foreach (var item in _cartManager.Items)
                        {
                            using (var cmd = DBConnection.connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                // Include item_name to satisfy NOT NULL column and preserve the sold name
                                cmd.CommandText = @"INSERT INTO tbl_sale_items (sale_id, item_id, item_name, quantity, price, subtotal)
                                                    VALUES (@saleId, @itemId, @itemName, @qty, @price, @subtotal);";
                                cmd.Parameters.AddWithValue("@saleId", saleId);
                                cmd.Parameters.AddWithValue("@itemId", item.ItemId);
                                cmd.Parameters.AddWithValue("@itemName", item.ItemName ?? string.Empty);
                                cmd.Parameters.AddWithValue("@qty", item.Quantity);
                                cmd.Parameters.AddWithValue("@price", item.UnitPrice);
                                cmd.Parameters.AddWithValue("@subtotal", item.Subtotal);
                                cmd.ExecuteNonQuery();
                            }

                            using (var cmd = DBConnection.connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"UPDATE tbl_inventory_items 
                                                    SET current_stock = current_stock - @qty
                                                    WHERE item_id = @itemId;";
                                cmd.Parameters.AddWithValue("@qty", item.Quantity);
                                cmd.Parameters.AddWithValue("@itemId", item.ItemId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();

                        MessageBox.Show($"Payment successful! Change: {FormatCurrency(change)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // show change in UI
                        if (lblChange != null)
                        {
                            lblChange.Text = FormatCurrency(change);
                            lblChange.ForeColor = change < 0 ? Color.Red : Color.Black;
                        }

                        try
                        {
                            var receipt = new ReceiptData
                            {
                                Items = _cartManager.CloneCart(),
                                Total = total,
                                AmountPaid = amountPaid,
                                Change = change,
                                Date = DateTime.Now
                            };
                            var printer = new ReceiptPrinter(receipt);
                            printer.Print();
                            _cartManager.Clear();
                            LoadProducts(_currentCategory, _currentSearch);
                            return;
                        }
                        catch (Exception pex)
                        {
                            MessageBox.Show("Printing failed: " + pex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        try { transaction.Rollback(); } catch { }
                        MessageBox.Show("Transaction failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        DBConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Payment failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isProcessingPayment = false;
                SetPayEnabled(_cartManager.Count > 0);
            }
        }
        private void CancelOrder()
        {
            var result = MessageBox.Show("Are you sure you want to cancel this order?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _cartManager.Clear();
                if (txtSearch != null) txtSearch.Text = "";
                if (txtAmountPaid != null) txtAmountPaid.Text = "";
                if (lblChange != null) lblChange.Text = FormatCurrency(0m);
                SetFocusToSearch();
            }
        }
        private void btnbayad_Click(object sender, EventArgs e)
        {
            PayOrder();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // label4 mapped to lblChange (no action required)
        }
        // ---------- New helpers for numeric validation & schema safety ----------
        private void AmountPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control characters (backspace), digits, and a single decimal point
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar)) return;

            if (e.KeyChar == '.')
            {
                var tb = sender as TextBox;
                if (tb != null)
                {
                    if (!tb.Text.Contains(".")) return;
                }
            }
            // otherwise block
            e.Handled = true;
        }
        private void SanitizeAmountPaidInput()
        {
            var tb = txtAmountPaid as TextBox;
            if (tb == null) return;
            var original = tb.Text;
            if (string.IsNullOrEmpty(original)) return;

            // keep digits, decimal separators, minus (though minus is normally not used)
            var cleaned = Regex.Replace(original, @"[^0-9\.\,\-]", "");

            // prefer '.' as decimal separator for invariant parsing: replace commas with dots
            cleaned = cleaned.Replace(',', '.');

            // collapse multiple dots to a single dot (keep first)
            int firstDot = cleaned.IndexOf('.');
            if (firstDot >= 0)
            {
                var before = cleaned.Substring(0, firstDot + 1);
                var after = cleaned.Substring(firstDot + 1).Replace(".", "");
                cleaned = before + after;
            }

            if (cleaned != original)
            {
                var sel = Math.Max(0, tb.SelectionStart - (original.Length - cleaned.Length));
                tb.Text = cleaned;
                tb.SelectionStart = Math.Min(tb.Text.Length, sel);
            }
        }

        // At startup ensure the amount_paid and change_amount columns exist; if not, add them.
        // This is a safe runtime migration fallback to prevent "Unknown column ... in 'field list'".
        private void EnsureSalesColumns()
        {
            try
            {
                DBConnection.Open();
                using (var cmd = DBConnection.connection.CreateCommand())
                {
                    // Check amount_paid column
                    cmd.CommandText = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tbl_sales' AND COLUMN_NAME = 'amount_paid';";
                    var amountPaidCount = Convert.ToInt32(cmd.ExecuteScalar());

                    // Check change_amount column
                    cmd.CommandText = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tbl_sales' AND COLUMN_NAME = 'change_amount';";
                    var changeAmountCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (amountPaidCount == 0)
                    {
                        using (var alter = DBConnection.connection.CreateCommand())
                        {
                            alter.CommandText = "ALTER TABLE tbl_sales ADD COLUMN amount_paid DECIMAL(12,2) NOT NULL DEFAULT 0.00 AFTER total_amount;";
                            alter.ExecuteNonQuery();
                        }
                    }
                    if (changeAmountCount == 0)
                    {
                        using (var alter = DBConnection.connection.CreateCommand())
                        {
                            alter.CommandText = "ALTER TABLE tbl_sales ADD COLUMN change_amount DECIMAL(12,2) NOT NULL DEFAULT 0.00 AFTER amount_paid;";
                            alter.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                // Swallow exceptions here so UI still loads; apply migration manually if this fails.
            }
            finally
            {
                DBConnection.Close();
            }
        }
    }
}
