using SocialMediaClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialMediaDesktopClient
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userService = new UserService();
            var existingUsers = await userService.GetUsersByNameAndEmailAsync(name, email);

            if (existingUsers != null && existingUsers.Count > 0)
            {
                MessageBox.Show("An account with this email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newUser = new User { Name = name, Email = email };
            bool isCreated = await userService.CreateUserAsync(newUser);

            if (isCreated)
            {
                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create the account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
