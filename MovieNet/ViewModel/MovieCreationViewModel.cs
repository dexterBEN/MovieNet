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
    public class MovieCreationViewModel: ViewModelBase
    {
        private String _title;
        private String _kind;
        private String _synopsis;

        ServiceFacade serviceFacade;
        MainWindow currentWindow;
        public RelayCommand CreateMovieCommand { get; }

        public MovieCreationViewModel()
        {
            serviceFacade = Singleton.GetInstance;
            currentWindow = (MainWindow)Application.Current.MainWindow;
            CreateMovieCommand = new RelayCommand(CreateMovieCommandExecute, CreateMovieCommandCanExecute);
        }

        //Properties binded with the view, look in Views/MovieCreationForm.xaml
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
            serviceFacade.createMovie(Title, Kind, Synopsis);//Call the function of service facade to insert the Movie entity in the DB

            //Then load the movieList view (each time this view is loaded, this get all exist movie and display it)
            currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
        }

        bool CreateMovieCommandCanExecute()
        {
            return true;
        }
    }
}
