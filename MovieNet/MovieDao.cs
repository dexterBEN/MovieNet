using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNet
{
    public class MovieDao
    {
        /*
            Exemple of use found in: 
            https://www.entityframeworktutorial.net/crud-operation-in-connected-scenario-entity-framework.aspx
            https://www.learnentityframeworkcore.com/dbset/querying-data
        */
        public MovieDao()
        {

        }

        public List<Movie> getMoviesDao()
        {
            //DataModelContainer container the string to reference/Target wich DB to used(ctrl+click or alt+click to see the content of the class)
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();
                
                var movies = (from m in db.MovieSet
                             select m).ToList();

                return movies;
            }
        }

        public Movie getMovie(int movieId)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var movieToGet = (from m in db.MovieSet
                                    where m.Id == movieId
                                    select m).First();

                return movieToGet;
            }
        }

        public int createMovieDao(string _title, string _kind, string _synopsis)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                //Create entity to insert and set his properties
                var myMovie = new Movie()
                {
                    title = _title,
                    kind = _kind,
                    synopsis = _synopsis,
                };

                
                db.Entry(myMovie).State = System.Data.Entity.EntityState.Added;//Insert property in the DB
                int res = db.SaveChanges();//Save the modification to th DB if the save is successful the function return "true"

                if (res > 0)
                {
                    MessageBox.Show("Movie creation successful");
                    return res;
                }
                else
                {
                    MessageBox.Show("Error cant create movie");
                    return res;
                }  
            }
                
        }

        public bool deleteMovieDao(int movieId)
        {
            using (var db = new DataModelContainer())
            {
                var movieToDelete = new Movie { Id = movieId };

                db.Entry(movieToDelete).State = System.Data.Entity.EntityState.Deleted;//Delete element in DB according to the ID

                db.SaveChanges();
                //db.Database.Connection.Close();
            }
                return true;
        }

        public int updateMovieDao(int movieId ,string movieTitle, string movieKind, string movieSynopsis)
        {
            using(var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var movieSelected = db.MovieSet.Where(m => m.Id == movieId).First();//Search the movie to update in db

                //set the property of the movie
                movieSelected.title = movieTitle;
                movieSelected.kind = movieKind;
                movieSelected.synopsis = movieSynopsis;

                int res = db.SaveChanges();

                if (res > 0)
                {
                    MessageBox.Show("Movie update successful");
                    return res;
                }
                else
                {
                    MessageBox.Show("Error cant update movie");
                    return res;
                }
            }
        }
    }
}
