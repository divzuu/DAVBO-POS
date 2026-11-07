
using System;
using System.Linq;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Form1 : Form
    {
        // LINQ to SQL database connection
        private readonly DataClasses1DataContext db =new DataClasses1DataContext(Properties.Settings.Default.EmployeeManagementDBConnectionString);

        public Form1()
        {
            InitializeComponent();

            // Configure controls
            txtEmployeeId.ReadOnly = true;
            txtAge.ReadOnly = true;

            dgvEmployeeData.ReadOnly = true;
            dgvEmployeeData.AutoGenerateColumns = true;
            dgvEmployeeData.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvEmployeeData.MultiSelect = false;

            // Sex ComboBox
            cbxSex.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSex.Items.Clear();
            cbxSex.Items.AddRange(new object[]
            {
                "Female",
                "Male",
                "Prefer not to say"
            });

            // Employment Status ComboBox
            cbxEmploymentStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;
            cbxEmploymentStatus.Items.Clear();
            cbxEmploymentStatus.Items.AddRange(new object[]
            {
                "Active",
                "On Leave",
                "Resigned",
                "Terminated",
                "Retired"
            });
            cbxEmploymentStatus.SelectedItem = "Active";

            // Date and salary settings
            dtpBirthdate.MaxDate = DateTime.Today;
            nudSalary.Minimum = 0;
            nudSalary.Maximum = 100000000;
            nudSalary.DecimalPlaces = 2;

            // Connect event handlers
            btnCreate.Click += btnCreate_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnEmployeeHistory.Click += btnEmployeeHistory_Click;
            txtSearchEmployee.TextChanged += txtSearchEmployee_TextChanged;
            dgvEmployeeData.CellClick += dgvEmployeeData_CellClick;

            // Load database records
            LoadDepartments();
            LoadEmployees(null);
        }

        // ==========================================
        // LOAD DEPARTMENTS
        // ==========================================
        private void LoadDepartments()
        {
            try
            {
                var departments = db.Departments
                    .OrderBy(d => d.DepartmentName)
                    .ToList();

                cbxDepartmentId.DataSource = departments;
                cbxDepartmentId.DisplayMember = "DepartmentName";
                cbxDepartmentId.ValueMember = "DepartmentId";
                cbxDepartmentId.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // ==========================================
        // LOAD AND SEARCH EMPLOYEES
        // ==========================================
        private void LoadEmployees(string search)
        {
            try
            {
                string searchText =
                    string.IsNullOrWhiteSpace(search)
                    ? null
                    : search.Trim();

                var employees = db.usp_Employee_Search(
                    searchText,
                    null,
                    null
                ).ToList();

                dgvEmployeeData.DataSource = employees;
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // ==========================================
        // VALIDATE EMPLOYEE INPUTS
        // ==========================================
        private bool ValidateEmployee()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First name is required.");
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last name is required.");
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.");
                txtEmail.Focus();
                return false;
            }

            try
            {
                var email = new MailAddress(txtEmail.Text.Trim());

                if (!string.Equals(
                    email.Address,
                    txtEmail.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new FormatException();
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid email address.");
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
            {
                MessageBox.Show("Job title is required.");
                txtJobTitle.Focus();
                return false;
            }

            if (dtpHireDate.Value.Date < dtpBirthdate.Value.Date)
            {
                MessageBox.Show(
                    "Hire date must be on or after the birth date.");
                dtpHireDate.Focus();
                return false;
            }

            if (dtpBirthdate.Value.Date >= DateTime.Today)
            {
                MessageBox.Show("Birth date must be in the past.");
                dtpBirthdate.Focus();
                return false;
            }

            if (nudSalary.Value < 0)
            {
                MessageBox.Show("Salary cannot be negative.");
                nudSalary.Focus();
                return false;
            }

            return true;
        }

        // ==========================================
        // CREATE EMPLOYEE
        // ==========================================
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateEmployee())
                return;

            try
            {
                string newEmployeeId = null;

                int? departmentId =
                    cbxDepartmentId.SelectedValue == null
                    ? (int?)null
                    : Convert.ToInt32(cbxDepartmentId.SelectedValue);

                string sex = cbxSex.SelectedItem == null
                    ? null
                    : cbxSex.SelectedItem.ToString();

                string status = cbxEmploymentStatus.SelectedItem == null
                    ? "Active"
                    : cbxEmploymentStatus.SelectedItem.ToString();

                db.usp_Employee_Create(
                    txtFirstName.Text.Trim(),
                    BlankToNull(txtMiddleName.Text),
                    txtLastName.Text.Trim(),
                    sex,
                    dtpBirthdate.Value.Date,
                    txtEmail.Text.Trim(),
                    BlankToNull(txtPhoneNumber.Text),
                    BlankToNull(txtAddress.Text),
                    departmentId,
                    txtJobTitle.Text.Trim(),
                    status,
                    dtpHireDate.Value.Date,
                    nudSalary.Value,
                    ref newEmployeeId
                );

                txtEmployeeId.Text = newEmployeeId ?? "";

                MessageBox.Show(
                    "Employee created successfully.\nEmployee ID: "
                    + txtEmployeeId.Text,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadEmployees(txtSearchEmployee.Text);
                ClearForm();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // ==========================================
        // UPDATE EMPLOYEE
        // ==========================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
            {
                MessageBox.Show(
                    "Please select an employee from the table first.");
                return;
            }

            if (!ValidateEmployee())
                return;

            try
            {
                int? departmentId =
                    cbxDepartmentId.SelectedValue == null
                    ? (int?)null
                    : Convert.ToInt32(cbxDepartmentId.SelectedValue);

                string sex = cbxSex.SelectedItem == null
                    ? null
                    : cbxSex.SelectedItem.ToString();

                string status = cbxEmploymentStatus.SelectedItem == null
                    ? "Active"
                    : cbxEmploymentStatus.SelectedItem.ToString();

                db.usp_Employee_Update(
                    txtEmployeeId.Text,
                    txtFirstName.Text.Trim(),
                    BlankToNull(txtMiddleName.Text),
                    txtLastName.Text.Trim(),
                    sex,
                    dtpBirthdate.Value.Date,
                    txtEmail.Text.Trim(),
                    BlankToNull(txtPhoneNumber.Text),
                    BlankToNull(txtAddress.Text),
                    departmentId,
                    txtJobTitle.Text.Trim(),
                    status,
                    dtpHireDate.Value.Date,
                    nudSalary.Value
                );

                MessageBox.Show(
                    "Employee updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadEmployees(txtSearchEmployee.Text);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // ==========================================
        // DELETE EMPLOYEE
        // ==========================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
            {
                MessageBox.Show(
                    "Please select an employee from the table first.");
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                "Are you sure you want to delete employee "
                + txtEmployeeId.Text
                + "?\nRelated employment history will also be deleted.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmation != DialogResult.Yes)
                return;

            try
            {
                db.usp_Employee_Delete(txtEmployeeId.Text);

                MessageBox.Show("Employee deleted successfully.");

                ClearForm();
                LoadEmployees(txtSearchEmployee.Text);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // ==========================================
        // SEARCH EMPLOYEES
        // ==========================================
        private void txtSearchEmployee_TextChanged(
            object sender, EventArgs e)
        {
            LoadEmployees(txtSearchEmployee.Text);
        }

        // ==========================================
        // SELECT EMPLOYEE FROM DATAGRIDVIEW
        // ==========================================
        private void label1_Click(object sender, EventArgs e)
        {
            // No action needed when the First Name label is clicked.
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // No action needed when the Search Employee label is clicked.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the form when it opens.
            // Keep any existing loading logic here.
        }
        private void dgvEmployeeData_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                string employeeId = Convert.ToString(
                    dgvEmployeeData.Rows[e.RowIndex]
                        .Cells["EmployeeId"].Value
                );

                if (string.IsNullOrWhiteSpace(employeeId))
                    return;

                var employee = db.usp_Employee_Read(employeeId)
                    .FirstOrDefault();

                if (employee == null)
                    return;

                txtEmployeeId.Text =
                    Convert.ToString(employee.EmployeeId);

                txtFirstName.Text =
                    Convert.ToString(employee.FirstName);

                txtMiddleName.Text =
                    Convert.ToString(employee.MiddleName);

                txtLastName.Text =
                    Convert.ToString(employee.LastName);

                txtAge.Text =
                    Convert.ToString(employee.Age);

                txtEmail.Text =
                    Convert.ToString(employee.Email);

                txtPhoneNumber.Text =
                    Convert.ToString(employee.Phone);

                txtAddress.Text =
                    Convert.ToString(employee.Address);

                txtJobTitle.Text =
                    Convert.ToString(employee.JobTitle);

                dtpBirthdate.Value =
                    Convert.ToDateTime(employee.BirthDate);

                dtpHireDate.Value =
                    Convert.ToDateTime(employee.HireDate);

                nudSalary.Value =
                    Convert.ToDecimal(employee.Salary);

                cbxSex.SelectedItem =
                    Convert.ToString(employee.Sex);

                cbxEmploymentStatus.SelectedItem =
                    Convert.ToString(employee.EmploymentStatus);

                if (employee.DepartmentId == null)
                {
                    cbxDepartmentId.SelectedIndex = -1;
                }
                else
                {
                    cbxDepartmentId.SelectedValue =
                        employee.DepartmentId;
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // ==========================================
        // CLEAR FORM
        // ==========================================
        private void ClearForm()
        {
            txtEmployeeId.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            txtEmail.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtJobTitle.Clear();

            cbxSex.SelectedIndex = -1;
            cbxEmploymentStatus.SelectedItem = "Active";
            cbxDepartmentId.SelectedIndex = -1;

            dtpBirthdate.Value = DateTime.Today.AddYears(-18);
            dtpHireDate.Value = DateTime.Today;
            nudSalary.Value = 0;

            txtFirstName.Focus();
        }

        // ==========================================
        // OPEN EMPLOYEE HISTORY
        // ==========================================
        private void btnEmployeeHistory_Click(
            object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeId.Text))
            {
                MessageBox.Show(
                    "Please select an employee first.");
                return;
            }

            // Requires an EmployeeHistoryForm with a constructor
            // that accepts the selected employee ID.
            using (EmployeeHistory historyForm =
                new EmployeeHistory(txtEmployeeId.Text))
            {
                historyForm.ShowDialog(this);
            }
        }

        // ==========================================
        // CONVERT EMPTY TEXT TO NULL
        // ==========================================
        private static string BlankToNull(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }

        // ==========================================
        // DATABASE ERROR HANDLING
        // ==========================================
        private void ShowDatabaseError(Exception ex)
        {
            Exception root = ex;

            while (root.InnerException != null)
                root = root.InnerException;

            string message = root.Message;

            if (root is SqlException sqlException)
            {
                if (sqlException.Number == 2627 ||
                    sqlException.Number == 2601)
                {
                    message =
                        "This email address or another unique value already exists.";
                }
                else if (sqlException.Number == 547)
                {
                    message =
                        "The operation conflicts with a database relationship or validation rule.";
                }
            }

            MessageBox.Show(
                message,
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
