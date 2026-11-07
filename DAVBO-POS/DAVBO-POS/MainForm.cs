using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DAVBO_POS
{
    public partial class MainForm : Form
    {
        private Label lblActivityMessage;

        public MainForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;

            // Window dimensions
            Size = new Size(816, 489);
            MinimumSize = new Size(816, 489);
            MaximumSize = new Size(816, 489);

            Text = "DAVBO POS - Dashboard";

            BuildDashboard();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                LoadDashboardMetrics();
                LoadRecentActivity();
            }
        }

        private void BuildDashboard()
        {
            Controls.Clear();

            // =========================
            // SIDEBAR
            // =========================

            Panel sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 190,
                BackColor = Color.FromArgb(35, 48, 66)
            };

            Label lblLogo = new Label
            {
                Text = "DAVBO POS",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                Location = new Point(18, 28),
                Size = new Size(160, 40)
            };

            sidebar.Controls.Add(lblLogo);

            Label lblMenu = new Label
            {
                Text = "MAIN MENU",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(18, 95),
                Size = new Size(150, 25)
            };

            sidebar.Controls.Add(lblMenu);

            Button btnDashboard = CreateMenuButton(
                "Dashboard", 130);

            Button btnInventory = CreateMenuButton(
                "Inventory", 180);

            Button btnSales = CreateMenuButton(
                "New sale", 230);

            Button btnLogout = CreateMenuButton(
                "Logout", 395);

            sidebar.Controls.Add(btnDashboard);
            sidebar.Controls.Add(btnInventory);
            sidebar.Controls.Add(btnSales);
            sidebar.Controls.Add(btnLogout);

            btnDashboard.Click += (s, e) =>
            {
                LoadDashboardMetrics();
                LoadRecentActivity();
            };

            btnInventory.Click += (s, e) =>
            {
                using (ProductForm productForm = new ProductForm())
                {
                    productForm.ShowDialog(this);
                }

                // ProductForm commits changes before closing, so refresh the dashboard
                // when control returns to MainForm.
                LoadDashboardMetrics();
                LoadRecentActivity();
            };

            btnSales.Click += (s, e) =>
            {
                using (SalesForm salesForm = new SalesForm())
                {
                    if (salesForm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadDashboardMetrics();
                        LoadRecentActivity();
                    }
                }
            };

            btnLogout.Click += (s, e) =>
            {
                Session.Clear();
                LoginForm login = new LoginForm();

                login.FormClosed += (s2, args) =>
                {
                    if (!IsDisposed)
                    {
                        Close();
                    }
                };

                Hide();
                login.Show();
            };

            // =========================
            // MAIN CONTENT
            // =========================

            Panel content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250)
            };

            // =========================
            // HEADER
            // =========================

            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = Color.White
            };

            Label lblHeader = new Label
            {
                Text = "Dashboard Overview",
                Font = new Font("Segoe UI", 17, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 48, 66),
                Location = new Point(20, 15),
                AutoSize = true
            };

            header.Controls.Add(lblHeader);

            // =========================
            // METRIC CARDS - 2 x 2
            // =========================

            // METRIC CARDS - 2 x 2 LAYOUT
            TableLayoutPanel cards = new TableLayoutPanel
            {
                Location = new Point(15, 90),
                Size = new Size(560, 230),
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(3),
                BackColor = Color.Transparent
            };

            cards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            cards.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            cards.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            cards.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            // TOP ROW
            cards.Controls.Add(
                CreateMetricCard(
                    "Today's Sales",
                    "₱0.00",
                    Color.FromArgb(39, 174, 96),
                    out lblTodaySalesValue),
                0, 0);

            cards.Controls.Add(
                CreateMetricCard(
                    "Today's Orders",
                    "0",
                    Color.FromArgb(52, 152, 219),
                    out lblTodayOrdersValue),
                1, 0);

            // BOTTOM ROW
            cards.Controls.Add(
                CreateMetricCard(
                    "Low Stock Items",
                    "0",
                    Color.FromArgb(230, 126, 34),
                    out lblLowStockValue),
                0, 1);

            cards.Controls.Add(
                CreateMetricCard(
                    "Active Products",
                    "0",
                    Color.FromArgb(142, 68, 173),
                    out lblActiveProductsValue),
                1, 1);

            // =========================
            // RECENT ACTIVITY
            // =========================

            Panel activityPanel = new Panel
            {
                Location = new Point(20, 325),
                Size = new Size(565, 105),
                BackColor = Color.White
            };

            Label lblActivityTitle = new Label
            {
                Text = "Recent Activity",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 48, 66),
                Location = new Point(15, 12),
                AutoSize = true
            };

            lblActivityMessage = new Label
            {
                Text = "No recent transactions or inventory updates.",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                Location = new Point(10, 8),
                AutoSize = true,
                MaximumSize = new Size(515, 0)
            };

            Panel activityScroll = new Panel
            {
                Location = new Point(10, 40),
                Size = new Size(545, 60),
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(0)
            };

            activityScroll.Controls.Add(lblActivityMessage);

            activityPanel.Controls.Add(lblActivityTitle);
            activityPanel.Controls.Add(activityScroll);

            // =========================
            // ADD CONTROLS
            // =========================

            content.Controls.Add(activityPanel);
            content.Controls.Add(cards);
            content.Controls.Add(header);

            Controls.Add(content);
            Controls.Add(sidebar);
        }

        // =========================
        // MENU BUTTON
        // =========================

        private Button CreateMenuButton(string text, int top)
        {
            return new Button
            {
                Text = text,
                Location = new Point(12, top),
                Size = new Size(166, 40),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(35, 48, 66),
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
        }

        // =========================
        // METRIC CARD
        // =========================

        private Panel CreateMetricCard(
            string title,
            string initialValue,
            Color accent,
            out Label valueLabel)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(5)
            };

            Panel accentBar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = accent
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                Location = new Point(17, 17),
                AutoSize = true
            };

            valueLabel = new Label
            {
                Text = initialValue,
                Font = new Font("Segoe UI", 19, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 48, 66),
                Location = new Point(17, 48),
                AutoSize = true
            };

            card.Controls.Add(titleLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(accentBar);

            return card;
        }

        // =========================
        // LOAD DASHBOARD METRICS
        // =========================

        private void LoadDashboardMetrics()
        {
            try
            {
                using (var db = new DataClasses1DataContext(
                    Properties.Settings.Default.boholConnectionString))
                {
                    var result = db.usp_Report_Dashboard()
                                   .FirstOrDefault();

                    if (result == null)
                        return;

                    lblTodaySalesValue.Text =
                        Convert.ToDecimal(result.TodaySales)
                        .ToString("C2");

                    lblTodayOrdersValue.Text =
                        Convert.ToInt32(result.TodayOrders)
                        .ToString();

                    lblLowStockValue.Text =
                        Convert.ToInt32(result.LowStockCount)
                        .ToString();

                    lblActiveProductsValue.Text =
                        Convert.ToInt32(result.ActiveProducts)
                        .ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load dashboard metrics: " + ex.Message,
                    "Dashboard Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadRecentActivity()
        {
            try
            {
                using (var db = new DataClasses1DataContext(
                    Properties.Settings.Default.boholConnectionString))
                {
                    var activity = db.usp_Report_RecentActivity().ToList();
                    lblActivityMessage.Text = activity.Count == 0
                        ? "No recent transactions or inventory updates."
                        : string.Join(Environment.NewLine, activity.Select(item =>
                            item.ActivityDate.ToString("g") + " - " + item.ActivityType + ": " + item.Description));
                }
            }
            catch (Exception ex)
            {
                lblActivityMessage.Text = "Unable to load recent activity: " + ex.Message;
            }
        }
    }
}

