using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Following;
using System.IO;

namespace NewsForm
{
    public partial class News : Form
    {
        public static bool CheckSignIn(string email, string password)
        {
            using (var db = new UsersContext())
            {
                var query = from b in db.Users_Table
                            select b;

                foreach (var item in query)
                {
                    if (item.eMail == email && item.Password == password)
                        return true;
                }
                return false;
            }
        }

        public static void AddNewUser(string name, string email, string password, out bool t)
        {
            t = true;
            using (var db = new UsersContext())
            {

                var query = from b in db.Users_Table
                            select b;

                foreach (var item in query)
                {
                    if (item.eMail == email)
                        t = false;
                    break;
                }

                if (t)
                {
                    var user = new Users_Table { Name = name, Password = password, eMail = email };
                    db.Users_Table.Add(user);

                }


            }
        }
        int pagenumber;
        int previouspagenumber;
        string pin, password;
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
        //label6---validation

        public void FirstPage()
        {

            label1.Show();
            label2.Show();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
            label6.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Show();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
            label6.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
            label6.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
            label6.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
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
        private async void button1_Click(object sender, EventArgs e)
        {

            button1.Enabled = false;
            var task = Task<bool>.Run(() =>
             {
                 bool t;
                 string email = textBox1.Text;
                 string name = textBox2.Text;
                 password = Password.NewPassword();
                 AddNewUser(name, email, password, out t);
                 SendMail.TextMessage(email, name, out pin, password);
                 return t;

             });
            var result = await task;
            if (result)
                AfterFollowButton();
            else
                label6.Text = "Email is already in use";
            button1.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox3.Text == pin)
            {

                AfterSubmitButton();
            }
            else
            {
                textBox3.Text = "";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {

            try
            {
                File.Delete(path);
            }
            catch
            {
                //file does nor exist
            }
            var task = Task.Run(() =>
            {
                MyNews mn = new MyNews("BlogNews");
                mn.DailyNews += ShowNews;
                mn.BroadcastNews();
                //System.Diagnostics.Process.Start(path);
            });
            await task;

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
            string email = textBox1.Text;
            string password = textBox2.Text;
            if (CheckSignIn(email, password))
                AfterSigninButton();
            else
            {
                label6.Show();
                label6.Text = "Uncorrect email or password";
            }


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
