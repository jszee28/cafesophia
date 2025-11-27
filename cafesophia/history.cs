using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cafesophia
{
    public partial class history : Form
    {
        public history()
        {
            InitializeComponent();
            this.Load += history_Load;

            // wire designer controls (they exist in your Designer)
            if (btnRefreshHistory != null) btnRefreshHistory.Click += BtnRefresh_Click;
            // txtSearchSale has an auto-wired event in Designer; keep a handler just in case
            // The Designer wires txtSearchSale.TextChanged to txtSearchSale_TextChanged already
        }

        private void history_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }

        // Minimal refresh button handler
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadHistory();
        }

        // Designer wired text changed; keep simple refresh behavior
        private void txtSearchSale_TextChanged(object sender, EventArgs e)
        {
            // keep minimal: refresh the grid when the search box changes
            LoadHistory();
        }

        // Checks if a column exists in a given table (safe, used to avoid unknown column errors)
        private bool HasColumn(string tableName, string columnName)
        {
            try
            {
                DBConnection.Open();
                using (var cmd = DBConnection.connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @table AND COLUMN_NAME = @column;";
                    cmd.Parameters.AddWithValue("@table", tableName);
                    cmd.Parameters.AddWithValue("@column", columnName);
                    var cnt = Convert.ToInt32(cmd.ExecuteScalar());
                    return cnt > 0;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Main simplified loader: binds one DataTable to dgvHistory with desired columns only
        private void LoadHistory()
        {
            try
            {
                // Determine optional columns to avoid referencing missing columns
                bool hasCustomerId = HasColumn("tbl_sales", "customer_id");
                bool hasAmountPaid = HasColumn("tbl_sales", "amount_paid");
                bool hasChangeAmount = HasColumn("tbl_sales", "change_amount");
                bool hasTotalAmount = HasColumn("tbl_sales", "total_amount"); // optional

                // build safe select expressions
                string customerExpr = hasCustomerId ? "t.customer_id" : "NULL AS customer_id";
                string amountPaidExpr = hasAmountPaid ? "COALESCE(t.amount_paid,0)" : "0";
                string changeExpr = hasChangeAmount ? "COALESCE(t.change_amount,0)" : "0";
                string totalExpr = hasTotalAmount ? "COALESCE(t.total_amount,0)" : "0";

                // search feature: simple text filter (search by sale_id or item name)
                var searchText = (txtSearchSale?.Text ?? "").Trim();
                bool useSearch = !string.IsNullOrEmpty(searchText);

                var sql = new StringBuilder();
                sql.AppendLine("SELECT");
                sql.AppendLine("  t.sale_id,");
                sql.AppendLine($"  {customerExpr},");
                sql.AppendLine("  t.sale_date AS date,");
                sql.AppendLine("  COALESCE(SUM(si.quantity),0) AS total_quantity,");
                sql.AppendLine("  COALESCE(GROUP_CONCAT(DISTINCT ii.item_name SEPARATOR ', '), '') AS item_names,");
                sql.AppendLine($"  {totalExpr} AS total_amount,");
                sql.AppendLine($"  {amountPaidExpr} AS amount_paid,");
                sql.AppendLine($"  {changeExpr} AS change_amount,");
                sql.AppendLine("  COALESCE(u.username, '') AS cashier_name");
                sql.AppendLine("FROM tbl_sales t");
                sql.AppendLine("LEFT JOIN tbl_sale_items si ON t.sale_id = si.sale_id");
                sql.AppendLine("LEFT JOIN tbl_inventory_items ii ON si.item_id = ii.item_id");
                sql.AppendLine("LEFT JOIN tbl_users u ON t.user_id = u.user_id");
                sql.AppendLine("WHERE 1=1");

                if (useSearch)
                {
                    // safe search: only reference customer_id cast if present (we used NULL AS customer_id above so CAST works)
                    sql.AppendLine("  AND (CAST(t.sale_id AS CHAR) LIKE @search OR ii.item_name LIKE @search)");
                }

                sql.AppendLine("GROUP BY t.sale_id");
                sql.AppendLine("ORDER BY t.sale_date DESC;");

                var dt = new DataTable();
                DBConnection.Open();
                using (var cmd = DBConnection.connection.CreateCommand())
                {
                    cmd.CommandText = sql.ToString();
                    if (useSearch) cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");

                    using (var da = new MySqlDataAdapter((MySqlCommand)cmd))
                    {
                        da.Fill(dt);
                    }
                }
                DBConnection.Close();

                // Post-process datatable to add combined Sale/Customer column and formatted currency columns
                if (!dt.Columns.Contains("SaleCustomer"))
                    dt.Columns.Add("SaleCustomer", typeof(string));

                if (!dt.Columns.Contains("AmountPaidDisplay"))
                    dt.Columns.Add("AmountPaidDisplay", typeof(string));

                if (!dt.Columns.Contains("ChangeDisplay"))
                    dt.Columns.Add("ChangeDisplay", typeof(string));

                foreach (DataRow r in dt.Rows)
                {
                    var saleId = r["sale_id"]?.ToString() ?? "";
                    var cust = r.Table.Columns.Contains("customer_id") && r["customer_id"] != DBNull.Value ? r["customer_id"].ToString() : "";
                    r["SaleCustomer"] = string.IsNullOrEmpty(cust) ? saleId : $"{saleId} / {cust}";

                    // amount_paid and change_amount may be numeric; convert to decimal safely
                    decimal paid = 0m;
                    decimal chg = 0m;
                    if (dt.Columns.Contains("amount_paid") && r["amount_paid"] != DBNull.Value)
                    {
                        Decimal.TryParse(r["amount_paid"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out paid);
                    }
                    if (dt.Columns.Contains("change_amount") && r["change_amount"] != DBNull.Value)
                    {
                        Decimal.TryParse(r["change_amount"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out chg);
                    }
                    r["AmountPaidDisplay"] = FormatCurrency(paid);
                    r["ChangeDisplay"] = FormatCurrency(chg);
                }

                // Bind the grid to a view of the table that orders columns as desired
                var view = new DataView(dt);

                dgvHistory.DataSource = null;
                dgvHistory.Columns.Clear();

                // Add and bind columns in the desired order
                dgvHistory.AutoGenerateColumns = false;

                void AddTextColumn(string colName, string header)
                {
                    dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = colName, Name = colName, HeaderText = header, ReadOnly = true });
                }

                AddTextColumn("SaleCustomer", "Sale ID / Customer ID");
                AddTextColumn("date", "Date");
                AddTextColumn("total_quantity", "Quantity");
                AddTextColumn("item_names", "Item Name(s)");
                AddTextColumn("AmountPaidDisplay", "Amount Paid");
                AddTextColumn("ChangeDisplay", "Change");
                AddTextColumn("cashier_name", "Cashier");

                dgvHistory.DataSource = view;

                // Format date column
                if (dgvHistory.Columns["date"] != null)
                {
                    dgvHistory.Columns["date"].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm tt";
                    dgvHistory.Columns["date"].Width = 160;
                }

                // final UI polish
                dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvHistory.ReadOnly = true;

                lblHistoryStatus.Text = $"Showing {dt.Rows.Count} rows";
            }
            catch (Exception ex)
            {
                try { DBConnection.Close(); } catch { }
                MessageBox.Show("Error loading transaction history: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatCurrency(decimal amount)
        {
            if (amount < 0) return "-" + string.Format("₱{0:N2}", Math.Abs(amount));
            return string.Format("₱{0:N2}", amount);
        }

        private void lblHistoryStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
