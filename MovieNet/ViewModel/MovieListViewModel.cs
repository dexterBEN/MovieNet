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

        
        public RelayCommand GetMoviesCommand { get; }//Get all movie in database
        public RelayCommand ShowMovieFormCommand { get; }//Command to navigate to movie creation form
        public RelayCommand ShowMovieUpdateFormCommand { get; }//Command to navigate to movie update form
        public RelayCommand ShowMovieSheetCommand { get; }//Command to show the sheet of one movie
        public RelayCommand SearchMovieCommand { get;  }//Command to filter movie by title and kind(launch when you press 'enter' in the keyboard)
        public RelayCommand DeleteMovieCommand { get; }//Command to delete a Movie

        
        public RelayCommand ShowCommentFormCommand { get; }
        public RelayCommand ShowMovieCommentsCommand { get; }

        public MovieListViewModel()
        {
            serviceFacade = Singleton.GetInstance;//Allow to get a unique instance of the facade
            currentWindow = (MainWindow)Application.Current.MainWindow;//Get the current window running
            GetMoviesCommand = new RelayCommand(GetMoviesCommandExecute, GetMoviesCommandCanExecute);
            DeleteMovieCommand = new RelayCommand(DeleteMovieCommandExecute, DeleteMovieCommandCanExecute);
            SearchMovieCommand = new RelayCommand(SearchMovieCommandExecute, SearchMovieCommandCanExecute);
            ShowMovieFormCommand = new RelayCommand(ShowMovieFormCommandExecute, ShowMovieFormCommandCanExecute);
            ShowMovieSheetCommand = new RelayCommand(ShowMovieSheetCommandExecute, ShowMovieSheetCommandCanExecute);
            ShowCommentFormCommand = new RelayCommand(ShowCommentFormCommandExecute, ShowCommentFormCommandCanExecute);
            ShowMovieUpdateFormCommand = new RelayCommand(ShowMovieUpdateFormCommandExecute, ShowMovieUpdateFormCanExecute);

            ShowMovieCommentsCommand = new RelayCommand(ShowMovieCommentsCommandExecute, ShowMovieCommentsCommandCanExecute);
        }

        //Property binded to the input search
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
            Movies = serviceFacade.getMovies();//Get all existing movie in DB

            /*
             * -Target the datagrid with Name="MovieListGrid" (look in Views/MovieListView.xaml)
             * -The attribut "ItemsSource" is there to specify which property will fill the DataGrid
             * -There the property is "Movies" a List i fill up my List with the movie in DB
             * -I used the "Movies" property to fill the MovieListGrid
             */
            ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid.ItemsSource = Movies;
        }

        bool GetMoviesCommandCanExecute()
        {
            //The "GetMoviesCommandExecute" can be launch only if the source of the current frame is (Views/MovieListView.xaml)
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
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;//Target the movie grid
            selectedMovie = (Movie)movieGrid.SelectedItem;//Get the element is selected by the user

            serviceFacade.deleteMovie(selectedMovie.Id);//Delete the movie in DB
            currentWindow.MainFrame.Refresh();//Reload the current frame 
        }

        bool DeleteMovieCommandCanExecute()
        {
            return true;
        }

        void ShowMovieUpdateFormCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;//Target the movie grid
            selectedMovie = (Movie)movieGrid.SelectedItem;//Get the element is selected by the user

            /*currentWindow.MainFrame.Navigate(
                new Uri("Views/MovieUpdateForm.xaml?movieTitle="+movieToUpdate.title+"&movieKind="+movieToUpdate.kind+"&movieSynopsis="+movieToUpdate.synopsis,
                UriKind.RelativeOrAbsolute)
            );*/

            //Application.Current.Properties["movieId"] = selectedMovie.Id;

            /*
             * -There i pass the "selectedMovie.Id" in the uri 
             * -You can see how i get this data in ViewModel/MovieUpdateViewModel.cs
             */
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

            //ICollectionView collectionView = CollectionViewSource.GetDefaultView(movieGrid.ItemsSource);

            /*
             * -Search in the List, get from the DB if there are one or more element which the property title or kind corresponding to InputSearch value
             * -Example here: https://stackoverflow.com/questions/16242885/c-sharp-search-query-with-linq
             */

            var searchRes = serviceFacade.searchMovie(Movies, InputSearch);

            //Fill the datagrid with the result of search
            movieGrid.ItemsSource = searchRes;

            //currentWindow.MainFrame.Refresh();
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

    }
}
