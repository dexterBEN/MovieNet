using MovieNet.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNet.Facade
{
    public class ServiceFacade
    {
        protected UserDao _userDao;
        protected MovieDao _movieDao;
        protected CommentDao _commentDao;

        public List<Movie> Movies = null;
        public List<Comment> Comments = null;

        public ServiceFacade()
        {
            this._userDao = new UserDao();
            this._movieDao = new MovieDao();
            this._commentDao = new CommentDao();
        }

        public User getUser(string login, string password)
        {
            User user = _userDao.getUserDao(login, password);

            if(user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public List<Movie> getMovies()
        {
             Movies = _movieDao.getMoviesDao();
             return Movies;
        }

        public Movie getMovie(int movieId)
        {
            Movie movie = _movieDao.getMovie(movieId);
            return movie;
        }

        public void deleteMovie(int movieId)
        {
            _movieDao.deleteMovieDao(movieId);
        }

        public void createMovie(string title, string kind, string synopsis)
        {
            _movieDao.createMovieDao(title, kind, synopsis);
        }

        public int updateMovie(int movieId, string movieTitle, string movieKind, string movieSynopsis)
        {
            return _movieDao.updateMovieDao(movieId, movieTitle, movieKind, movieSynopsis);
        }

        public void createComment(int userId, int movieId, string comment)
        {
            _commentDao.createComment(userId, movieId, comment);
        }

        public List<Comment> getMovieComments()
        {
           //Comments =  _commentDao.getMovieComments(movie);
            return null;
        }
    }
}
