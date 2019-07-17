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

        
        void GetMovieCommentsCommandExecute()
        {
            Comments = serviceFacade.getMovieComments();
            ((MovieCommentList)currentWindow.MainFrame.Content).MovieCommentsGrid.ItemsSource = Comments;
        }

        bool GetMovieCommentsCommandCanExecute()
        {
            return true;
        }
    }
}
