using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace cafesophia
{
    public static class DbSetup
    {
        // Call this once (e.g. application startup) to create sale items table if it doesn't exist.
        public static void EnsureSaleItemsTable()
        {
            const string createSql = @"
CREATE TABLE IF NOT EXISTS `tbl_sale_items` (
  `sale_item_id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `sale_id` INT NOT NULL,
  `item_id` INT NOT NULL,
  `item_name` VARCHAR(255) NOT NULL,
  `price` DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  `quantity` INT NOT NULL DEFAULT 0,
  `line_total` DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  INDEX `idx_sale_id` (`sale_id`),
  INDEX `idx_item_id` (`item_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;";
            try
            {
                DBConnection.Open();
                using (var cmd = new MySqlCommand(createSql, DBConnection.connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to ensure tbl_sale_items exists: " + ex.Message, "DB Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }
        }
    }
}