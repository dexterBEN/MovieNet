using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using MovieNet.Facade;
using MovieNet.utils;
using System.Windows.Data;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;

namespace MovieNet
{
   public class MovieListViewModel: ViewModelBase
    {
        private String _inputSearch;

        MainWindow currentWindow;
        ServiceFacade serviceFacade;
        Movie selectedMovie;
        public List<Movie> Movies { get; set; }

        
        public RelayCommand GetMoviesCommand { get; }
        public RelayCommand ShowMovieFormCommand { get; }
        public RelayCommand ShowMovieUpdateFormCommand { get; }
        public RelayCommand ShowMovieSheetCommand { get; }
        public RelayCommand SearchMovieCommand { get;  }
        public RelayCommand DeleteMovieCommand { get; }

        public RelayCommand ShowCommentFormCommand { get; }
        public RelayCommand ShowMovieCommentsCommand { get; }

        public RelayCommand ShowMovieRateFormCommand { get; }

        public MovieListViewModel()
        {
            serviceFacade = Singleton.GetInstance;
            currentWindow = (MainWindow)Application.Current.MainWindow;
            GetMoviesCommand = new RelayCommand(GetMoviesCommandExecute, GetMoviesCommandCanExecute);
            DeleteMovieCommand = new RelayCommand(DeleteMovieCommandExecute, DeleteMovieCommandCanExecute);
            SearchMovieCommand = new RelayCommand(SearchMovieCommandExecute, SearchMovieCommandCanExecute);
            ShowMovieFormCommand = new RelayCommand(ShowMovieFormCommandExecute, ShowMovieFormCommandCanExecute);
            ShowMovieSheetCommand = new RelayCommand(ShowMovieSheetCommandExecute, ShowMovieSheetCommandCanExecute);
            ShowCommentFormCommand = new RelayCommand(ShowCommentFormCommandExecute, ShowCommentFormCommandCanExecute);
            ShowMovieUpdateFormCommand = new RelayCommand(ShowMovieUpdateFormCommandExecute, ShowMovieUpdateFormCanExecute);

            ShowMovieCommentsCommand = new RelayCommand(ShowMovieCommentsCommandExecute, ShowMovieCommentsCommandCanExecute);
            ShowMovieRateFormCommand = new RelayCommand(ShowMovieRateFormCommandExecute, ShowMovieRateFormCommandCanExecute);
        }

        public String InputSearch
        {
            get { return _inputSearch; }

            set
            {
                _inputSearch = value;
                RaisePropertyChanged();
            }
        }

        void GetMoviesCommandExecute()
        {
            Movies = serviceFacade.getMovies();
            ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid.ItemsSource = Movies;
        }

        bool GetMoviesCommandCanExecute()
        {
            return currentWindow.MainFrame.Source.Equals("Views/MovieListView.xaml");
        }

        void ShowMovieFormCommandExecute()
        {
            /*Can be used to keep user logged data
             * string str = currentWindow.MainFrame.NavigationService.Source.OriginalString;
             */

            currentWindow.MainFrame.Navigate(new Uri("Views/MovieCreationForm.xaml", UriKind.RelativeOrAbsolute));
            //MessageBox.Show("Voici l'identifiant du user: "+str);
        }

        bool ShowMovieFormCommandCanExecute()
        {
            return true;
        }
        
        void DeleteMovieCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            selectedMovie = (Movie)movieGrid.SelectedItem;

            serviceFacade.deleteMovie(selectedMovie.Id);
            currentWindow.MainFrame.Refresh();
        }

        bool DeleteMovieCommandCanExecute()
        {
            return true;
        }

        void ShowMovieUpdateFormCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            selectedMovie = (Movie)movieGrid.SelectedItem;

            //Application.Current.Properties["movieId"] = selectedMovie.Id;

         
            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieUpdateForm.xaml?movieId="+ selectedMovie.Id,UriKind.RelativeOrAbsolute));
        }

        bool ShowMovieUpdateFormCanExecute()
        {
            return true;
        }

        void ShowMovieSheetCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;//Target the entire datagrid
            selectedMovie = (Movie)movieGrid.SelectedItem;//get the selected row and cast it to a Movie entity

            Application.Current.Properties["movieId"] = selectedMovie.Id;

            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieSheet.xaml?movieId="+ selectedMovie.Id, UriKind.RelativeOrAbsolute));
        }

        bool ShowMovieSheetCommandCanExecute()
        {
            return true;
        }

        void SearchMovieCommandExecute()
        { 
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            Movies = serviceFacade.getMovies();

            var searchRes = serviceFacade.searchMovie(Movies, InputSearch);
            movieGrid.ItemsSource = searchRes;
        }

        bool SearchMovieCommandCanExecute()
        {
            return true;
        }


        void ShowCommentFormCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            selectedMovie = (Movie)movieGrid.SelectedItem;

            var userId = Application.Current.Properties["userId"];
            Application.Current.Properties["movieId"] = selectedMovie.Id;

            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieCommentForm.xaml", UriKind.RelativeOrAbsolute));
        }

        bool ShowCommentFormCommandCanExecute()
        {
            return true;
        }


        void ShowMovieCommentsCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            selectedMovie = (Movie)movieGrid.SelectedItem;

            Application.Current.Properties["movieId"] = selectedMovie.Id;
            Application.Current.Properties["movieTitle"] = selectedMovie.title;

            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieCommentList.xaml", UriKind.RelativeOrAbsolute));
        }

        bool ShowMovieCommentsCommandCanExecute()
        {
            return true;
        }

       void ShowMovieRateFormCommandExecute()
       {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            selectedMovie = (Movie)movieGrid.SelectedItem;

            Application.Current.Properties["movieId"] = selectedMovie.Id;
            var userId = Application.Current.Properties["userId"];

            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieRateForm.xaml", UriKind.RelativeOrAbsolute));
        }

        bool ShowMovieRateFormCommandCanExecute()
        {
            return true;
        }

    }
}
