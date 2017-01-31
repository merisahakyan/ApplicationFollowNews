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
using System.IO;

namespace NewsForm
{
    public partial class Form1 : Form
    {
        string pin;
        static string path;
        public Form1()
        {
            InitializeComponent();
            label3.Hide();
            textBox3.Hide();
            button2.Hide();
            button3.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string name = textBox2.Text;
            SendMail.TextMessage(email, name, out pin);
            label1.Hide();
            label2.Hide();
            textBox1.Hide();
            textBox2.Hide();
            button1.Hide();

            label3.Show();
            textBox3.Show();
            button2.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool t = false;
            if (textBox3.Text == pin)
            {
                t = true;
                label3.Hide();
                textBox3.Hide();
                button2.Hide();

                button3.Show();
            }
            else
            {
                textBox3.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            try
            {
                File.Delete(path);
            }
            catch
            {
                //file does nor exist
            }
            MyNews mn = new MyNews("BlogNews");
            mn.DailyNews += ShowNews;
            mn.BroadcastNews();
            System.Diagnostics.Process.Start(path);
            
        }
        public static void ShowNews(object sender, EventArgs e)
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/BlogNews.txt";
            var agency = (MyNews)sender;
            if (agency != null)
            {
                using (StreamWriter r = new StreamWriter(path,true))
                {
                    r.WriteLine(e.ToString());
                }
            }
                        
        }
    }
}
