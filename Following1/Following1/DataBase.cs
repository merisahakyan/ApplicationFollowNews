using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Following1
{
    public class DataBase
    {
        public static bool CheckSignIn(string email, string password)
        {
            using (var db = new UsersContext())
            {
                var query = from b in db.User_Table
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

                var query = from b in db.User_Table
                            select b;

                foreach (var item in query)
                {
                    if (item.eMail == email)
                        t = false;
                    break;
                }

                if (t)
                {
                    var user = new User_Table { Name = name, Password = password, eMail = email };
                    db.User_Table.Add(user);
                }

            }
        }
    }
}
