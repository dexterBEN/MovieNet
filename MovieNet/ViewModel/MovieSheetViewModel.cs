using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using MovieNet.Facade;
using MovieNet.utils;

namespace MovieNet.ViewModel
{
    public class MovieSheetViewModel: ViewModelBase
    {
        private String _title;
        private String _kind;
        private String _synopsis;

        MovieDao movieDao = new MovieDao();
        MainWindow currentWindow;
        ServiceFacade serviceFacade;
        public RelayCommand GetMovieCommand { get; }

        public MovieSheetViewModel()
        {
            GetMovieCommand = new RelayCommand(GetMovieCommandExecute, GetMovieCommandCanExecute);
            serviceFacade = Singleton.GetInstance;
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

       void GetMovieCommandExecute()
        {
            var idmovie = currentWindow.MainFrame.NavigationService.Source.OriginalString.ElementAt(30);
            //MessageBox.Show("id du film "+idmovie);
            var idInt = int.Parse(idmovie.ToString());

            var movieSelected = serviceFacade.getMovie(idInt);

            this.Title = movieSelected.title;
            this.Kind = movieSelected.kind;
            this.Synopsis = movieSelected.synopsis;
        }

        bool GetMovieCommandCanExecute()
        {
            return true;
        }
    }
}
