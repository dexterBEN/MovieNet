using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNet.Dao
{
    public class RateDao
    {
        public RateDao()
        {

        }

        public int addRate(int userId, int movieId, int rateValue)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var myRate = new Rate()
                {
                    rate = rateValue,
                    UserId = userId,
                    MovieId = movieId
                };

                db.Entry(myRate).State = System.Data.Entity.EntityState.Added;
                int res = db.SaveChanges();

                if(res > 0)
                {
                    MessageBox.Show("successfuly add rate");
                    return res;
                }
                else
                {
                    MessageBox.Show("Error can't add rate to the movie");
                    return res;
                }
            }
        }

        public int calculateMovieScore(int movieId)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var score = 0;

                var rates = (from r in db.RateSet
                             where r.MovieId == movieId
                             select r).ToList();

                var numberOfRate = rates.Count;

                foreach(Rate rate in rates)
                {
                    score += rate.rate;
                }
                score = score / numberOfRate;
                return score;
            }
        }
    }
}
