using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DAVBO_POS
{
    public sealed class SalesForm : Form
    {
        private readonly ComboBox cboProduct = new ComboBox();
        private readonly NumericUpDown nudQuantity = new NumericUpDown();
        private readonly DataGridView gridCart = new DataGridView();
        private readonly ComboBox cboPayment = new ComboBox();
        private readonly NumericUpDown nudDiscount = new NumericUpDown();
        private readonly NumericUpDown nudTaxRate = new NumericUpDown();
        private readonly NumericUpDown nudAmountPaid = new NumericUpDown();
        private readonly Label lblSubtotal = new Label();
        private readonly Label lblTax = new Label();
        private readonly Label lblTotal = new Label();
        private readonly Label lblChange = new Label();
        private readonly List<CartLine> cart = new List<CartLine>();
        private bool amountPaidEntered;
        private bool updatingAmountPaid;
        private List<Product> products;

        public SalesForm()
        {
            Text = "New Sale";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(820, 560);
            MinimumSize = new Size(820, 560);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(245, 247, 250);

            BuildLayout();
            LoadProducts();
        }

        private void BuildLayout()
        {
            var title = new Label
            {
                Text = "New Sale",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 48, 66),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var productPanel = new Panel
            {
                Location = new Point(20, 60),
                Size = new Size(780, 55),
                BackColor = Color.White,
                Padding = new Padding(12)
            };

            cboProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProduct.Location = new Point(12, 14);
            cboProduct.Size = new Size(390, 25);
            cboProduct.DisplayMember = "DisplayName";

            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = 100000;
            nudQuantity.Value = 1;
            nudQuantity.Location = new Point(415, 14);
            nudQuantity.Size = new Size(75, 25);

            var btnAdd = new Button
            {
                Text = "Add item",
                Location = new Point(505, 12),
                Size = new Size(105, 29),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(35, 48, 66),
                ForeColor = Color.White
            };
            btnAdd.Click += AddItemClick;

            var btnRemove = new Button
            {
                Text = "Remove selected",
                Location = new Point(620, 12),
                Size = new Size(135, 29),
                FlatStyle = FlatStyle.Flat
            };
            btnRemove.Click += RemoveItemClick;

            productPanel.Controls.Add(cboProduct);
            productPanel.Controls.Add(nudQuantity);
            productPanel.Controls.Add(btnAdd);
            productPanel.Controls.Add(btnRemove);

            gridCart.Location = new Point(20, 125);
            gridCart.Size = new Size(780, 245);
            gridCart.AllowUserToAddRows = false;
            gridCart.AllowUserToDeleteRows = false;
            gridCart.AllowUserToResizeRows = false;
            gridCart.AutoGenerateColumns = false;
            gridCart.BackgroundColor = Color.White;
            gridCart.ReadOnly = true;
            gridCart.RowHeadersVisible = false;
            gridCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductId", Visible = false });
            gridCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Product", HeaderText = "Product", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            gridCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Qty", Width = 70 });
            gridCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Unit price", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });
            gridCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "LineTotal", HeaderText = "Total", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } });

            var paymentPanel = new Panel
            {
                Location = new Point(20, 385),
                Size = new Size(780, 125),
                BackColor = Color.White,
                Padding = new Padding(12)
            };

            paymentPanel.Controls.Add(CreateFieldLabel("Payment", 12, 15));
            cboPayment.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPayment.Items.AddRange(new object[] { "Cash", "Card", "Other" });
            cboPayment.SelectedIndex = 0;
            cboPayment.Location = new Point(90, 12);
            cboPayment.Size = new Size(120, 25);
            cboPayment.SelectedIndexChanged += PaymentChanged;
            paymentPanel.Controls.Add(cboPayment);

            paymentPanel.Controls.Add(CreateFieldLabel("Discount", 230, 15));
            nudDiscount.DecimalPlaces = 2;
            nudDiscount.Maximum = 1000000000;
            nudDiscount.Location = new Point(300, 12);
            nudDiscount.Size = new Size(100, 25);
            nudDiscount.ValueChanged += TotalsChanged;
            paymentPanel.Controls.Add(nudDiscount);

            paymentPanel.Controls.Add(CreateFieldLabel("Tax %", 420, 15));
            nudTaxRate.DecimalPlaces = 2;
            nudTaxRate.Maximum = 100;
            nudTaxRate.Location = new Point(465, 12);
            nudTaxRate.Size = new Size(70, 25);
            nudTaxRate.ValueChanged += TotalsChanged;
            paymentPanel.Controls.Add(nudTaxRate);

            paymentPanel.Controls.Add(CreateFieldLabel("Amount paid", 555, 15));
            nudAmountPaid.DecimalPlaces = 2;
            nudAmountPaid.Maximum = 1000000000;
            nudAmountPaid.Location = new Point(655, 12);
            nudAmountPaid.Size = new Size(100, 25);
            nudAmountPaid.Enabled = true;
            nudAmountPaid.ValueChanged += AmountPaidChanged;
            paymentPanel.Controls.Add(nudAmountPaid);

            paymentPanel.Controls.Add(CreateFieldLabel("Subtotal:", 12, 55));
            ConfigureValueLabel(paymentPanel, lblSubtotal, 90, 55);
            paymentPanel.Controls.Add(CreateFieldLabel("Tax:", 230, 55));
            ConfigureValueLabel(paymentPanel, lblTax, 270, 55);
            paymentPanel.Controls.Add(CreateFieldLabel("Total:", 420, 55));
            ConfigureValueLabel(paymentPanel, lblTotal, 465, 55);
            paymentPanel.Controls.Add(CreateFieldLabel("Change:", 555, 55));
            ConfigureValueLabel(paymentPanel, lblChange, 605, 55);

            var btnComplete = new Button
            {
                Text = "Complete sale",
                Location = new Point(650, 82),
                Size = new Size(105, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White
            };
            btnComplete.Click += CompleteSaleClick;
            paymentPanel.Controls.Add(btnComplete);

            Controls.Add(title);
            Controls.Add(productPanel);
            Controls.Add(gridCart);
            Controls.Add(paymentPanel);
        }

        private static Label CreateFieldLabel(string text, int left, int top)
        {
            return new Label
            {
                Text = text,
                Location = new Point(left, top + 4),
                AutoSize = true,
                ForeColor = Color.FromArgb(70, 78, 88)
            };
        }

        private void ConfigureValueLabel(Panel panel, Label label, int left, int top)
        {
            label.Location = new Point(left, top + 4);
            label.AutoSize = true;
            label.Font = new Font(Font, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(35, 48, 66);
            panel.Controls.Add(label);
        }

        private void LoadProducts()
        {
            try
            {
                using (var db = new DataClasses1DataContext(Properties.Settings.Default.boholConnectionString))
                {
                    products = db.Products
                        .Where(p => p.IsActive && p.StockQty > 0)
                        .OrderBy(p => p.Name)
                        .ToList();
                }

                cboProduct.DataSource = products.Select(p => new ProductChoice(p)).ToList();
                if (cboProduct.Items.Count > 0)
                    cboProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load products: " + ex.Message, "Sale Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void AddItemClick(object sender, EventArgs e)
        {
            var choice = cboProduct.SelectedItem as ProductChoice;
            if (choice == null)
                return;

            CartLine line = cart.SingleOrDefault(item => item.Product.ProductId == choice.Product.ProductId);
            int quantity = Convert.ToInt32(nudQuantity.Value);
            int newQuantity = (line == null ? 0 : line.Quantity) + quantity;
            if (newQuantity > choice.Product.StockQty)
            {
                MessageBox.Show("The requested quantity exceeds available stock.", "Insufficient stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (line == null)
                cart.Add(new CartLine(choice.Product, quantity));
            else
                line.Quantity = newQuantity;

            RefreshCart();
        }

        private void RemoveItemClick(object sender, EventArgs e)
        {
            if (gridCart.SelectedRows.Count == 0)
                return;

            int productId = Convert.ToInt32(gridCart.SelectedRows[0].Cells["ProductId"].Value);
            cart.RemoveAll(line => line.Product.ProductId == productId);
            RefreshCart();
        }

        private void RefreshCart()
        {
            gridCart.Rows.Clear();
            foreach (CartLine line in cart)
            {
                gridCart.Rows.Add(line.Product.ProductId, line.Product.Name, line.Quantity,
                    line.Product.Price, line.LineTotal);
            }
            UpdateTotals();
        }

        private void TotalsChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void AmountPaidChanged(object sender, EventArgs e)
        {
            if (!updatingAmountPaid)
                amountPaidEntered = true;
            UpdateTotals();
        }

        private void PaymentChanged(object sender, EventArgs e)
        {
            amountPaidEntered = false;
            SetAmountPaid(Total);
            UpdateTotals();
        }

        private decimal Subtotal
        {
            get { return cart.Sum(line => line.LineTotal); }
        }

        private decimal TaxAmount
        {
            get { return Math.Round((Subtotal - nudDiscount.Value) * nudTaxRate.Value / 100M, 2); }
        }

        private decimal Total
        {
            get { return Subtotal - nudDiscount.Value + TaxAmount; }
        }

        private void UpdateTotals()
        {
            decimal subtotal = Subtotal;
            if (nudDiscount.Value > subtotal)
                nudDiscount.Value = subtotal;

            lblSubtotal.Text = subtotal.ToString("C2");
            lblTax.Text = TaxAmount.ToString("C2");
            lblTotal.Text = Total.ToString("C2");
            if (string.Equals(Convert.ToString(cboPayment.SelectedItem), "Card", StringComparison.OrdinalIgnoreCase) || !amountPaidEntered)
                SetAmountPaid(Total);
            lblChange.Text = Math.Max(0, nudAmountPaid.Value - Total).ToString("C2");
        }

        private void SetAmountPaid(decimal amount)
        {
            updatingAmountPaid = true;
            nudAmountPaid.Value = Math.Min(nudAmountPaid.Maximum, Math.Max(nudAmountPaid.Minimum, amount));
            updatingAmountPaid = false;
        }

        private void CompleteSaleClick(object sender, EventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Add at least one product before completing the sale.", "Sale", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Session.UserId.HasValue)
            {
                MessageBox.Show("Your session has expired. Please log in again.", "Sale", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudAmountPaid.Value < Total)
            {
                MessageBox.Show("The amount paid is less than the total.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            XElement itemsXml = new XElement("Items", cart.Select(line =>
                new XElement("Item",
                    new XAttribute("ProductId", line.Product.ProductId),
                    new XAttribute("Quantity", line.Quantity),
                    new XAttribute("UnitPrice", line.Product.Price.ToString("0.00", CultureInfo.InvariantCulture)))));

            try
            {
                using (var db = new DataClasses1DataContext(Properties.Settings.Default.boholConnectionString))
                {
                    var result = db.usp_Order_Create(
                        Session.UserId.Value,
                        null,
                        Convert.ToString(cboPayment.SelectedItem),
                        nudDiscount.Value,
                        nudTaxRate.Value,
                        nudAmountPaid.Value,
                        itemsXml).FirstOrDefault();

                    if (result == null || !result.Id.HasValue)
                        throw new InvalidOperationException("The database did not return a sale number.");
                }

                MessageBox.Show("Sale completed. Change: " + Math.Max(0, nudAmountPaid.Value - Total).ToString("C2"),
                    "Sale complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to complete sale: " + ex.Message, "Sale Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private sealed class ProductChoice
        {
            public Product Product { get; private set; }
            public string DisplayName { get { return Product.Name + " (" + Product.StockQty + " available) - " + Product.Price.ToString("C2"); } }

            public ProductChoice(Product product)
            {
                Product = product;
            }
        }

        private sealed class CartLine
        {
            public Product Product { get; private set; }
            public int Quantity { get; set; }
            public decimal LineTotal { get { return Product.Price * Quantity; } }

            public CartLine(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }
        }
    }
}
