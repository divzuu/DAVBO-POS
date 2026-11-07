using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DAVBO_POS
{
    public partial class ProductForm : Form
    {
        private readonly BindingSource productSource = new BindingSource();
        private DataTable productTable;

        public ProductForm()
        {
            InitializeComponent();
            gridProducts.DataSource = productSource;
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                LoadProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditProduct();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void gridProducts_DoubleClick(object sender, EventArgs e)
        {
            EditProduct();
        }

        private void LoadProducts()
        {
            try
            {
                using (var db = CreateContext())
                {
                    productTable = new DataTable();
                    productTable.Columns.Add("ProductId", typeof(int));
                    productTable.Columns.Add("Image", typeof(Image));
                    productTable.Columns.Add("SKU", typeof(string));
                    productTable.Columns.Add("Barcode", typeof(string));
                    productTable.Columns.Add("Name", typeof(string));
                    productTable.Columns.Add("Category", typeof(string));
                    productTable.Columns.Add("Price", typeof(decimal));
                    productTable.Columns.Add("Cost", typeof(decimal));
                    productTable.Columns.Add("Stock", typeof(int));
                    productTable.Columns.Add("Reorder level", typeof(int));
                    productTable.Columns.Add("Active", typeof(bool));

                    foreach (var product in db.Products.OrderBy(p => p.Name))
                    {
                        productTable.Rows.Add(product.ProductId, CreateImage(product.Image), product.Sku, product.Barcode, product.Name,
                            product.Category == null ? string.Empty : product.Category.Name, product.Price, product.Cost,
                            product.StockQty, product.ReorderLevel, product.IsActive);
                    }
                }

                productSource.DataSource = productTable;
                DataGridViewColumn productIdColumn = gridProducts.Columns["ProductId"];
                if (productIdColumn != null)
                    productIdColumn.Visible = false;
                DataGridViewColumn imageColumn = gridProducts.Columns["Image"];
                if (imageColumn != null)
                {
                    imageColumn.HeaderText = "Image";
                    imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    imageColumn.Width = 80;
                }
                DataGridViewColumn activeColumn = gridProducts.Columns["Active"];
                if (activeColumn != null)
                {
                    // The grid uses Fill sizing globally.  Set this generated
                    // column to an explicit sizing mode before changing Width;
                    // otherwise the .NET Framework 4.7.2 Fill-layout path can
                    // run while the bound rows are still being formatted and
                    // dereference incomplete layout state inside WinForms.
                    activeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    activeColumn.Width = 55;
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError("load products", ex);
            }
        }

        private static Image CreateImage(System.Data.Linq.Binary imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            try
            {
                using (var stream = new MemoryStream(imageBytes.ToArray()))
                using (var sourceImage = Image.FromStream(stream))
                    return new Bitmap(sourceImage);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private void AddProduct()
        {
            try
            {
                using (var db = CreateContext())
                {
                    var product = new Product();
                    if (EditProduct(product, db))
                    {
                        db.Products.InsertOnSubmit(product);
                        db.SubmitChanges();
                        LoadProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError("save the product", ex);
            }
        }

        private void EditProduct()
        {
            if (gridProducts.SelectedRows.Count == 0)
                return;

            int productId = Convert.ToInt32(gridProducts.SelectedRows[0].Cells["ProductId"].Value);
            try
            {
                using (var db = CreateContext())
                {
                    Product product = db.Products.SingleOrDefault(p => p.ProductId == productId);
                    if (product != null && EditProduct(product, db))
                    {
                        db.SubmitChanges();
                        LoadProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError("update the product", ex);
            }
        }

        private bool EditProduct(Product product, DataClasses1DataContext db)
        {
            IList<Category> categories = db.Categories.OrderBy(c => c.Name).ToList();
            if (categories.Count == 0)
            {
                MessageBox.Show("Create a category before adding a product.", "No categories", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (var editor = new ProductEditorForm(product, categories))
                return editor.ShowDialog(this) == DialogResult.OK;
        }

        private void DeleteProduct()
        {
            if (gridProducts.SelectedRows.Count == 0)
                return;

            int productId = Convert.ToInt32(gridProducts.SelectedRows[0].Cells["ProductId"].Value);
            string productName = Convert.ToString(gridProducts.SelectedRows[0].Cells["Name"].Value);
            if (MessageBox.Show("Delete '" + productName + "'?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                using (var db = CreateContext())
                {
                    Product product = db.Products.SingleOrDefault(p => p.ProductId == productId);
                    if (product != null)
                    {
                        db.Products.DeleteOnSubmit(product);
                        db.SubmitChanges();
                        LoadProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError("delete the product", ex);
            }
        }

        private DataClasses1DataContext CreateContext()
        {
            return new DataClasses1DataContext(Properties.Settings.Default.boholConnectionString);
        }

        private void ShowDatabaseError(string operation, Exception ex)
        {
            MessageBox.Show("Unable to " + operation + ": " + ex.Message, "Inventory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
