using MovieApp.Contracts;
using MovieApp.Core.Utilities;
using MovieApp.Entities;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace MovieApp.Core.ViewModels
{
    public class FavoritesViewModel : BaseNavigationViewModel
    {       
        #region Properties
        
        public MvxObservableCollection<Movie> ItemsSource { get; set; }
        #endregion

        #region Commands
        
        public IMvxCommand<Movie> RemoveFavoriteItemCommand => new MvxAsyncCommand<Movie>(RemoveFavoriteItem);

        private async Task RemoveFavoriteItem(Movie arg)
        {
            if(ItemsSource != null)
            {
                AppData.Movies.Remove(arg);
                ItemsSource.Remove(arg);
                await RaisePropertyChanged(nameof(ItemsSource));
            }
        }

        public IMvxCommand ItemSelectedCommand => new MvxAsyncCommand<object>(ItemSelected);

        private Task ItemSelected(object arg)
        {
            if (arg is Movie movieItem)
            {
                return NavigationService.Navigate<MovieDetailsViewModel, Movie>(movieItem);
            }
            return Task.FromResult(false);
        }
        #endregion

        #region Constructor
        public FavoritesViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService,
            IMovieService movieService) : base(logProvider, navigationService, movieService)
        {
            ItemsSource = new MvxObservableCollection<Movie>();
        }
        #endregion

        #region Overridden Methods

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            ItemsSource = new MvxObservableCollection<Movie>(AppData.Movies);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
