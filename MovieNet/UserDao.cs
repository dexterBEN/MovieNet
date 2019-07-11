using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;

namespace MovieNet
{
    public class UserDao
    {
        public UserDao()
        {

        }

        public User getUserDao(string login, string password)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var user = (from u in db.UserSet
                           where u.login == login && u.password == password
                           select u).FirstOrDefault<User>();

                if (user != null)
                    MessageBox.Show("Welcome " + user.login);
                
                return user;
            }
        }
    }
}
