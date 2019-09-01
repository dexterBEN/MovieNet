using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNet.utils;

namespace MovieNet
{
    public class AuthenticationViewModel : ViewModelBase
    {
     
        MainWindow currentWindow;
        public RelayCommand<object> LoginCommand { get; }

        public RelayCommand ShowUserFormCommand { get; }

        private string _login;
        public String Login
        {
            get { return _login; }

            set
            {
                _login = value;
                RaisePropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public AuthenticationViewModel()
        {
           
            currentWindow = ((MainWindow)Application.Current.MainWindow);
            LoginCommand = new RelayCommand<object>(LoginCommandExecute, LoginCommandCanExecute);
            ShowUserFormCommand = new RelayCommand(ShowUserFormCommandExecute, ShowUserFormCommandCanExecute);
        }


        void LoginCommandExecute(object pwdBox)
        {
            PasswordBox passwordBox = pwdBox as PasswordBox;

            User user = Singleton.GetInstance.getUser(Login, passwordBox.Password);

            if (user != null)
            {
               
                Application.Current.Properties["userId"] = user.Id;
                MessageBox.Show("Welcolm to MovieNet\n" +
                                "In this plateform you can do the following action:\n"+
                                "-create a movie by click on 'add movie' \n"+
                                "-delete the movie you create by click on the movie you want to delete then 'delete movie' \n"+
                                "-update your movie by click on the movie then 'update movie'\n"+
                                "-search a movie according to his title or kind, write your search "
                                );
                currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        bool LoginCommandCanExecute(object arg)
        {
            PasswordBox passwordBox = arg as PasswordBox;
            return !(String.IsNullOrEmpty(Login));
        }


        void ShowUserFormCommandExecute()
        {
            MessageBox.Show("This is art");
            currentWindow.MainFrame.Navigate(new Uri("Views/UserCreationForm.xaml", UriKind.RelativeOrAbsolute));
        }

        bool ShowUserFormCommandCanExecute()
        {
            return true;
        }

    }
}
                
                