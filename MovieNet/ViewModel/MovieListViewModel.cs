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

namespace MovieNet
{
   public class MovieListViewModel: ViewModelBase
    {
        MovieDao movieDao = new MovieDao();
        MainWindow currentWindow;

        public RelayCommand GetMoviesCommand { get; }
        public RelayCommand ShowMovieFormCommand { get; }
        public RelayCommand ShowMovieUpdateFormCommand { get; }
        public RelayCommand DeleteMovieCommand { get; }

        public List<Movie> Movies { get; set; }

        public MovieListViewModel()
        {
            GetMoviesCommand = new RelayCommand(GetMoviesCommandExecute, GetMoviesCommandCanExecute);
            ShowMovieFormCommand = new RelayCommand(ShowMovieFormCommandExecute, ShowMovieFormCommandCanExecute);
            ShowMovieUpdateFormCommand = new RelayCommand(ShowMovieUpdateFormCommandExecute, ShowMovieUpdateFormCanExecute);
            DeleteMovieCommand = new RelayCommand(DeleteMovieCommandExecute, DeleteMovieCommandCanExecute);
            currentWindow = (MainWindow)Application.Current.MainWindow;
        }

        void GetMoviesCommandExecute()
        {
            Movies = movieDao.getMoviesDao();
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

            movieDao.deleteMovieDao(movieToDelete.Id);
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

            currentWindow.MainFrame.Navigate(new Uri("Views/MovieUpdateForm.xaml?movieId="+movieToUpdate.Id.ToString(),UriKind.RelativeOrAbsolute));


        }

        bool ShowMovieUpdateFormCanExecute()
        {
            return true;
        }

    }
}
