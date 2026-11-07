using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class EmployeeHistory : Form
    {
        private readonly DataClasses1DataContext db =new DataClasses1DataContext(Properties.Settings.Default.EmployeeManagementDBConnectionString);

        // Pass the selected employee ID from Form1 when opening this form.
        public EmployeeHistory(string employeeId)
        {
            InitializeComponent();

            txtHistoryId.ReadOnly = true;
            txtEmployeeId.ReadOnly = true;

            dgvEmployeeHistory.ReadOnly = true;
            dgvEmployeeHistory.AutoGenerateColumns = true;
            dgvEmployeeHistory.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvEmployeeHistory.MultiSelect = false;

            cbxEmploymentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEmploymentType.Items.Clear();
            cbxEmploymentType.Items.AddRange(new object[]
            {
                "Permanent",
                "Contractual",
                "Part-time",
                "Internship",
                "Temporary",
                "Volunteer"
            });

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            dtpEndDate.ShowCheckBox = true;
            dtpEndDate.Checked = false; // Unchecked means current employment / NULL EndDate.

            txtEmployeeId.Text = employeeId;

            btnCreate.Click += btnCreate_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            dgvEmployeeHistory.CellClick += dgvEmployeeHistory_CellClick;

            LoadHistory();
        }

        // Optional parameterless constructor if this form is opened manually.
        // The employee ID must then be entered/selected another way.
        public EmployeeHistory() : this(string.Empty)
        {
        }

        private void LoadHistory()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
                {
                    dgvEmployeeHistory.DataSource = null;
                    return;
                }

                var rows = db.usp_EmployeeHistory_ReadByEmployee(
                    txtEmployeeId.Text.Trim()).ToList();

                dgvEmployeeHistory.DataSource = rows;
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private bool ValidateHistory()
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
            {
                MessageBox.Show("Employee ID is required.");
                txtEmployeeId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Company name is required.");
                txtCompanyName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
            {
                MessageBox.Show("Job title is required.");
                txtJobTitle.Focus();
                return false;
            }

            if (cbxEmploymentType.SelectedItem == null)
            {
                MessageBox.Show("Please select an employment type.");
                cbxEmploymentType.Focus();
                return false;
            }

            if (dtpEndDate.Checked &&
                dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                MessageBox.Show("End date must be on or after start date.");
                dtpEndDate.Focus();
                return false;
            }

            return true;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            // No action needed when the title label is clicked.
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // No action needed when the Employee ID label is clicked.
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Validate the end date when it is changed.
            if (dtpEndDate.Checked &&
                dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                MessageBox.Show(
                    "End Date cannot be earlier than Start Date.",
                    "Invalid Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateHistory())
                return;

            try
            {
                DateTime? endDate = dtpEndDate.Checked
                    ? dtpEndDate.Value.Date
                    : (DateTime?)null;

                db.usp_EmployeeHistory_Create(
                    txtEmployeeId.Text.Trim(),
                    txtCompanyName.Text.Trim(),
                    txtJobTitle.Text.Trim(),
                    cbxEmploymentType.SelectedItem.ToString(),
                    dtpStartDate.Value.Date,
                    endDate,
                    BlankToNull(txtReasonForLeaving.Text),
                    BlankToNull(txtNotes.Text));

                MessageBox.Show("Employment history created successfully.");
                LoadHistory();
                ClearHistoryFields(keepEmployeeId: true);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHistoryId.Text))
            {
                MessageBox.Show("Select a history record from the grid first.");
                return;
            }

            if (!ValidateHistory())
                return;

            if (!long.TryParse(txtHistoryId.Text, out long historyId))
            {
                MessageBox.Show("The selected History ID is invalid.");
                return;
            }

            try
            {
                DateTime? endDate = dtpEndDate.Checked
                    ? dtpEndDate.Value.Date
                    : (DateTime?)null;

                db.usp_EmployeeHistory_Update(
                    historyId,
                    txtCompanyName.Text.Trim(),
                    txtJobTitle.Text.Trim(),
                    cbxEmploymentType.SelectedItem.ToString(),
                    dtpStartDate.Value.Date,
                    endDate,
                    BlankToNull(txtReasonForLeaving.Text),
                    BlankToNull(txtNotes.Text));

                MessageBox.Show("Employment history updated successfully.");
                LoadHistory();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHistoryId.Text))
            {
                MessageBox.Show("Select a history record from the grid first.");
                return;
            }

            if (!long.TryParse(txtHistoryId.Text, out long historyId))
            {
                MessageBox.Show("The selected History ID is invalid.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this employment history record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                db.usp_EmployeeHistory_Delete(historyId);
                MessageBox.Show("Employment history deleted successfully.");
                LoadHistory();
                ClearHistoryFields(keepEmployeeId: true);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void dgvEmployeeHistory_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow gridRow = dgvEmployeeHistory.Rows[e.RowIndex];

                txtHistoryId.Text =
                    Convert.ToString(gridRow.Cells["HistoryId"].Value);

                // Employee ID stays fixed to the employee selected in Form1.
                if (gridRow.Cells["EmployeeId"] != null &&
                    gridRow.Cells["EmployeeId"].Value != null)
                {
                    txtEmployeeId.Text =
                        Convert.ToString(gridRow.Cells["EmployeeId"].Value);
                }

                txtCompanyName.Text =
                    Convert.ToString(gridRow.Cells["CompanyName"].Value);

                // The procedure's result column may be named JobTitle or PreviousTitle.
                if (dgvEmployeeHistory.Columns.Contains("JobTitle"))
                    txtJobTitle.Text = Convert.ToString(
                        gridRow.Cells["JobTitle"].Value);
                else if (dgvEmployeeHistory.Columns.Contains("PreviousTitle"))
                    txtJobTitle.Text = Convert.ToString(
                        gridRow.Cells["PreviousTitle"].Value);

                cbxEmploymentType.SelectedItem =
                    Convert.ToString(gridRow.Cells["EmploymentType"].Value);

                dtpStartDate.Value = Convert.ToDateTime(
                    gridRow.Cells["StartDate"].Value);

                object endValue = gridRow.Cells["EndDate"].Value;
                bool hasEndDate = endValue != null &&
                                  endValue != DBNull.Value;

                dtpEndDate.Checked = hasEndDate;
                if (hasEndDate)
                    dtpEndDate.Value = Convert.ToDateTime(endValue);

                txtReasonForLeaving.Text =
                    Convert.ToString(gridRow.Cells["ReasonForLeaving"].Value);

                txtNotes.Text =
                    Convert.ToString(gridRow.Cells["Notes"].Value);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void ClearHistoryFields(bool keepEmployeeId)
        {
            txtHistoryId.Clear();

            if (!keepEmployeeId)
                txtEmployeeId.Clear();

            txtCompanyName.Clear();
            txtJobTitle.Clear();
            cbxEmploymentType.SelectedIndex = -1;
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            dtpEndDate.Checked = false;
            txtReasonForLeaving.Clear();
            txtNotes.Clear();
            txtCompanyName.Focus();
        }

        private static string BlankToNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private void ShowDatabaseError(Exception ex)
        {
            Exception root = ex;
            while (root.InnerException != null)
                root = root.InnerException;

            string message = root.Message;

            if (root is SqlException sqlException)
            {
                if (sqlException.Number == 547)
                    message = "The employee reference or another database constraint is invalid.";
                else if (sqlException.Number == 2627 || sqlException.Number == 2601)
                    message = "A record with the same unique value already exists.";
            }

            MessageBox.Show(
                message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}