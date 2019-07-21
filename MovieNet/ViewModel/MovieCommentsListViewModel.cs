using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using MovieNet.Facade;
using MovieNet.utils;
using System.Windows;
using MovieNet.Views;

namespace MovieNet.ViewModel
{
    public class MovieCommentsListViewModel: ViewModelBase
    {
        /*private String _userLogin;*/
        private String _movieTitle;

        public RelayCommand GetMovieCommentsCommand { get; }
        public List<Comment> Comments { get; set; }
        ServiceFacade serviceFacade;
        MainWindow currentWindow;

        public MovieCommentsListViewModel()
        {
            serviceFacade = Singleton.GetInstance;
            currentWindow = (MainWindow)Application.Current.MainWindow;
            GetMovieCommentsCommand = new RelayCommand(GetMovieCommentsCommandExecute, GetMovieCommentsCommandCanExecute);
        }

        /*public String UserLogin
        {
            get { return _userLogin; }

            set
            {
                _userLogin = value;
                RaisePropertyChanged();
            }
        }*/

        public String MovieTitle
        {
            get { return _movieTitle; }

            set
            {
                _movieTitle = value;
                RaisePropertyChanged();
            }
        }


        void GetMovieCommentsCommandExecute()
        {
            var movieId = (int) Application.Current.Properties["movieId"];
            MovieTitle =(String) Application.Current.Properties["movieTitle"];     

            Comments = serviceFacade.getMovieComments(movieId);

            ((MovieCommentList)currentWindow.MainFrame.Content).MovieCommentsGrid.ItemsSource = Comments;
        }

        bool GetMovieCommentsCommandCanExecute()
        {
            return true;
        }
    }
}
