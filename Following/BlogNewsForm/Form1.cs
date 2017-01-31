using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Following;

namespace BlogNewsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string name = textBox2.Text;
            string pin;
            SendMail.TextMessage(email, name,out pin);
            

        }
    }
}
