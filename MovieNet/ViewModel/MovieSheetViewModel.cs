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
        private int _scoreValue;

        MainWindow currentWindow;
        ServiceFacade serviceFacade;
        public RelayCommand GetMovieCommand { get; }
        public RelayCommand GetMovieScoreCommand { get; }

        public MovieSheetViewModel()
        {
            GetMovieCommand = new RelayCommand(GetMovieCommandExecute, GetMovieCommandCanExecute);
            GetMovieScoreCommand = new RelayCommand(GetMovieScoreCommandExecute, GetMovieScoreCommandCanExecute);
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

        public int ScoreValue
        {
            get { return _scoreValue; }

            set
            {
                _scoreValue = value;
                RaisePropertyChanged();
            }
        }

       void GetMovieCommandExecute()
        {
            var uri = currentWindow.MainFrame.NavigationService.CurrentSource.ToString();
            var idStr = uri.Substring(uri.IndexOf('=') + 1);
            var idInt = int.Parse(idStr.ToString());
            var movieSelected = serviceFacade.getMovie(idInt);

            
            this.Title = movieSelected.title;
            this.Kind = movieSelected.kind;
            this.Synopsis = movieSelected.synopsis;
        }

        bool GetMovieCommandCanExecute()
        {
            return true;
        }

        void GetMovieScoreCommandExecute()
        {
            var movieId = (int)Application.Current.Properties["movieId"];
            var movieScore = serviceFacade.getMovieScore(movieId);

            this.ScoreValue = movieScore;
        }

        bool GetMovieScoreCommandCanExecute()
        {
            return true;
        }
    }
}
