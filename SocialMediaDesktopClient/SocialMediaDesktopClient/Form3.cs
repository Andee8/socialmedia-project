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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            string email = textBox3.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userService = new UserService();
            var users = await userService.GetUsersByNameAndEmailAsync(name, email);

            if (users != null && users.Count > 0)
            {
                Global.LoggedInUser = users.First();
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
            else
            {
                MessageBox.Show("No account found with these credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
    }
    }
}
