using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
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
                    db.SaveChanges();
                }


            }
        }
            static void Main(string[] args)
        {
            string name = Console.ReadLine();
            string email = Console.ReadLine();
            string password = Console.ReadLine();
            bool t;
            AddNewUser(name, email, password, out t);
            Console.WriteLine(t);
            //Console.WriteLine(DataBase.CheckSignIn(email,password));
            Console.ReadLine();
        }
    }
}
