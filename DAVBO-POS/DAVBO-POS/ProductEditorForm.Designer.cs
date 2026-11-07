namespace DAVBO_POS
{
    partial class ProductEditorForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSku;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblReorderLevel;
        private System.Windows.Forms.Label lblImageCaption;
        private System.Windows.Forms.TextBox txtSku;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.NumericUpDown nudCost;
        private System.Windows.Forms.NumericUpDown nudStock;
        private System.Windows.Forms.NumericUpDown nudReorderLevel;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.PictureBox picImagePreview;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblSku = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblReorderLevel = new System.Windows.Forms.Label();
            this.lblImageCaption = new System.Windows.Forms.Label();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.nudCost = new System.Windows.Forms.NumericUpDown();
            this.nudStock = new System.Windows.Forms.NumericUpDown();
            this.nudReorderLevel = new System.Windows.Forms.NumericUpDown();
            this.btnImage = new System.Windows.Forms.Button();
            this.btnClearImage = new System.Windows.Forms.Button();
            this.lblImage = new System.Windows.Forms.Label();
            this.picImagePreview = new System.Windows.Forms.PictureBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReorderLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).BeginInit();
            this.SuspendLayout();

            this.lblSku.AutoSize = true;
            this.lblSku.Location = new System.Drawing.Point(20, 18);
            this.lblSku.Text = "SKU:";
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(20, 56);
            this.lblBarcode.Text = "Barcode:";
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 94);
            this.lblName.Text = "Name:";
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 132);
            this.lblCategory.Text = "Category:";
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(20, 170);
            this.lblPrice.Text = "Price:";
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(20, 208);
            this.lblCost.Text = "Cost:";
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(20, 246);
            this.lblStock.Text = "Stock quantity:";
            this.lblReorderLevel.AutoSize = true;
            this.lblReorderLevel.Location = new System.Drawing.Point(20, 284);
            this.lblReorderLevel.Text = "Reorder level:";
            this.lblImageCaption.AutoSize = true;
            this.lblImageCaption.Location = new System.Drawing.Point(20, 322);
            this.lblImageCaption.Text = "Image:";

            this.txtSku.Location = new System.Drawing.Point(135, 15);
            this.txtSku.Size = new System.Drawing.Size(160, 20);
            this.txtBarcode.Location = new System.Drawing.Point(135, 53);
            this.txtBarcode.Size = new System.Drawing.Size(160, 20);
            this.txtName.Location = new System.Drawing.Point(135, 91);
            this.txtName.Size = new System.Drawing.Size(160, 20);
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(135, 129);
            this.cboCategory.Size = new System.Drawing.Size(160, 21);
            this.cboCategory.DisplayMember = "Name";
            this.cboCategory.ValueMember = "CategoryId";

            this.nudPrice.DecimalPlaces = 2;
            this.nudPrice.Location = new System.Drawing.Point(135, 167);
            this.nudPrice.Maximum = 1000000000M;
            this.nudPrice.Size = new System.Drawing.Size(160, 20);
            this.nudCost.DecimalPlaces = 2;
            this.nudCost.Location = new System.Drawing.Point(135, 205);
            this.nudCost.Maximum = 1000000000M;
            this.nudCost.Size = new System.Drawing.Size(160, 20);
            this.nudStock.Location = new System.Drawing.Point(135, 243);
            this.nudStock.Maximum = 1000000M;
            this.nudStock.Size = new System.Drawing.Size(160, 20);
            this.nudReorderLevel.Location = new System.Drawing.Point(135, 281);
            this.nudReorderLevel.Maximum = 1000000M;
            this.nudReorderLevel.Size = new System.Drawing.Size(160, 20);

            this.picImagePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImagePreview.Location = new System.Drawing.Point(305, 15);
            this.picImagePreview.Size = new System.Drawing.Size(105, 105);
            this.picImagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImagePreview.TabStop = false;
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(305, 125);
            this.lblImage.Text = "No image";

            this.btnImage.Location = new System.Drawing.Point(135, 319);
            this.btnImage.Size = new System.Drawing.Size(95, 25);
            this.btnImage.Text = "Choose...";
            this.btnImage.Click += new System.EventHandler(this.ChooseImageClick);
            this.btnClearImage.Location = new System.Drawing.Point(235, 319);
            this.btnClearImage.Size = new System.Drawing.Size(75, 25);
            this.btnClearImage.Text = "Clear";
            this.btnClearImage.Click += new System.EventHandler(this.ClearImageClick);
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(135, 359);
            this.chkActive.Text = "Active product";
            this.btnSave.Location = new System.Drawing.Point(239, 397);
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.SaveClick);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(320, 397);
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.Text = "Cancel";

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSku, this.lblBarcode, this.lblName, this.lblCategory,
                this.lblPrice, this.lblCost, this.lblStock, this.lblReorderLevel,
                this.lblImageCaption, this.txtSku, this.txtBarcode, this.txtName,
                this.cboCategory, this.nudPrice, this.nudCost, this.nudStock,
                this.nudReorderLevel, this.btnImage, this.btnClearImage,
                this.lblImage, this.picImagePreview, this.chkActive,
                this.btnSave, this.btnCancel });
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 445);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProductEditorForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReorderLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}