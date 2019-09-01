using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNet.Facade;
using MovieNet.utils;

namespace MovieNet.ViewModel
{
    public class UserCreationViewModel: ViewModelBase
    {
        private String _login;
        private String _password;

        MainWindow currentWindow;
        ServiceFacade serviceFacade;
        public RelayCommand CreateUserCommand { get; } 

        public UserCreationViewModel()
        {
            serviceFacade = Singleton.GetInstance;
            currentWindow = (MainWindow)Application.Current.MainWindow;
            CreateUserCommand = new RelayCommand(CreateUserCommandExecute, CreateUserCommandCanExecute);
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

        public void CreateUserCommandExecute()
        {
            User userCreated = serviceFacade.createUser(Login, Password);

            if(userCreated != null)
            {
                Application.Current.Properties["userId"] = userCreated.Id;
                MessageBox.Show("Welcome user:", userCreated.login);
                currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        public bool CreateUserCommandCanExecute()
        {
            return true;
        }
    }
}
