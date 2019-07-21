using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieNet.Dao
{
    public class CommentDao
    {
        public CommentDao()
        {

        }

        public List<Comment> getMovieComments(int movieId)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var comments = (from c in db.CommentSet.Include("User").Include("Movie")
                                where c.MovieId == movieId
                                select c).ToList();



                return comments;
            }
        }

        public Comment getComment()
        {
            return null;
        }

        public int createComment(int userId, int movieId, string commentContent)
        {
            using (var db = new DataModelContainer())
            {
                db.Database.Connection.Open();

                var myComment = new Comment()
                {
                    content = commentContent,
                    UserId = userId,
                    MovieId = movieId
                };

                db.Entry(myComment).State = System.Data.Entity.EntityState.Added;
                int res = db.SaveChanges();

                if (res > 0)
                {
                    MessageBox.Show("Comment creation successful");
                    return res;
                }
                else
                {
                    MessageBox.Show("Error cant create comment");
                    return res;
                }
            }
        }
    }
}
