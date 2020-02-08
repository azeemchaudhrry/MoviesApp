using MovieApp.Core.ViewModels;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MovieApp.Core
{
    public class HomeViewModel : MvxNavigationViewModel
    {
        public IMvxCommand ShowInitialViewModelsCommand => new MvxAsyncCommand(ShowInitialViewModels);
        public IMvxAsyncCommand<Type> NavigateCommand => new MvxAsyncCommand<Type>(ShowSecletedTabViewModel);
        public HomeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : 
            base(logProvider, navigationService)
        {
        }

        private async Task ShowInitialViewModels()
        {
            try
            {
                var tasks = new List<Task>();
                tasks.Add(NavigationService.Navigate<DiscoverViewModel>());
                tasks.Add(NavigationService.Navigate<FavoritesViewModel>());
                await Task.WhenAll(tasks);
            }
            catch (System.Exception exp)
            {
                LogProvider.GetLogFor<HomeViewModel>().Debug(exp, "", "");
            }
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public override void ViewDisappeared()
        {
            base.ViewDisappeared();

            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if(e.NetworkAccess != NetworkAccess.Internet)
            {
                var readableString = GetReadableString(e.NetworkAccess);
                await NavigationService.Navigate<NoInternetViewModel, string>(readableString);
            }
            else if (e.NetworkAccess == NetworkAccess.Internet)
            {
                await ShowSecletedTabViewModel(typeof(DiscoverViewModel));
            }
        }

        private string GetReadableString(NetworkAccess networkAccess)
        {
            switch (networkAccess)
            {
                case NetworkAccess.Unknown:
                    return "Unknown internet state.";
                case NetworkAccess.None:
                    return "No internet available.";
                case NetworkAccess.Local:
                    return "Local network access only.";
                case NetworkAccess.ConstrainedInternet:
                    return "Limited internet access.";
                default:
                    return "No internet available.";
            }
        }

        private async Task ShowSecletedTabViewModel(Type viewModelType)
        {
            if (viewModelType == typeof(DiscoverViewModel))
            {
                await NavigationService.ChangePresentation(new MvxPagePresentationHint(typeof(DiscoverViewModel)));
                return;
            }
            if (viewModelType == typeof(FavoritesViewModel))
            {
                await NavigationService.ChangePresentation(new MvxPagePresentationHint(typeof(FavoritesViewModel)));
                return;
            }
        }
    }
}