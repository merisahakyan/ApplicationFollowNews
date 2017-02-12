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
                var query = from b in db.MyUsers
                            select b;

                foreach (var item in query)
                {
                    if (item.eMail == email && item.Password == password)
                        return true;
                }
                return false;
            }
        }

        public static bool CheckMailForRegister(string mail)
        {
            bool t = true;
            using (var db = new UsersContext())
            {

                var query = from b in db.MyUsers
                            select b;

                foreach (var item in query)
                {
                    if (item.eMail == mail)
                        t = false;
                    break;
                }
                return t;
            }
        }
        public static void AddNewUser(string name, string email, string password)
        {


            using (var db = new UsersContext())
            {
                if (CheckMailForRegister(email))
                {
                    var user = new MyUser { Name = name, Password = password, eMail = email };
                    db.MyUsers.Add(user);
                    db.SaveChanges();
                }


            }
        }
        int pagenumber;
        string pin, password;
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/BlogNews.html";
        string email, name;

        public News()
        {
            InitializeComponent();
            FirstPage();
        }
        //button1----Follow
        //button2----Submit
        //button3----GetNews
        //button4----or Sign in
        //button5----Sign in
        //button6----Prev
        //textbox1---email
        //textbox2---Name
        //textbox3---PIN
        //textbox4---Password
        //label1-----eMail
        //label2-----Name
        //label3-----PIN
        //label4-----email
        //label5-----password
        //label6-----validation
        //combobox1--language

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
            textBox4.Text = "";
            textBox1.Show();
            textBox2.Show();
            textBox3.Hide();
            textBox4.Hide();
            button1.Show();
            button2.Hide();
            button3.Hide();
            button4.Show();
            button5.Hide();
            button6.Hide();
            comboBox1.Hide();
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
            textBox4.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            button1.Hide();
            button2.Show();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            button6.Show();
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
            textBox4.Text = "";
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            button1.Hide();
            button2.Hide();
            button3.Show();
            button4.Hide();
            button5.Hide();
            button6.Hide();
            comboBox1.Hide();
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
            textBox4.Text = "";
            textBox1.Show();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Show();
            textBox4.PasswordChar = '*';
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Show();
            button6.Show();
            comboBox1.Hide();
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
            textBox4.Text = "";
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            button1.Hide();
            button2.Hide();
            button3.Show();
            button4.Hide();
            button5.Hide();
            button6.Hide();
            comboBox1.Show();
            comboBox1.Items.AddRange(new string [] { "Armenian", "Russian"});
            comboBox1.Text = "Armenian";
            pagenumber = 4;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            button1.Enabled = false;
            email = textBox1.Text;
            name = textBox2.Text;
            bool mailcheck = SendMail.CheckMail(email);
            if (mailcheck)
            {
                var task = Task<bool>.Run(() =>
               {
                   if (CheckMailForRegister(email))
                   {
                       password = Password.NewPassword();
                       SendMail.TextMessage(email, name, out pin, password);
                   }
                   return CheckMailForRegister(email);


               });
                var result = task.Result;
                if (result)
                {
                    AfterFollowButton();
                }
                else
                {
                    label6.Text = "Email is already in use";
                    label6.Show();
                    textBox1.Text = "";
                }
                button1.Enabled = true;
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
                label6.Show();
                label6.Text = "Uncorrect Email format";
            }
            button1.Enabled = true;


        }
        int pincount = 0;

        private void button2_Click(object sender, EventArgs e)
        {

            if (pincount < 2)
            {
                if (textBox3.Text == pin)
                {
                    AddNewUser(name, email, password);
                    AfterSubmitButton();
                }
                else
                {
                    pincount++;
                    label6.Show();
                    label6.Text = "Wrong PIN !";
                    textBox3.Text = "";
                }
            }
            else
            {
                FirstPage();
                pincount = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            try
            {
                File.Delete(path);
            }
            catch
            {
                //file does nor exist
            }
            object selected = comboBox1.SelectedItem;
            var task = Task.Run(() =>
            {
                
                MyNews mn = new MyNews("BlogNews",selected.ToString());
                mn.BroadcastNews(path);


            });

            task.ContinueWith((t) =>
            {
                System.Diagnostics.Process.Start(path);
            });




        }

        private void button4_Click(object sender, EventArgs e)
        {
            AfterOrSigninButton();
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox4.Text;
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
            switch (pagenumber)
            {
                case 1: FirstPage(); break;
                case 3: FirstPage(); break;
                case 4: AfterOrSigninButton(); break;
            }
        }
    }
}
