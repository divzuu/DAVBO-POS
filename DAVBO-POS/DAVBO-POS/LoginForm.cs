using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DAVBO_POS
{
    public sealed partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if (username.Length == 0 || txtPassword.Text.Length == 0)
            {
                lblStatus.Text = "Enter your username and password.";
                return;
            }

            try
            {
                using (var db = new DataClasses1DataContext(Properties.Settings.Default.boholConnectionString))
                {
                    var result = db.usp_Login(username).FirstOrDefault();
                    if (result == null || !result.IsActive || !PasswordMatches(txtPassword.Text, result.Salt, result.PasswordHash))
                    {
                        lblStatus.Text = "Invalid credentials or inactive user.";
                        return;
                    }

                    Session.UserId = result.UserId;
                    Session.Username = result.Username;
                    Session.FullName = result.FullName;
                }

                Hide();
                using (var main = new MainForm())
                    main.ShowDialog(this);
                Show();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                Session.Clear();
                lblStatus.Text = "Unable to connect: " + ex.Message;
            }
        }

        private static bool PasswordMatches(string password, string salt, string expectedHash)
        {
            if (string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(expectedHash))
                return false;

            // Passwords are stored by the database as SHA-256(Salt + Password).
            // Keep the comparison in hexadecimal because PasswordHash is varchar(64).
            using (var sha256 = SHA256.Create())
            {
                string hash = BitConverter.ToString(
                    sha256.ComputeHash(Encoding.UTF8.GetBytes(salt + password)))
                    .Replace("-", string.Empty);
                return string.Equals(hash, expectedHash.Trim(), StringComparison.OrdinalIgnoreCase);
            }
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}