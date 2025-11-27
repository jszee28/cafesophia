using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic; // for Interaction.InputBox

namespace cafesophia
{
    public partial class addnew : Form
    {
        // runtime controls for image selection / preview
        private Label lblItemImage;
        private RadioButton rbDefaultImage;
        private RadioButton rbCustomImage;
        private Button btnBrowseImage;
        private PictureBox pbImagePreview;

        // temp path of chosen custom file (full path on disk)
        private string chosenCustomImagePath = null;

        // upload + defaults folders (full paths)
        private string uploadsFolderFullPath;
        private string defaultsFolderFullPath;

        public addnew()
        {
            InitializeComponent();

            // create image UI elements at runtime (don't change designer)
            CreateImageControls();

            // wire existing events
            this.Load += addnew_Load;
        }

        private void addnew_Load(object sender, EventArgs e)
        {
            // Ensure DB has item_image column and status column
            EnsureItemImageColumn();
            EnsureStatusColumn();

            LoadUnits();
            LoadInventory();

            // change item type options per requirement
            cmbItemType.Items.Clear();
            cmbItemType.Items.Add("Coffee");
            cmbItemType.Items.Add("Food");
            cmbItemType.Items.Add("Milktea");

            // prepare folders (relative to app startup)
            defaultsFolderFullPath = Path.Combine(Application.StartupPath, "images", "defaults");
            uploadsFolderFullPath = Path.Combine(Application.StartupPath, "images", "uploads");
            try
            {
                Directory.CreateDirectory(defaultsFolderFullPath);
                Directory.CreateDirectory(uploadsFolderFullPath);
            }
            catch
            {
                // ignore directory creation errors here; will surface on save if needed
            }

            // set default selections
            rbDefaultImage.Checked = true;
            cmbItemType.SelectedIndex = cmbItemType.Items.Count > 0 ? 0 : -1;

            // wire runtime events
            cmbItemType.SelectedIndexChanged += (s, ev) => { if (rbDefaultImage.Checked) UpdatePreviewForSelectedType(); };
            rbDefaultImage.CheckedChanged += (s, ev) => { UpdateControlsStateForImageSelection(); };
            rbCustomImage.CheckedChanged += (s, ev) => { UpdateControlsStateForImageSelection(); };
            btnBrowseImage.Click += BtnBrowseImage_Click;

            // wire existing designer buttons
            btnAdd.Click += btnAdd_Click;
            btnclear.Click += btnclear_Click;

            // wire inventory grid button clicks (safe to attach here)
            dgvInventory.CellContentClick -= dgvInventory_CellContentClick;
            dgvInventory.CellContentClick += dgvInventory_CellContentClick;

            // initial preview
            UpdatePreviewForSelectedType();
        }

        // --- create image UI controls programmatically so designer remains unchanged ---
        private void CreateImageControls()
        {
            // Locate position: place below txtItemName. Use txtItemName.Location + Height
            var baseX = txtItemName.Location.X;
            var baseY = txtItemName.Location.Y + txtItemName.Height + 8;

            // Label
            lblItemImage = new Label
            {
                Text = "Item Image:",
                Font = new Font("Century", 10F, FontStyle.Bold),
                Location = new Point(baseX, baseY),
                AutoSize = true
            };
            this.Controls.Add(lblItemImage);

            // Radio buttons
            rbDefaultImage = new RadioButton
            {
                Text = "Use Default Image",
                Location = new Point(baseX + 0, baseY + 26),
                AutoSize = true,
                Checked = true
            };
            this.Controls.Add(rbDefaultImage);

            rbCustomImage = new RadioButton
            {
                Text = "Upload Custom Image",
                Location = new Point(baseX + 170, baseY + 26),
                AutoSize = true
            };
            this.Controls.Add(rbCustomImage);

            // Browse button (enabled only when custom selected)
            btnBrowseImage = new Button
            {
                Text = "Browse...",
                Location = new Point(baseX + 360, baseY + 22),
                Size = new Size(90, 30),
                Enabled = false
            };
            this.Controls.Add(btnBrowseImage);

            // PictureBox preview (150x150)
            pbImagePreview = new PictureBox
            {
                Location = new Point(baseX + 480, baseY),
                Size = new Size(150, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };
            this.Controls.Add(pbImagePreview);
        }

        // enable/disable browse button and update preview when toggling default/custom
        private void UpdateControlsStateForImageSelection()
        {
            btnBrowseImage.Enabled = rbCustomImage.Checked;
            if (rbDefaultImage.Checked)
            {
                chosenCustomImagePath = null;
                UpdatePreviewForSelectedType();
            }
            else if (rbCustomImage.Checked)
            {
                // if previously selected custom image exists, show it
                if (!string.IsNullOrEmpty(chosenCustomImagePath) && File.Exists(chosenCustomImagePath))
                {
                    try { pbImagePreview.Image = Image.FromFile(chosenCustomImagePath); }
                    catch { pbImagePreview.Image = null; }
                }
                else
                {
                    pbImagePreview.Image = null;
                }
            }
        }

        // when Item Type changes and default image chosen, update preview
        private void UpdatePreviewForSelectedType()
        {
            if (!rbDefaultImage.Checked) return;

            string type = cmbItemType.SelectedItem?.ToString() ?? "";
            string filename = "coffee.png";
            switch (type.ToLower())
            {
                case "coffee":
                    filename = "coffee.png";
                    break;
                case "food":
                    filename = "food.png";
                    break;
                case "milktea":
                    filename = "milktea.png";
                    break;
                default:
                    filename = "coffee.png";
                    break;
            }

            string full = Path.Combine(defaultsFolderFullPath, filename);
            if (File.Exists(full))
            {
                try
                {
                    // dispose previous to avoid file lock
                    var old = pbImagePreview.Image;
                    pbImagePreview.Image = Image.FromFile(full);
                    old?.Dispose();
                }
                catch
                {
                    pbImagePreview.Image = null;
                }
            }
            else
            {
                // file doesn't exist in defaults; show null
                pbImagePreview.Image = null;
            }
        }

        // Browse custom image
        private void BtnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select product image";
                ofd.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    chosenCustomImagePath = ofd.FileName;
                    try
                    {
                        var old = pbImagePreview.Image;
                        pbImagePreview.Image = Image.FromFile(chosenCustomImagePath);
                        old?.Dispose();
                    }
                    catch
                    {
                        MessageBox.Show("Unable to load selected image.", "Image error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        pbImagePreview.Image = null;
                    }
                }
            }
        }

        // Ensure tbl_inventory_items has item_image column
        private void EnsureItemImageColumn()
        {
            try
            {
                DBConnection.Open();

                // check information_schema if column exists
                string checkSql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                    WHERE TABLE_SCHEMA = DATABASE()
                                      AND TABLE_NAME = 'tbl_inventory_items'
                                      AND COLUMN_NAME = 'item_image'";
                using (MySqlCommand cmd = new MySqlCommand(checkSql, DBConnection.connection))
                {
                    var exists = Convert.ToInt32(cmd.ExecuteScalar());
                    if (exists == 0)
                    {
                        // add column
                        string alter = "ALTER TABLE tbl_inventory_items ADD COLUMN item_image VARCHAR(255) NULL";
                        using (MySqlCommand a = new MySqlCommand(alter, DBConnection.connection))
                        {
                            a.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // don't block user - but inform developer
                MessageBox.Show("Warning: Could not ensure item_image column exists: " + ex.Message, "DB Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Ensure tbl_inventory_items has status column for soft-delete
        private void EnsureStatusColumn()
        {
            try
            {
                DBConnection.Open();

                string checkSql = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                                    WHERE TABLE_SCHEMA = DATABASE()
                                      AND TABLE_NAME = 'tbl_inventory_items'
                                      AND COLUMN_NAME = 'status'";
                using (MySqlCommand cmd = new MySqlCommand(checkSql, DBConnection.connection))
                {
                    var exists = Convert.ToInt32(cmd.ExecuteScalar());
                    if (exists == 0)
                    {
                        // add column with default 'Active'
                        string alter = "ALTER TABLE tbl_inventory_items ADD COLUMN status VARCHAR(20) DEFAULT 'Active'";
                        using (MySqlCommand a = new MySqlCommand(alter, DBConnection.connection))
                        {
                            a.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Warning: Could not ensure status column exists: " + ex.Message, "DB Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Modified LoadInventory to pull item_image as well and only show active items
        private void LoadInventory()
        {
            DBConnection.Open();
            try
            {
                string query = @"SELECT i.item_id, i.item_name, i.item_type, u.unit_name, 
                                 i.current_stock, i.cost_price, i.selling_price, i.low_stock_alert, i.item_image
                                 FROM tbl_inventory_items i
                                 LEFT JOIN tbl_units u ON i.unit_id = u.unit_id
                                 WHERE i.status = 'Active' OR i.status IS NULL
                                 ORDER BY i.item_name";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvInventory.DataSource = dt;

                // add button columns if not already present
                EnsureInventoryGridButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Ensure the Update/Delete button columns exist once
        private void EnsureInventoryGridButtons()
        {
            // avoid duplicates
            if (dgvInventory.Columns["btnUpdateCol"] == null)
            {
                var updateCol = new DataGridViewButtonColumn
                {
                    Name = "btnUpdateCol",
                    HeaderText = "",
                    Text = "Update Stock",
                    UseColumnTextForButtonValue = true,
                    Width = 100,
                    FlatStyle = FlatStyle.Standard
                };
                dgvInventory.Columns.Add(updateCol);
            }

            if (dgvInventory.Columns["btnDeleteCol"] == null)
            {
                var deleteCol = new DataGridViewButtonColumn
                {
                    Name = "btnDeleteCol",
                    HeaderText = "",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true,
                    Width = 100,
                    FlatStyle = FlatStyle.Standard
                };
                dgvInventory.Columns.Add(deleteCol);
            }

            // ensure they are the rightmost columns
            dgvInventory.Columns["btnUpdateCol"].DisplayIndex = dgvInventory.Columns.Count - 2;
            dgvInventory.Columns["btnDeleteCol"].DisplayIndex = dgvInventory.Columns.Count - 1;
        }   

        // Clear fields - extend to reset image selection & preview
        private void ClearFields()
        {
            txtItemName.Clear();
            cmbItemType.SelectedIndex = -1;
            cmbUnit.SelectedIndex = -1;
            txtCostPrice.Clear();
            txtSellingPrice.Clear();
            txtLowStock.Clear();

            // image controls reset
            rbDefaultImage.Checked = true;
            chosenCustomImagePath = null;
            UpdatePreviewForSelectedType();
        }

        // Modified btnAdd_Click to include image handling
        private void btnAdd_Click(object sender, EventArgs e)
        {
             DBConnection.Open();
            try
            {
                // validation
                if (string.IsNullOrWhiteSpace(txtItemName.Text) || cmbItemType.SelectedIndex == -1 || cmbUnit.SelectedIndex == -1)
                {
                    MessageBox.Show("Please complete all fields before saving.");
                    return;
                }

                // Get unit_id from unit name
                string unitQuery = "SELECT unit_id FROM tbl_units WHERE unit_name=@unit";
                MySqlCommand unitCmd = new MySqlCommand(unitQuery, DBConnection.connection);
                unitCmd.Parameters.AddWithValue("@unit", cmbUnit.Text);
                var unitIdObj = unitCmd.ExecuteScalar();
                if (unitIdObj == null)
                {
                    MessageBox.Show("Selected unit not found. Please select a valid unit.");
                    return;
                }
                int unitId = Convert.ToInt32(unitIdObj);

                // Determine image path to store
                string imagePathToStore = null;

                if (rbDefaultImage.Checked)
                {
                    // default relative path
                    string type = cmbItemType.Text?.ToLower() ?? "coffee";
                    string filename = "coffee.png";
                    if (type == "coffee") filename = "coffee.png";
                    else if (type == "food") filename = "food.png";
                    else if (type == "milktea") filename = "milktea.png";
                    imagePathToStore = $"/images/defaults/{filename}";
                }
                else if (rbCustomImage.Checked)
                {
                    if (string.IsNullOrEmpty(chosenCustomImagePath) || !File.Exists(chosenCustomImagePath))
                    {
                        MessageBox.Show("Please choose a valid image file to upload.", "Image required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // copy file to uploads folder with unique name
                    try
                    {
                        string ext = Path.GetExtension(chosenCustomImagePath).ToLower();
                        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                        {
                            MessageBox.Show("Invalid image format. Only JPG/JPEG/PNG are allowed.", "Invalid image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Directory.CreateDirectory(uploadsFolderFullPath);
                        string uniqueName = $"{Guid.NewGuid()}{ext}";
                        string destFull = Path.Combine(uploadsFolderFullPath, uniqueName);
                        File.Copy(chosenCustomImagePath, destFull);
                        imagePathToStore = $"/images/uploads/{uniqueName}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to upload image: " + ex.Message, "Upload error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // insert record with image path
                string insertQuery = @"INSERT INTO tbl_inventory_items 
                                       (item_name, item_type, unit_id, cost_price, selling_price, low_stock_alert, current_stock, item_image)
                                       VALUES (@name, @type, @unit, @cost, @sell, @low, @stock, @image)";
                MySqlCommand cmd = new MySqlCommand(insertQuery, DBConnection.connection);
                cmd.Parameters.AddWithValue("@name", txtItemName.Text.Trim());
                cmd.Parameters.AddWithValue("@type", cmbItemType.Text.Trim());
                cmd.Parameters.AddWithValue("@unit", unitId);
                cmd.Parameters.AddWithValue("@cost", string.IsNullOrWhiteSpace(txtCostPrice.Text) ? 0 : Convert.ToDecimal(txtCostPrice.Text));
                cmd.Parameters.AddWithValue("@sell", string.IsNullOrWhiteSpace(txtSellingPrice.Text) ? 0 : Convert.ToDecimal(txtSellingPrice.Text));
                cmd.Parameters.AddWithValue("@low", string.IsNullOrWhiteSpace(txtLowStock.Text) ? 0 : Convert.ToDecimal(txtLowStock.Text));
                // If there's a current_stock field and a quantity control, keep default 0 when adding new item.
                cmd.Parameters.AddWithValue("@stock", string.IsNullOrWhiteSpace(txtquantity.Text) ? 0 : Convert.ToDecimal(txtquantity.Text));
                cmd.Parameters.AddWithValue("@image", (object)imagePathToStore ?? DBNull.Value);

                cmd.ExecuteNonQuery();

                MessageBox.Show("✅ Item added successfully!");
                LoadInventory();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void LoadUnits()
        {
            DBConnection.Open();
            try
            {
                string query = "SELECT unit_name FROM tbl_units";
                MySqlCommand cmd = new MySqlCommand(query, DBConnection.connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                cmbUnit.Items.Clear();
                while (reader.Read())
                {
                    cmbUnit.Items.Add(reader["unit_name"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading units: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Handle clicks on Update Stock / Deactivate buttons in dgvInventory
        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var colName = dgvInventory.Columns[e.ColumnIndex].Name;
                var row = dgvInventory.Rows[e.RowIndex];

                if (row.Cells["item_id"].Value == null) return;
                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string itemName = row.Cells["item_name"].Value?.ToString() ?? "";

                if (colName == "btnUpdateCol")
                {
                    // Ask for new stock value using InputBox
                    string input = Interaction.InputBox($"Enter new stock quantity for '{itemName}':", "Update Stock", row.Cells["current_stock"].Value?.ToString() ?? "0");
                    if (string.IsNullOrWhiteSpace(input)) return;
                    if (!int.TryParse(input.Trim(), out int newStock) || newStock < 0)
                    {
                        MessageBox.Show("Please enter a valid non-negative integer for stock.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        DBConnection.Open();
                        string updateSql = "UPDATE tbl_inventory_items SET current_stock = @stock WHERE item_id = @id";
                        using (var cmd = new MySqlCommand(updateSql, DBConnection.connection))
                        {
                            cmd.Parameters.AddWithValue("@stock", newStock);
                            cmd.Parameters.AddWithValue("@id", itemId);
                            int affected = cmd.ExecuteNonQuery();
                            if (affected > 0)
                            {
                                MessageBox.Show("✅ Stock updated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Update failed. Item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating stock: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        DBConnection.Close();
                        LoadInventory();
                    }
                }
                else if (colName == "btnDeleteCol")
                {
                    var confirm = MessageBox.Show($"Are you sure you want to deactivate '{itemName}'?", "Confirm deactivate", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;

                    // Get image path for potential deletion
                    string imagePathStored = row.Cells["item_image"].Value == DBNull.Value ? null : row.Cells["item_image"].Value?.ToString();

                    try
                    {
                        DBConnection.Open();
                        string updateStatus = "UPDATE tbl_inventory_items SET status = 'Inactive' WHERE item_id = @id";
                        using (var delCmd = new MySqlCommand(updateStatus, DBConnection.connection))
                        {
                            delCmd.Parameters.AddWithValue("@id", itemId);
                            int affected = delCmd.ExecuteNonQuery();
                            if (affected > 0)
                            {
                                // attempt to delete uploaded file if stored under uploads
                                try
                                {
                                    if (!string.IsNullOrWhiteSpace(imagePathStored))
                                    {
                                        // typically stored as "/images/uploads/filename.ext"
                                        string filename = Path.GetFileName(imagePathStored.Replace('/', Path.DirectorySeparatorChar));
                                        if (!string.IsNullOrEmpty(filename))
                                        {
                                            string candidate = Path.Combine(uploadsFolderFullPath, filename);
                                            if (File.Exists(candidate))
                                            {
                                                File.Delete(candidate);
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    // ignore file delete failures (non-critical)
                                }

                                MessageBox.Show("Item deactivated successfully.");
                            }
                            else
                            {
                                MessageBox.Show("Deactivate failed. Item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deactivating item: " + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        DBConnection.Close();
                        LoadInventory();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error handling grid action: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
