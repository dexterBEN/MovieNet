using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace MovieNet
{
    public class ViewModelLocator
    {

        public MainViewModel MainVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AuthenticationViewModel AuthenticationVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AuthenticationViewModel>();
            }
        }

        public MovieListViewModel MovieListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MovieListViewModel>();
            }
        }

        public ViewModel.MovieCreationViewModel MovieCreationVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MovieCreationViewModel>();
            }
        }

        public ViewModel.MovieUpdateViewModel MovieUpdateVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MovieUpdateViewModel>();
            }
        }

        public ViewModel.MovieSheetViewModel MovieSheetVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MovieSheetViewModel>();
            }
        }

        public ViewModel.MovieCommentFormViewModel MovieCommentFormVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MovieCommentFormViewModel>();
            }
        }

        public ViewModel.MovieCommentsListViewModel CommentsListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MovieCommentsListViewModel>();
            }
        }

        public ViewModel.MovieRateFormViewModel MovieRateFormVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MovieRateFormViewModel>();
            }
        }

        public NavigationService NavigationService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NavigationService>();
            }
        }


        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
          
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AuthenticationViewModel>();
            SimpleIoc.Default.Register<MovieListViewModel>();

            SimpleIoc.Default.Register<ViewModel.MovieCreationViewModel>();
            SimpleIoc.Default.Register<ViewModel.MovieUpdateViewModel>();
            SimpleIoc.Default.Register<ViewModel.MovieSheetViewModel>();
            SimpleIoc.Default.Register<ViewModel.MovieCommentFormViewModel>();
            SimpleIoc.Default.Register<ViewModel.MovieCommentsListViewModel>();
            SimpleIoc.Default.Register<ViewModel.MovieRateFormViewModel>();

            //NavigationService navigation = NavigationService.GetNavigationService();
            //navigation.Configure(Locator.HomePage, typeof(HomePage));
            //navigation.Configure(Locator.SecondPage, typeof(SecondPage));
            //SimpleIoc.Default.Register(() => navigation);
        }


    }
}
