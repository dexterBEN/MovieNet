using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNet.Facade;
using MovieNet.utils;

namespace MovieNet.ViewModel
{
    public class MovieCommentFormViewModel: ViewModelBase
    {
        private String _commentContent;

        ServiceFacade serviceFacade;
        public RelayCommand CreateMovieCommentCommand { get; }

        public MovieCommentFormViewModel()
        {
            serviceFacade = Singleton.GetInstance;
            CreateMovieCommentCommand = new RelayCommand(CreateMovieCommentExecute, CreateMovieCommentCanExecute);
        }

        public String CommentContent
        {
            get { return _commentContent; }

            set
            {
                _commentContent = value;
                RaisePropertyChanged();
            }
        }

        void CreateMovieCommentExecute()
        {
            var userId =  (int) Application.Current.Properties["userId"];
            var movieId = (int)Application.Current.Properties["movieId"];

            serviceFacade.createComment(userId, movieId, CommentContent);
        }

        bool CreateMovieCommentCanExecute()
        {
            return true;
        }
    }
}
