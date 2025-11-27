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
    public partial class management : Form
    {
        private int selectedUserId = -1;

        public management()
        {
            InitializeComponent();
        }
 
        private void LoadUsers()
        {
            try
            {
                DBConnection.Open();
                string query = "SELECT user_id, username, user_role FROM tbl_users";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, DBConnection.connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvUsers.DataSource = dt;

                EnsureGridButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Add/refresh Update/Delete button columns on right side
        private void EnsureGridButtons()
        {
            // Remove existing if present
            if (dgvUsers.Columns["btnUpdateCol"] != null)
                dgvUsers.Columns.Remove("btnUpdateCol");
            if (dgvUsers.Columns["btnDeleteCol"] != null)
                dgvUsers.Columns.Remove("btnDeleteCol");

            // Update button
            DataGridViewButtonColumn updateCol = new DataGridViewButtonColumn();
            updateCol.Name = "btnUpdateCol";
            updateCol.HeaderText = "";
            updateCol.Text = "Edit";
            updateCol.UseColumnTextForButtonValue = true;
            updateCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            updateCol.Width = 80;
            dgvUsers.Columns.Add(updateCol);

            // Delete button
            DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
            deleteCol.Name = "btnDeleteCol";
            deleteCol.HeaderText = "";
            deleteCol.Text = "Delete";
            deleteCol.UseColumnTextForButtonValue = true;
            deleteCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteCol.Width = 80;
            dgvUsers.Columns.Add(deleteCol);

            // Make them appear at the far right
            updateCol.DisplayIndex = dgvUsers.ColumnCount - 2;
            deleteCol.DisplayIndex = dgvUsers.ColumnCount - 1;
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
            selectedUserId = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                DBConnection.Open();

                string checkUser = "SELECT COUNT(*) FROM tbl_users WHERE username=@username";
                MySqlCommand checkCmd = new MySqlCommand(checkUser, DBConnection.connection);
                checkCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (exists > 0)
                {
                    MessageBox.Show("❌ Username already exists!");
                    return;
                }

                string query = "INSERT INTO tbl_users (username, password, user_role) VALUES (@username, @password, @role)";
                MySqlCommand cmd = new MySqlCommand(query, DBConnection.connection);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text); // stored in plain text (future: hash)
                cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("✅ User added successfully!");
                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        private void management_Load(object sender, EventArgs e)
        {
            LoadUsers();
            cmbRole.Items.Add("Owner");
            cmbRole.Items.Add("Staff");

            // Wire grid events
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            dgvUsers.CellClick += dgvUsers_CellClick;
        }

        // When user clicks a row (non-button), populate fields and set selected user id
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                var row = dgvUsers.Rows[e.RowIndex];

                if (row.Cells["user_id"].Value != null)
                {
                    selectedUserId = Convert.ToInt32(row.Cells["user_id"].Value);
                    txtUsername.Text = row.Cells["username"].Value?.ToString();
                    // password not retrieved for security; clear it so user can set a new one
                    txtPassword.Clear();
                    if (row.Cells["user_role"].Value != null)
                    {
                        var role = row.Cells["user_role"].Value.ToString();
                        int idx = cmbRole.Items.IndexOf(role);
                        cmbRole.SelectedIndex = idx >= 0 ? idx : -1;
                    }
                }
            }
            catch
            {
                // ignore
            }
        }

        // Handle clicks on the Update / Delete buttons embedded in the grid
        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string colName = dgvUsers.Columns[e.ColumnIndex].Name;
            var row = dgvUsers.Rows[e.RowIndex];
            if (row.Cells["user_id"].Value == null) return;
            int userId = Convert.ToInt32(row.Cells["user_id"].Value);

            if (colName == "btnUpdateCol")
            {
                // populate the form fields for editing and set selectedUserId
                selectedUserId = userId;
                txtUsername.Text = row.Cells["username"].Value?.ToString();
                txtPassword.Clear(); // require user to re-enter password if they want to change it
                if (row.Cells["user_role"].Value != null)
                {
                    var role = row.Cells["user_role"].Value.ToString();
                    int idx = cmbRole.Items.IndexOf(role);
                    cmbRole.SelectedIndex = idx >= 0 ? idx : -1;
                }
                MessageBox.Show("Edit the fields (username/password/role) and click the Update button below to save changes.", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (colName == "btnDeleteCol")
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this user?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                try
                {
                    DBConnection.Open();
                    string delQuery = "DELETE FROM tbl_users WHERE user_id = @id";
                    MySqlCommand delCmd = new MySqlCommand(delQuery, DBConnection.connection);
                    delCmd.Parameters.AddWithValue("@id", userId);
                    int affected = delCmd.ExecuteNonQuery();
                    if (affected > 0)
                        MessageBox.Show("✅ User deleted.");
                    else
                        MessageBox.Show("Delete failed. User not found.");
                    LoadUsers();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
                finally
                {
                    DBConnection.Close();
                }
            }
        }

        // Panel Update button now applies changes to the selected user (selected via grid)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedUserId < 0)
            {
                MessageBox.Show("Select a user row (or use the Edit button in a row) before updating.", "No user selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Username and Role are required.");
                return;
            }

            try
            {
                DBConnection.Open();
                string updateQuery;
                MySqlCommand cmd;

                // If password field is empty, don't update password
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    updateQuery = "UPDATE tbl_users SET username=@username, user_role=@role WHERE user_id=@id";
                    cmd = new MySqlCommand(updateQuery, DBConnection.connection);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                    cmd.Parameters.AddWithValue("@id", selectedUserId);
                }
                else
                {
                    updateQuery = "UPDATE tbl_users SET username=@username, password=@password, user_role=@role WHERE user_id=@id";
                    cmd = new MySqlCommand(updateQuery, DBConnection.connection);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text); // consider hashing later
                    cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                    cmd.Parameters.AddWithValue("@id", selectedUserId);
                }

                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    MessageBox.Show("✅ User updated successfully!");
                    LoadUsers();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Update failed. User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }

        // Panel Delete button deletes currently selected user (selected via grid)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedUserId < 0)
            {
                MessageBox.Show("Select a user row (or use the Delete button in a row) before deleting.", "No user selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete the selected user?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                DBConnection.Open();
                string delQuery = "DELETE FROM tbl_users WHERE user_id = @id";
                MySqlCommand delCmd = new MySqlCommand(delQuery, DBConnection.connection);
                delCmd.Parameters.AddWithValue("@id", selectedUserId);
                int affected = delCmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    MessageBox.Show("✅ User deleted.");
                    LoadUsers();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Delete failed. User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
            finally
            {
                DBConnection.Close();
            }
        }
    }
}
