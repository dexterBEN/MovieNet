using System;
using System.Collections.Generic;
using System.IO;
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
using System.Web;
using System.Collections.Specialized;

namespace MovieNet.ViewModel
{
    public class MovieUpdateViewModel: ViewModelBase
    {
        private String _title;
        private String _kind;
        private String _synopsis;

        MainWindow currentWindow;
        ServiceFacade serviceFacade;
        public RelayCommand UpdateMovieCommand { get; }
        public RelayCommand GetMovieSelectedCommand { get;  }

        public MovieUpdateViewModel()
        {
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
            //NDT for team: i explain these 4 lines in the next meeting
            var baseUri = new Uri("http://www.contoso.com/");
            var currentUri = currentWindow.MainFrame.NavigationService.CurrentSource;
            var finalUri = new Uri(baseUri, currentUri);
            var movieId =  HttpUtility.ParseQueryString(finalUri.Query).Get("movieId");

            /////////////////////////////////////////////////////////////
            /*
             * Other mean to get query params
             * 
             * The method Query on Uri 
             
             var baseUri = new Uri("http://www.contoso.com/");
            var currentUri = currentWindow.MainFrame.NavigationService.CurrentSource;
            var finalUri = new Uri(baseUri, currentUri);

            var test = Application.Current.Properties.Values;
            var resList = (from int element in test select element).ToList().FirstOrDefault();

           
           
            MessageBox.Show("id du film " + resList);*/
            //var data = absUri.Query;

            ///////////////////////////////////////////////////////////////////////////////////////
            
            //Target the current source of the MainFrame, the string looks like "Views/MovieUpdateForm.xaml?key=value"
            var uri = currentWindow.MainFrame.NavigationService.CurrentSource.ToString();

            var idStr = uri.Substring(uri.IndexOf('=') + 1);//Get all element after '=' (not really safe because if you have one more data it can be complicated)

            var idInt = int.Parse(idStr.ToString());//The id i get above is a string, so i cast to int

            var movieSelected = serviceFacade.getMovie(idInt);//I  get the corresponding movie in db then change is props
            movieSelected.title = Title;
            movieSelected.kind = Kind;
            movieSelected.synopsis = Synopsis;

            //Call facade, then the facade call the DAO to update the movie 
            var movieToUpdate = serviceFacade.updateMovie(movieSelected.Id, movieSelected.title, movieSelected.kind, movieSelected.synopsis);

            //The update function in the DAO return 1 if the update is successful, so in that case i just redirect to the list
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
