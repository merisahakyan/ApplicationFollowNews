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
    public partial class News : Form
    {
        int pagenumber;
        int previouspagenumber;
        string pin,password;
        static string path;
        public News()
        {
            InitializeComponent();
            FirstPage();
        }
        //button1----Follow
        //button2----Submit
        //button3---GetNews
        //button4---or Sign in
        //button5---Sign in
        //button6---Prev
        // textbox1---email
        //textbox2---Name
        //textbox3----PIN
        //label1----eMail
        //label2----Name
        //label3---PIN
        //label4----email
        //label5----password

        public void FirstPage()
        {
            label1.Show();
            label2.Show();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            textBox1.Show();
            textBox2.Show();
            textBox3.Hide();
            button1.Show();
            button2.Hide();
            button3.Hide();
            button4.Show();
            button5.Hide();
            button6.Hide();
            pagenumber = 0;
        }
        public void AfterFollowButton()
        {
            label1.Hide();
            label2.Hide();
            label3.Show();
            label4.Hide();
            label5.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Show();
            button1.Hide();
            button2.Show();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            button6.Show();
            previouspagenumber = pagenumber;
            pagenumber = 1;
        }
        public void AfterSubmitButton()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            button1.Hide();
            button2.Hide();
            button3.Show();
            button4.Hide();
            button5.Hide();
            button6.Show();
            previouspagenumber = pagenumber;
            pagenumber = 2;
        }
        public void AfterOrSigninButton()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Show();
            label5.Show();
            textBox1.Show();
            textBox2.Show();
            textBox3.Hide();
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Show();
            button6.Show();
            previouspagenumber = pagenumber;
            pagenumber = 3;
        }
        public void AfterSigninButton()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            button1.Hide();
            button2.Hide();
            button3.Show();
            button4.Hide();
            button5.Hide();
            button6.Show();
            previouspagenumber = pagenumber;
            pagenumber = 4;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string name = textBox2.Text;
            SendMail.TextMessage(email, name,out pin,out password);
            AfterFollowButton();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool t = false;
            if (textBox3.Text == pin)
            {
                t = true;
                AfterSubmitButton();
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
                using (StreamWriter r = new StreamWriter(path, true))
                {
                    r.WriteLine(e.ToString());
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AfterOrSigninButton();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AfterSigninButton();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            switch (previouspagenumber)
            {
                case 0: FirstPage(); break;
                case 1: AfterFollowButton(); break;
                case 2: AfterSubmitButton(); break;
                case 3: AfterOrSigninButton(); break;
                case 4: AfterSigninButton(); break;

            }
        }
    }
}
