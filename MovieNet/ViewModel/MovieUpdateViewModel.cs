using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNet.Facade;
using MovieNet.utils;

namespace MovieNet.ViewModel
{
    public class MovieUpdateViewModel: ViewModelBase
    {
        private String _title;
        private String _kind;
        private String _synopsis;

        MovieDao movieDao;
        MainWindow currentWindow;
        ServiceFacade serviceFacade;
        public RelayCommand UpdateMovieCommand { get; }
        public RelayCommand GetMovieSelectedCommand { get;  }

        public MovieUpdateViewModel()
        {
            movieDao =  new MovieDao();
            UpdateMovieCommand = new RelayCommand(UpdateMovieCommandExecute, UpdateMovieCommandCanExecute);
            currentWindow = (MainWindow)Application.Current.MainWindow;
            serviceFacade = Singleton.GetInstance;

            //previousMovie = movieDao.getMovie((int)currentWindow.MainFrame.NavigationService.Source.OriginalString.ElementAt(35));
            //GetMovieSelectedCommand = new RelayCommand(GetMovieSelectedCommandExecute, GetMovieSelectedCommandCanExecute);
        }

        public String Title
        {
            get { return _title; }

            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public String Kind
        {
            get { return _kind; }

            set
            {
                _kind = value;
                RaisePropertyChanged();
            }
        }

        public String Synopsis
        {
            get { return _synopsis; }

            set
            {
                _synopsis = value;
                RaisePropertyChanged();
            }
        }


        /*
         * Can be used to fill the form with the previous data of movie:
         * 
         * public Movie GetMovieSelectedCommandExecute()
        {
            var idmovie = currentWindow.MainFrame.NavigationService.Source.OriginalString.ElementAt(35);
            var idInt = int.Parse(idmovie.ToString());

            var movieSelected = movieDao.getMovie(idInt);

            MessageBox.Show("id du film sélectionné" + movieSelected.Id.ToString());
            return movieSelected;
        }

        bool GetMovieSelectedCommandCanExecute()
        {
            return true;
        }*/

        void UpdateMovieCommandExecute()
        {
            /* var url =  currentWindow.MainFrame.NavigationService.CurrentSource;
             MessageBox.Show("The movie you want modif have the title:" + Application.Current.Properties.Values.ToString());*/

            //currentWindow.MainFrame.CurrentSource.

            /*var url = currentWindow.MainFrame.NavigationService.CurrentSource.ToString();
            var id = url.Substring(url.IndexOf('=')+1);
           //var url =  currentWindow.MainFrame.CurrentSource;

            MessageBox.Show("the value:"+ url.Substring(35));*/


            var uri = currentWindow.MainFrame.NavigationService.CurrentSource.ToString();
            var idStr = uri.Substring(uri.IndexOf('=') + 1);

            var idInt = int.Parse(idStr.ToString());

            var movieSelected = serviceFacade.getMovie(idInt);
            movieSelected.title = Title;
            movieSelected.kind = Kind;
            movieSelected.synopsis = Synopsis;

            var movieToUpdate = serviceFacade.updateMovie(movieSelected.Id, movieSelected.title, movieSelected.kind, movieSelected.synopsis);

            if (movieToUpdate > 0)
            {
                currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
            }
            //MessageBox.Show("id du film "+idmovie);
        }

        bool UpdateMovieCommandCanExecute()
        {
            return true;
        }
    }
}
