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
    class UserDao
    {
        //DbContext dbContext = new DbContext(@"Server=.\SQLExpress;AttachDbFilename=|DataDirectory|MyBDD.mdf;Database=MYBDD;");
        //DataModelContainer dmc = new DataModelContainer();
  

        public UserDao()
        {
            //DataModelContainer ctx = new DataModelContainer();
            //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\benoni.d\source\repos\MovieNet\MovieNet\MyBDD.mdf;Integrated Security=True");
        }


        public User getUserDao(string login, string password)
        {
            /*dbContext.Database.Connection.Open();
            string connectionStat;
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\benoni.d\source\repos\MovieNet\MovieNet\MyBDD.mdf;Integrated Security=True");

            con.Open();
            connectionStat =  (con.State == ConnectionState.Open) ? "con success" : "con fail";

            if(connectionStat == "con success")
            {
                MessageBox.Show("BDD is open");
            }*/

            //sqlCmd = new SqlCommand("SELECT * FROM User WHERE login= '"+login+"' AND password='"+password+"'", con);
            //dataReader = sqlCmd.ExecuteReader();

            //MessageBox.Show(connectionStat);
            //User user;
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                /*var users = (from u in db.Users
                            select u).Count();*/

                //var user = db.Users.SqlQuery("Select * From Users WHERE login=@login AND password=@password", new SqlParameter("@login", login), new SqlParameter("@password", password)).FirstOrDefault();


                //Query Target UserSet not Users
                var user = (from u in db.UserSet
                           where u.login == login && u.password == password
                           select u).FirstOrDefault<User>();

                if (user != null)
                    MessageBox.Show("Welcome " + user.login);
                

                //db.Database.Connection.Close();
                return user;
            }
        }
    }
}
