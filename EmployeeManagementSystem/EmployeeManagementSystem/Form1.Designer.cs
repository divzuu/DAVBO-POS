namespace EmployeeManagementSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.MaskedTextBox();
            this.txtEmail = new System.Windows.Forms.MaskedTextBox();
            this.txtPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.txtJobTitle = new System.Windows.Forms.MaskedTextBox();
            this.cbxSex = new System.Windows.Forms.ComboBox();
            this.cbxEmploymentStatus = new System.Windows.Forms.ComboBox();
            this.cbxDepartmentId = new System.Windows.Forms.ComboBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.dtpBirthdate = new System.Windows.Forms.DateTimePicker();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.nudSalary = new System.Windows.Forms.NumericUpDown();
            this.lblEmployeeID = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblMiddleName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.MaskedTextBox();
            this.txtMiddleName = new System.Windows.Forms.MaskedTextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblDepartmentId = new System.Windows.Forms.Label();
            this.lblJobTitle = new System.Windows.Forms.Label();
            this.lblSalary = new System.Windows.Forms.Label();
            this.lblHireDate = new System.Windows.Forms.Label();
            this.lblEmploymentStatus = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.MaskedTextBox();
            this.dgvEmployeeData = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEmployeeHistory = new System.Windows.Forms.Button();
            this.txtSearchEmployee = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSalary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeData)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEmployeeId
            // 
            this.txtEmployeeId.Location = new System.Drawing.Point(144, 80);
            this.txtEmployeeId.Name = "txtEmployeeId";
            this.txtEmployeeId.ReadOnly = true;
            this.txtEmployeeId.Size = new System.Drawing.Size(139, 20);
            this.txtEmployeeId.TabIndex = 0;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(144, 112);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(139, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(144, 289);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(139, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(144, 315);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(139, 20);
            this.txtPhoneNumber.TabIndex = 4;
            // 
            // txtJobTitle
            // 
            this.txtJobTitle.Location = new System.Drawing.Point(144, 396);
            this.txtJobTitle.Name = "txtJobTitle";
            this.txtJobTitle.Size = new System.Drawing.Size(139, 20);
            this.txtJobTitle.TabIndex = 6;
            // 
            // cbxSex
            // 
            this.cbxSex.FormattingEnabled = true;
            this.cbxSex.Location = new System.Drawing.Point(144, 205);
            this.cbxSex.Name = "cbxSex";
            this.cbxSex.Size = new System.Drawing.Size(139, 21);
            this.cbxSex.TabIndex = 7;
            // 
            // cbxEmploymentStatus
            // 
            this.cbxEmploymentStatus.FormattingEnabled = true;
            this.cbxEmploymentStatus.Location = new System.Drawing.Point(144, 423);
            this.cbxEmploymentStatus.Name = "cbxEmploymentStatus";
            this.cbxEmploymentStatus.Size = new System.Drawing.Size(139, 21);
            this.cbxEmploymentStatus.TabIndex = 9;
            // 
            // cbxDepartmentId
            // 
            this.cbxDepartmentId.FormattingEnabled = true;
            this.cbxDepartmentId.Location = new System.Drawing.Point(144, 367);
            this.cbxDepartmentId.Name = "cbxDepartmentId";
            this.cbxDepartmentId.Size = new System.Drawing.Size(139, 21);
            this.cbxDepartmentId.TabIndex = 10;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(144, 264);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(139, 20);
            this.txtAge.TabIndex = 11;
            // 
            // dtpBirthdate
            // 
            this.dtpBirthdate.Location = new System.Drawing.Point(144, 235);
            this.dtpBirthdate.Name = "dtpBirthdate";
            this.dtpBirthdate.Size = new System.Drawing.Size(139, 20);
            this.dtpBirthdate.TabIndex = 12;
            // 
            // dtpHireDate
            // 
            this.dtpHireDate.Location = new System.Drawing.Point(144, 449);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(139, 20);
            this.dtpHireDate.TabIndex = 13;
            // 
            // nudSalary
            // 
            this.nudSalary.Location = new System.Drawing.Point(144, 475);
            this.nudSalary.Name = "nudSalary";
            this.nudSalary.Size = new System.Drawing.Size(140, 20);
            this.nudSalary.TabIndex = 14;
            // 
            // lblEmployeeID
            // 
            this.lblEmployeeID.AutoSize = true;
            this.lblEmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeID.Location = new System.Drawing.Point(19, 87);
            this.lblEmployeeID.Name = "lblEmployeeID";
            this.lblEmployeeID.Size = new System.Drawing.Size(80, 13);
            this.lblEmployeeID.TabIndex = 15;
            this.lblEmployeeID.Text = "Employee Id:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(19, 119);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(69, 13);
            this.lblFirstName.TabIndex = 16;
            this.lblFirstName.Text = "First name:";
            this.lblFirstName.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblMiddleName
            // 
            this.lblMiddleName.AutoSize = true;
            this.lblMiddleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiddleName.Location = new System.Drawing.Point(19, 151);
            this.lblMiddleName.Name = "lblMiddleName";
            this.lblMiddleName.Size = new System.Drawing.Size(84, 13);
            this.lblMiddleName.TabIndex = 17;
            this.lblMiddleName.Text = "Middle Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(144, 175);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(139, 20);
            this.txtLastName.TabIndex = 18;
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(144, 144);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(139, 20);
            this.txtMiddleName.TabIndex = 19;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(19, 182);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(69, 13);
            this.lblLastName.TabIndex = 20;
            this.lblLastName.Text = "Last name:";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSex.Location = new System.Drawing.Point(19, 213);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(32, 13);
            this.lblSex.TabIndex = 21;
            this.lblSex.Text = "Sex:";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBirthDate.Location = new System.Drawing.Point(19, 241);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(68, 13);
            this.lblBirthDate.TabIndex = 22;
            this.lblBirthDate.Text = "Birth Date:";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAge.Location = new System.Drawing.Point(19, 271);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(33, 13);
            this.lblAge.TabIndex = 23;
            this.lblAge.Text = "Age:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(19, 322);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(94, 13);
            this.lblPhone.TabIndex = 24;
            this.lblPhone.Text = "Phone Number:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(19, 296);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 13);
            this.lblEmail.TabIndex = 25;
            this.lblEmail.Text = "Email:";
            // 
            // lblDepartmentId
            // 
            this.lblDepartmentId.AutoSize = true;
            this.lblDepartmentId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentId.Location = new System.Drawing.Point(19, 375);
            this.lblDepartmentId.Name = "lblDepartmentId";
            this.lblDepartmentId.Size = new System.Drawing.Size(91, 13);
            this.lblDepartmentId.TabIndex = 26;
            this.lblDepartmentId.Text = "Department Id:";
            // 
            // lblJobTitle
            // 
            this.lblJobTitle.AutoSize = true;
            this.lblJobTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobTitle.Location = new System.Drawing.Point(19, 403);
            this.lblJobTitle.Name = "lblJobTitle";
            this.lblJobTitle.Size = new System.Drawing.Size(60, 13);
            this.lblJobTitle.TabIndex = 27;
            this.lblJobTitle.Text = "Job Title:";
            // 
            // lblSalary
            // 
            this.lblSalary.AutoSize = true;
            this.lblSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalary.Location = new System.Drawing.Point(19, 482);
            this.lblSalary.Name = "lblSalary";
            this.lblSalary.Size = new System.Drawing.Size(46, 13);
            this.lblSalary.TabIndex = 28;
            this.lblSalary.Text = "Salary:";
            // 
            // lblHireDate
            // 
            this.lblHireDate.AutoSize = true;
            this.lblHireDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHireDate.Location = new System.Drawing.Point(19, 456);
            this.lblHireDate.Name = "lblHireDate";
            this.lblHireDate.Size = new System.Drawing.Size(65, 13);
            this.lblHireDate.TabIndex = 29;
            this.lblHireDate.Text = "Hire Date:";
            // 
            // lblEmploymentStatus
            // 
            this.lblEmploymentStatus.AutoSize = true;
            this.lblEmploymentStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmploymentStatus.Location = new System.Drawing.Point(19, 431);
            this.lblEmploymentStatus.Name = "lblEmploymentStatus";
            this.lblEmploymentStatus.Size = new System.Drawing.Size(118, 13);
            this.lblEmploymentStatus.TabIndex = 30;
            this.lblEmploymentStatus.Text = "Employment Status:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(19, 348);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(56, 13);
            this.lblAddress.TabIndex = 32;
            this.lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(144, 341);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(139, 20);
            this.txtAddress.TabIndex = 31;
            // 
            // dgvEmployeeData
            // 
            this.dgvEmployeeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployeeData.Location = new System.Drawing.Point(309, 80);
            this.dgvEmployeeData.Name = "dgvEmployeeData";
            this.dgvEmployeeData.Size = new System.Drawing.Size(751, 487);
            this.dgvEmployeeData.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 31);
            this.label1.TabIndex = 34;
            this.label1.Text = "Employee Form:";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(28, 515);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 35;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(109, 515);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 36;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(190, 515);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 38;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEmployeeHistory
            // 
            this.btnEmployeeHistory.Location = new System.Drawing.Point(109, 544);
            this.btnEmployeeHistory.Name = "btnEmployeeHistory";
            this.btnEmployeeHistory.Size = new System.Drawing.Size(156, 23);
            this.btnEmployeeHistory.TabIndex = 39;
            this.btnEmployeeHistory.Text = "Go to Employee History";
            this.btnEmployeeHistory.UseVisualStyleBackColor = true;
            // 
            // txtSearchEmployee
            // 
            this.txtSearchEmployee.Location = new System.Drawing.Point(421, 44);
            this.txtSearchEmployee.Name = "txtSearchEmployee";
            this.txtSearchEmployee.Size = new System.Drawing.Size(184, 20);
            this.txtSearchEmployee.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(306, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Search Employee:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(28, 544);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 42;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 579);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchEmployee);
            this.Controls.Add(this.btnEmployeeHistory);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvEmployeeData);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblEmploymentStatus);
            this.Controls.Add(this.lblHireDate);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.lblJobTitle);
            this.Controls.Add(this.lblDepartmentId);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblBirthDate);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblMiddleName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblEmployeeID);
            this.Controls.Add(this.nudSalary);
            this.Controls.Add(this.dtpHireDate);
            this.Controls.Add(this.dtpBirthdate);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.cbxDepartmentId);
            this.Controls.Add(this.cbxEmploymentStatus);
            this.Controls.Add(this.cbxSex);
            this.Controls.Add(this.txtJobTitle);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtEmployeeId);
            this.Name = "Form1";
            this.Text = "Employee Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSalary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmployeeId;
        private System.Windows.Forms.MaskedTextBox txtFirstName;
        private System.Windows.Forms.MaskedTextBox txtEmail;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumber;
        private System.Windows.Forms.MaskedTextBox txtJobTitle;
        private System.Windows.Forms.ComboBox cbxSex;
        private System.Windows.Forms.ComboBox cbxEmploymentStatus;
        private System.Windows.Forms.ComboBox cbxDepartmentId;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.DateTimePicker dtpBirthdate;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.NumericUpDown nudSalary;
        private System.Windows.Forms.Label lblEmployeeID;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblMiddleName;
        private System.Windows.Forms.MaskedTextBox txtLastName;
        private System.Windows.Forms.MaskedTextBox txtMiddleName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblDepartmentId;
        private System.Windows.Forms.Label lblJobTitle;
        private System.Windows.Forms.Label lblSalary;
        private System.Windows.Forms.Label lblHireDate;
        private System.Windows.Forms.Label lblEmploymentStatus;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.MaskedTextBox txtAddress;
        private System.Windows.Forms.DataGridView dgvEmployeeData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEmployeeHistory;
        private System.Windows.Forms.MaskedTextBox txtSearchEmployee;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
    }
}

