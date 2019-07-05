using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MovieNet.ViewModel
{
    public class MovieCreationViewModel: ViewModelBase
    {
        private String _title;
        private String _kind;
        private String _synopsis;

        MovieDao movieDao = new MovieDao();
        MainWindow currentWindow;
        public RelayCommand CreateMovieCommand { get; }

        public MovieCreationViewModel()
        {
            CreateMovieCommand = new RelayCommand(CreateMovieCommandExecute, CreateMovieCommandCanExecute);
            currentWindow = (MainWindow)Application.Current.MainWindow;
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

       void CreateMovieCommandExecute()
        {
            movieDao.createMovieDao(Title, Kind, Synopsis);

            currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
        }

        bool CreateMovieCommandCanExecute()
        {
            return true;
        }
    }
}
