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
    public class UserSettingsViewModel: ViewModelBase
    {
        private string _login;
        private string _password;


        MainWindow currentWindow;
        ServiceFacade serviceFacade;

        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand UpdateUserCommand { get; }
        public RelayCommand GetMovieSelectedCommand { get; }

        public UserSettingsViewModel()
        {
            DeleteUserCommand = new RelayCommand(DeleteUserCommandExecute, DeleteUserCommandCanExecute);
            UpdateUserCommand = new RelayCommand(UpdateUserCommandExecute, UpdateUserCommandCanExecute);
            currentWindow = (MainWindow)Application.Current.MainWindow;
            serviceFacade = Singleton.GetInstance;
        }

        public String Login
        {
            get { return _login; }

            set
            {
                _login = value;
                RaisePropertyChanged();
            }
        }

        public String Password
        {
            get { return _password; }

            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public void DeleteUserCommandExecute()
        {
            var userId = Int32.Parse(Application.Current.Properties["userId"].ToString());
            User user = serviceFacade.getUser(userId);

            if(serviceFacade.deleteUser(user))
            {
                currentWindow.MainFrame.Navigate(new Uri("Views/AuthenticationScreen.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        public bool DeleteUserCommandCanExecute()
        {
            return true;
        }

        public void UpdateUserCommandExecute()
        {
            var userId = Int32.Parse(Application.Current.Properties["userId"].ToString());

            if(serviceFacade.updateUser(userId, Login, Password))
            {
                currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        public bool UpdateUserCommandCanExecute()
        {
            return true;
        }
    }
}
