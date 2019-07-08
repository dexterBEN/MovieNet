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
            LoginCommand = new RelayCommand<object>(LoginCommandExecute, LoginCommandCanExecute);
            currentWindow = ((MainWindow)Application.Current.MainWindow);
        }

        void LoginCommandExecute(object pwdBox)
        {
            PasswordBox passwordBox = pwdBox as PasswordBox;
            User user = Singleton.GetInstance.getUser(Login, passwordBox.Password);

            if (user != null)
            {
                /* Can be used to pass parameter like user data to id:
                 * currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml?userID="+user.Id, UriKind.RelativeOrAbsolute));
                 */
                currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        bool LoginCommandCanExecute(object arg)
        {
            PasswordBox passwordBox = arg as PasswordBox;
            return !(String.IsNullOrEmpty(Login));
        }

    }
}
                