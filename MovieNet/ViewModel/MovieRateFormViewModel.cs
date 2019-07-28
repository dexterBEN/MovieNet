using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNet.Facade;
using MovieNet.utils;
using System.Windows;

namespace MovieNet.ViewModel
{
    public class MovieRateFormViewModel: ViewModelBase
    {
        private int _rateValue;

        ServiceFacade serviceFacade;
        public RelayCommand AddRateToMovieCommand { get; }

        public MovieRateFormViewModel()
        {
            serviceFacade = Singleton.GetInstance;
            AddRateToMovieCommand = new RelayCommand(AddRateToMovieCommandExecute, AddRateToMovieCommandCanExecute);
        }

        public int RateValue
        {
            get { return _rateValue; }
            set
            {
                _rateValue = value;
                RaisePropertyChanged();
            }
        }

        public void AddRateToMovieCommandExecute()
        {
            var userId = (int)Application.Current.Properties["userId"];
            var movieId = (int)Application.Current.Properties["movieId"];

            serviceFacade.addRateToMovie(userId, movieId, RateValue);
        }

        bool AddRateToMovieCommandCanExecute()
        {
            return true;
        }
    }
}
