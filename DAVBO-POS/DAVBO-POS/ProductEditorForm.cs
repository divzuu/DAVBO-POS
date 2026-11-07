using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data.Linq;

namespace DAVBO_POS
{
    public partial class ProductEditorForm : Form
    {
        private readonly Product product;
        private Binary selectedImage;
        private bool imageChanged;
        private System.Drawing.Image previewImage;

        // Required by the WinForms designer. Runtime code uses the data-bound constructor below.
        public ProductEditorForm()
            : this(new Product(), new List<Category>())
        {
        }

        public ProductEditorForm(Product product, IList<Category> categories)
        {
            this.product = product;
            InitializeComponent();
            Text = product.ProductId == 0 ? "Add Product" : "Edit Product";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(430, 445);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            cboCategory.DataSource = categories;
            LoadProduct();
            FormClosed += ProductEditorForm_FormClosed;
        }

        private void ProductEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (previewImage != null)
            {
                previewImage.Dispose();
                previewImage = null;
            }
        }

        private void ChooseImageClick(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files|*.*" })
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    selectedImage = new Binary(File.ReadAllBytes(dialog.FileName));
                    imageChanged = true;
                    lblImage.Text = Path.GetFileName(dialog.FileName);
                    SetImagePreview(selectedImage);
                }
            }
        }

        private void ClearImageClick(object sender, EventArgs e)
        {
            selectedImage = null;
            imageChanged = true;
            lblImage.Text = "No image";
            SetImagePreview(null);
        }

        private void LoadProduct()
        {
            txtSku.Text = product.Sku ?? string.Empty;
            txtBarcode.Text = product.Barcode ?? string.Empty;
            txtName.Text = product.Name ?? string.Empty;
            if (product.ProductId != 0)
                cboCategory.SelectedValue = product.CategoryId;
            nudPrice.Value = product.Price;
            nudCost.Value = product.Cost;
            nudStock.Value = product.StockQty;
            nudReorderLevel.Value = product.ReorderLevel;
            chkActive.Checked = product.ProductId == 0 || product.IsActive;
            selectedImage = product.Image;
            lblImage.Text = selectedImage == null ? "No image" : "Existing image";
            SetImagePreview(selectedImage);
        }

        private void SetImagePreview(Binary imageBytes)
        {
            if (previewImage != null)
            {
                previewImage.Dispose();
                previewImage = null;
            }

            picImagePreview.Image = null;
            if (imageBytes == null || imageBytes.Length == 0)
                return;

            try
            {
                using (var stream = new MemoryStream(imageBytes.ToArray()))
                using (var sourceImage = System.Drawing.Image.FromStream(stream))
                {
                    previewImage = new Bitmap(sourceImage);
                    picImagePreview.Image = previewImage;
                }
            }
            catch (ArgumentException)
            {
                lblImage.Text = "Invalid image";
            }
        }

        private void SaveClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSku.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("SKU and name are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Select a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            product.Sku = txtSku.Text.Trim();
            product.Barcode = string.IsNullOrWhiteSpace(txtBarcode.Text) ? null : txtBarcode.Text.Trim();
            product.Name = txtName.Text.Trim();
            product.CategoryId = Convert.ToInt32(cboCategory.SelectedValue);
            product.Price = nudPrice.Value;
            product.Cost = nudCost.Value;
            product.StockQty = Convert.ToInt32(nudStock.Value);
            product.ReorderLevel = Convert.ToInt32(nudReorderLevel.Value);
            product.IsActive = chkActive.Checked;
            if (imageChanged)
                product.Image = selectedImage;
            product.UpdatedAt = DateTime.Now;
            if (product.ProductId == 0)
                product.CreatedAt = DateTime.Now;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
