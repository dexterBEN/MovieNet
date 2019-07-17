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

        public User getUserDao(string login, string password)
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
    }
}
