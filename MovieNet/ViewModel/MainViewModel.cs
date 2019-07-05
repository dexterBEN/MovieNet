using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace MovieNet
{
    public class MainViewModel: ViewModelBase
    {
        UserDao userDao = new UserDao();
        MovieDao movieDao = new MovieDao();
        List<Movie> movieList;
        MainWindow currentWindow;

        private string _login;
        public RelayCommand MyCommand { get; }
        public RelayCommand GetMovieCommand { get; }
        public string _password;
        //public MainWindow main;

        public MainViewModel()
        {
            /*currentWindow = ((MainWindow)Application.Current.MainWindow);
            MyCommand = new RelayCommand(MycommandExecute, MyCommandCanExecute);
            GetMovieCommand = new RelayCommand(GetMovieCommandExecute, GetMovieCommandCanExecute);*/

        }

        /*public String Login
        {
            get { return _login; }

            set {
                _login = value;
                RaisePropertyChanged();
            }
        }

        public String Password
        {
            get { return _password; }

            set{
                _password = value;
                RaisePropertyChanged();
            }
        }

        public Uri Source { get; set; }

        void MycommandExecute()
        {
            User user = userDao.getUserDao(Login, Password);

            if(user != null)
            {
                currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        void GetMovieCommandExecute()
        {
            
                movieList = movieDao.getMoviesDao();
                var toto = (currentWindow.MainFrame.Content);

                // ((MovieListView)currentWindow.MainFrame).MovieListGrid.ItemsSource = movieList;
             
                //toto.MovieList.ItemsSource = movieList;
        }

        bool GetMovieCommandCanExecute()
        {
           return  currentWindow.MainFrame.Source.Equals("Views/MovieListView.xaml"); 
        }

        bool MyCommandCanExecute()
        {
            return true;
        }*/
    }
}
