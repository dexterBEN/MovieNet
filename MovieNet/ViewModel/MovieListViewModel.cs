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

        MovieDao movieDao = new MovieDao();
        MainWindow currentWindow;
        ServiceFacade serviceFacade;

        public RelayCommand GetMoviesCommand { get; }
        public RelayCommand ShowMovieFormCommand { get; }
        public RelayCommand ShowMovieUpdateFormCommand { get; }
        public RelayCommand ShowMovieSheetCommand { get; }
        public RelayCommand DeleteMovieCommand { get; }
        public RelayCommand SearchMovieCommand { get;  }
        public List<Movie> Movies { get; set; }

        public MovieListViewModel()
        {
            GetMoviesCommand = new RelayCommand(GetMoviesCommandExecute, GetMoviesCommandCanExecute);
            ShowMovieFormCommand = new RelayCommand(ShowMovieFormCommandExecute, ShowMovieFormCommandCanExecute);
            ShowMovieUpdateFormCommand = new RelayCommand(ShowMovieUpdateFormCommandExecute, ShowMovieUpdateFormCanExecute);
            ShowMovieSheetCommand = new RelayCommand(ShowMovieSheetCommandExecute, ShowMovieSheetCommandCanExecute);
            DeleteMovieCommand = new RelayCommand(DeleteMovieCommandExecute, DeleteMovieCommandCanExecute);
            SearchMovieCommand = new RelayCommand(SearchMovieCommandExecute, SearchMovieCommandCanExecute);
            currentWindow = (MainWindow)Application.Current.MainWindow;
            serviceFacade = Singleton.GetInstance;
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
            Movie movieToDelete = (Movie)movieGrid.SelectedItem;

            serviceFacade.deleteMovie(movieToDelete.Id);
            currentWindow.MainFrame.Refresh();
        }

        bool DeleteMovieCommandCanExecute()
        {
            return true;
        }

        void ShowMovieUpdateFormCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            Movie movieToUpdate = (Movie)movieGrid.SelectedItem;

            /*currentWindow.MainFrame.Navigate(
                new Uri("Views/MovieUpdateForm.xaml?movieTitle="+movieToUpdate.title+"&movieKind="+movieToUpdate.kind+"&movieSynopsis="+movieToUpdate.synopsis,
                UriKind.RelativeOrAbsolute)
            );*/

            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieUpdateForm.xaml?movieId="+movieToUpdate.Id,UriKind.RelativeOrAbsolute));
        }

        bool ShowMovieUpdateFormCanExecute()
        {
            return true;
        }

        void ShowMovieSheetCommandExecute()
        {
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;
            Movie movieToShow = (Movie)movieGrid.SelectedItem;

            currentWindow.MainFrame.NavigationService.Navigate(new Uri("Views/MovieSheet.xaml?movieId="+movieToShow.Id, UriKind.RelativeOrAbsolute));
        }

        bool ShowMovieSheetCommandCanExecute()
        {
            return true;
        }

        void SearchMovieCommandExecute()
        {
            //MessageBox.Show("Voici ce que user à taper: "+InputSearch);
            var movieGrid = ((MovieListView)currentWindow.MainFrame.Content).MovieListGrid;

            Movies = serviceFacade.getMovies();

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(movieGrid.ItemsSource);

            var searchRes = Movies.Where(movie => movie.title.Contains(InputSearch) || movie.kind.Contains(InputSearch)).ToList();

            movieGrid.ItemsSource = searchRes;
            //currentWindow.MainFrame.Refresh();
        }

        bool SearchMovieCommandCanExecute()
        {
            return true;
        }

    }
}
