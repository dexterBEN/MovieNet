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
        /*
        * -When you create a WPF page(a .xaml file), you have an associated class(the code behind)
        * -The class MainWindow here is the class related to MainWindow.xaml
        * -I create a single window and i just change is Frame(the frame is the content of the window)
        */
        MainWindow currentWindow;

        /*
        * -RelayCommand is just an action you want to launch on specific event.
        * -The LoginCommand is bind with the button "btnSubmit" in  the Views/AuthenticationScreen.xaml.
        * -The "object" is there to say you can pass parameter to the command 
        */
        public RelayCommand<object> LoginCommand { get; }

        /*
        *   -Define the propertie binded with the view.
        *   -if you look in AuthenticationScreen.xaml you can see the property in the TextBox "userLogin"  
        */
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
            /*
            * -When you create an instance of the command you have to define two functions.
            * -The first function is what you  want to accomplish when the command is triggered
            * -The second function return a boolean to define if the first function can be launch
            * 
            * NDT:
            * You write can also like this: LoginCommand = new RelayCommand<object>(LoginCommandExecute, true);
            */
            LoginCommand = new RelayCommand<object>(LoginCommandExecute, LoginCommandCanExecute);

            /*
            * There i get the current window when application running   
            */
            currentWindow = ((MainWindow)Application.Current.MainWindow);
        }


        void LoginCommandExecute(object pwdBox)
        {
            PasswordBox passwordBox = pwdBox as PasswordBox;

            //The Singleton class create a single instance of the ServiceFacade and each function of the facade call the DAO
            User user = Singleton.GetInstance.getUser(Login, passwordBox.Password);

            if (user != null)
            {
                /* Can be used to pass parameter like user data to id:
                 * currentWindow.MainFrame.Navigate(new Uri("Views/MovieListView.xaml?userID="+user.Id, UriKind.RelativeOrAbsolute));
                 */

                /*I target the whole application and define a property named "userdId" and set it.
                * More  information there: https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.properties?view=netframework-4.8
                */
                Application.Current.Properties["userId"] = user.Id;

                /*
                * -This the way i manage the navigation
                * -I target the current instance of the main window then target the main frame  and change the frame location
                * -I look for two way to pass data between view and i found:
                *   1)Passing data as query: "View/file.xaml?key=value"
                *   2)The navigate propose to pass a second parameter as a data for the navigation(example: https://docs.microsoft.com/fr-fr/dotnet/api/system.windows.navigation.navigationwindow.navigate?view=netframework-4.8)
                *   
                *  NDT: I tried to implement these solutions but encounter too many issue, so to pass data i define propertie like this -> Application.Current["props"]
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
                
                