using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace cafesophia
{
    internal class EnsureSaleItemsColumns
    {
        // call this from Processsale_Load after EnsureSalesColumns();
        public static void EnsureColumns()
        {
            try
            {
                DBConnection.Open();

                // Collect existing columns for tbl_sale_items
                var existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var cmd = DBConnection.connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
                                        WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'tbl_sale_items';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            existing.Add(reader.GetString(0));
                        }
                    }
                }

                // Add unit_price if missing
                if (!existing.Contains("unit_price"))
                {
                    using (var alter = DBConnection.connection.CreateCommand())
                    {
                        alter.CommandText = "ALTER TABLE tbl_sale_items ADD COLUMN unit_price DECIMAL(12,2) NOT NULL DEFAULT 0.00;";
                        alter.ExecuteNonQuery();
                    }
                }

                // Add subtotal if missing (defensive)
                if (!existing.Contains("subtotal"))
                {
                    using (var alter = DBConnection.connection.CreateCommand())
                    {
                        alter.CommandText = "ALTER TABLE tbl_sale_items ADD COLUMN subtotal DECIMAL(12,2) NOT NULL DEFAULT 0.00;";
                        alter.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException mex)
            {
                // ignore duplicate column error; show other SQL errors for diagnostics
                if (mex.Number != 1060)
                {
                    MessageBox.Show("DB migration (tbl_sale_items) failed: " + mex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // keep quiet in production but surface during dev if needed
#if DEBUG
                MessageBox.Show("EnsureSaleItemsColumns failed: " + ex.Message, "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // --- New helper: show current columns in a dialog for quick verification ---
        public static void ShowColumnsDialog()
        {
            try
            {
                DBConnection.Open();
                using (var cmd = DBConnection.connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE, COLUMN_DEFAULT
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_SCHEMA = 'sophia_inventory' AND TABLE_NAME = 'tbl_sale_items'
                        ORDER BY ORDINAL_POSITION;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var sb = new StringBuilder();
                        while (reader.Read())
                        {
                            var name = reader.IsDBNull(0) ? "" : reader.GetString(0);
                            var type = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            var nullable = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            var def = reader.IsDBNull(3) ? "NULL" : reader.GetValue(3).ToString();
                            sb.AppendFormat("{0}  |  {1}  |  nullable:{2}  |  default:{3}\r\n", name, type, nullable, def);
                        }

                        var output = sb.Length == 0 ? "No columns found or table 'tbl_sale_items' does not exist." : sb.ToString();
                        MessageBox.Show(output, "tbl_sale_items columns", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to read tbl_sale_items columns: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }
        }
    }
}
