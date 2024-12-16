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
using System.Xml.Linq;

namespace SocialMediaDesktopClient
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 registerForm = new Form4(); 
            registerForm.ShowDialog();
            this.Close();   
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 loginForm = new Form3();    
            loginForm.ShowDialog();    
            this.Close();
        }



    }
}
