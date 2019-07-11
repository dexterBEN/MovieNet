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
        public MovieDao()
        {

        }

        public List<Movie> getMoviesDao()
        {

            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                //movieList = db.Movies.SqlQuery("Select * From Movies").ToList();

                var movies = (from m in db.MovieSet
                             select m).ToList();

                //movieList = movies;

               // db.Database.Connection.Close();
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

                var myMovie = new Movie()
                {
                    title = _title,
                    kind = _kind,
                    synopsis = _synopsis,
                    
                };

                //db.MovieSet.Add(myMovie);
                db.Entry(myMovie).State = System.Data.Entity.EntityState.Added;
                int res = db.SaveChanges();

                //db.Database.Connection.Close();

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

                db.Entry(movieToDelete).State = System.Data.Entity.EntityState.Deleted;

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

                var movieSelected = db.MovieSet.Where(m => m.Id == movieId).First();

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
