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
        /*
            Exemple of use found in: 
            https://www.entityframeworktutorial.net/crud-operation-in-connected-scenario-entity-framework.aspx
            https://www.learnentityframeworkcore.com/dbset/querying-data
        */

        public UserDao()
        {

        }

        public User createUserDao(String _login, String _password)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                //Create entity to insert and set his properties
                var newUser = new User()
                {
                    login = _login,
                    password = _password,
                    
                };


                db.Entry(newUser).State = System.Data.Entity.EntityState.Added;//Insert property in the DB
                int res = db.SaveChanges();//Save the modification to th DB if the save is successful the function return "true"

                if (res > 0)
                {
                    MessageBox.Show("User successfuly created");
                    return newUser;
                }
                else
                {
                    MessageBox.Show("Error cant create user");
                    return newUser;
                }
            }
        }

        public User loginDao(string login, string password)
        {
            //DataModelContainer container the string to reference/Target wich DB to used(ctrl+click or alt+click to see the content of the class)
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var user = (from u in db.UserSet
                           where u.login == login && u.password == password
                           select u).FirstOrDefault<User>();//Récupère le premier user correspondant o critère de recherche

                if (user != null)
                    MessageBox.Show("Welcome " + user.login);
                
                return user;
            }
        }

        public User getUserDao(int userID)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var user = (from u in db.UserSet
                            where u.Id == userID
                            select u).FirstOrDefault<User>();

                if (user != null)
                    MessageBox.Show("the user exist" + user.login);


                return user;
            }
        }

        public bool isUserExists(int id)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var user = (from u in db.UserSet
                            where u.Id == id
                            select u).FirstOrDefault<User>();
                return user != null;
            }
        }

        public bool deleteUserDao(User userTodelete)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var user = (from u in db.UserSet
                            where u.Id == userTodelete.Id
                            select u).FirstOrDefault<User>();


                if (user == null)
                {

                    return false;
                }
                else
                {

                    db.Entry(user).State = System.Data.Entity.EntityState.Deleted;//Delete element in DB according to
                    db.SaveChanges();
                    return true;
                }
            }
        }

        public bool updateUserDao(int userId, String Login, String Password)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var userToUpdate = db.UserSet.Where(u => u.Id == userId).First();//Search the movie to update in db

                
                userToUpdate.login = Login;
                userToUpdate.password = Password;

                db.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
               

                int res = db.SaveChanges();

                if (res > 0)
                {
                    MessageBox.Show("User data update successful");
                    return true;
                }
                else
                {
                    MessageBox.Show("User data can't be updated, try again");
                    return false;
                }
            }
        }
    }
}
