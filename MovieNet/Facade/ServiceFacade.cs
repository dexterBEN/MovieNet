﻿using MovieNet.Dao;
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
        protected RateDao _rateDao;

        public List<Movie> Movies = null;
        public List<Comment> Comments = null;

        public ServiceFacade()
        {
            this._userDao = new UserDao();
            this._movieDao = new MovieDao();
            this._commentDao = new CommentDao();
            this._rateDao = new RateDao();
        }

        public User getUser(string login, string password)
        {
            User user = _userDao.loginDao(login, password);

            if(user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User getUser(int userID)
        {
            User user = _userDao.getUserDao(userID);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User createUser(String login, String password)
        {
            User userCreated = _userDao.createUserDao(login, password);

            if(userCreated != null)
            {
                return userCreated;
            }
            else
            {
                return null;
            }
        }

        public bool deleteUser(User userToDelete)
        {
            if (!_userDao.isUserExists(userToDelete.Id))
                return false;
            _rateDao.deleteRatesByUser(userToDelete.Id);
            _commentDao.deleteCommentsByUser(userToDelete.Id);
            return _userDao.deleteUserDao(userToDelete);
        }

        public bool updateUser(int userId, String Login, String Password)
        {
           return  _userDao.updateUserDao(userId, Login, Password);
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

        public List<Movie> searchMovie(List<Movie> movies, string inputSearch)
        {
           var searchRes = _movieDao.searchMovie(movies, inputSearch);
           return searchRes;
        }

        public List<Comment> getMovieComments(int movieId)
        {
            Comments =  _commentDao.getMovieComments(movieId);
            return Comments;
        }

        public void addRateToMovie(int userId, int movieId, int rateValue)
        {
            _rateDao.addRate(userId, movieId, rateValue);
        }

        public int getMovieScore(int movieId)
        {
            var movieScore = _rateDao.calculateMovieScore(movieId);

            return movieScore;
        }
    }
}
