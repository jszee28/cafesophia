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
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
            LoadInventorySummary();
            LoadStatistics();
            LoadSalesSummary(); // populate sales panels
        }
        private void btnLoadLowStock_Click(object sender, EventArgs e)
        {
            LoadLowStockItems();
        }

        private void btnLoadSummary_Click(object sender, EventArgs e)
        {
            LoadInventorySummary();
            LoadSalesSummary(); // refresh sales when user clicks Summary
        }
        private void LoadLowStockItems()
        {
            try
            {
                DBConnection.Open();
                string query = @"SELECT item_name, current_stock, low_stock_alert 
                                 FROM tbl_inventory_items 
                                 WHERE current_stock <= low_stock_alert
                                 ORDER BY current_stock ASC";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvSummary.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("✅ No low stock items at the moment!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock items: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }
        private void LoadInventorySummary()
        {
            try
            {
                DBConnection.Open();
                string query = @"SELECT i.item_name, i.item_type, u.unit_name, i.current_stock, 
                                 i.cost_price, (i.current_stock * i.cost_price) AS total_value
                                 FROM tbl_inventory_items i
                                 LEFT JOIN tbl_units u ON i.unit_id = u.unit_id
                                 ORDER BY i.item_type ASC, i.item_name ASC";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvSummary.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory summary: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }
        private void LoadStatistics()
        {
            try
            {
                DBConnection.Open();

                // Count total items
                MySqlCommand cmdTotal = new MySqlCommand("SELECT COUNT(*) FROM tbl_inventory_items", DBConnection.connection);
                int totalItems = Convert.ToInt32(cmdTotal.ExecuteScalar());
                lblTotalItems.Text = $"{totalItems}";

                // Count raw materials
                MySqlCommand cmdRaw = new MySqlCommand("SELECT COUNT(*) FROM tbl_inventory_items WHERE item_type='RAW'", DBConnection.connection);
                int totalRaw = Convert.ToInt32(cmdRaw.ExecuteScalar());

                // Compute total inventory value (sum of cost * quantity)
                MySqlCommand cmdValue = new MySqlCommand("SELECT SUM(current_stock * cost_price) FROM tbl_inventory_items", DBConnection.connection);
                object result = cmdValue.ExecuteScalar();
                decimal totalValue = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;
                lblTotalValue.Text = $" ₱{totalValue:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading statistics: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // ----------------------
        // Sales summary methods
        // ----------------------
        // Assumes a sales table named 'tbl_sales' with:
        //   - sale_date (DATE or DATETIME)
        //   - total_amount (numeric/decimal)
        // If your schema uses different names, update the queries below.

        private decimal GetSalesSum(string whereClause)
        {
            try
            {
                DBConnection.Open();
                string sql = $"SELECT IFNULL(SUM(total_amount), 0) FROM tbl_sales WHERE {whereClause}";
                using (MySqlCommand cmd = new MySqlCommand(sql, DBConnection.connection))
                {
                    object r = cmd.ExecuteScalar();
                    if (r == null || r == DBNull.Value) return 0m;
                    return Convert.ToDecimal(r);
                }
            }
            finally
            {
                DBConnection.Close();
            }
        }

        private decimal GetDailySales()
        {
            return GetSalesSum("DATE(sale_date) = CURDATE()");
        }

        private decimal GetWeeklySales()
        {
            // Current ISO week (Monday=1). Adjust if you want last 7 days instead.
            return GetSalesSum("YEARWEEK(sale_date, 1) = YEARWEEK(CURDATE(), 1)");
        }

        private decimal GetMonthlySales()
        {
            return GetSalesSum("MONTH(sale_date) = MONTH(CURDATE()) AND YEAR(sale_date) = YEAR(CURDATE())");
        }

        private decimal GetYearlySales()
        {
            return GetSalesSum("YEAR(sale_date) = YEAR(CURDATE())");
        }

        // Populates the labels on the form
        private void LoadSalesSummary()
        {
            try
            {
                decimal daily = GetDailySales();
                decimal weekly = GetWeeklySales();
                decimal monthly = GetMonthlySales();
                decimal yearly = GetYearlySales();

                lbldailysale.Text = $"₱{daily:N2}";
                lblweeklysale.Text = $"₱{weekly:N2}";
                lblmonthlysale.Text = $"₱{monthly:N2}";
                lblyearlysale.Text = $"₱{yearly:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales summary: " + ex.Message);
            }
        }


        private void btnlowstock_Click(object sender, EventArgs e)
        {
            LoadLowStockItems();
        }
    }
}
