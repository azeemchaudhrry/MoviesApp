using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApp.Entities;
using MvvmCross.Plugin.Messenger;
using MovieApp.Contracts;
using MovieApp.Core.Utilities;
using System.Collections.ObjectModel;

namespace MovieApp.Core.ViewModels
{
    public abstract class MoviesListingBaseViewModel : BaseNavigationViewModel
    {
        #region Properties

        public int CurrentPage { get; set; } = 1;
        public int TotalCount { get; set; }
        public ObservableCollection<Genre> GenreSource { get; set; }


        #endregion End Properties

        #region Commands

        public IMvxCommand ItemSelectedCommand => new MvxAsyncCommand<object>(ItemSelected);

        private Task ItemSelected(object arg)
        {
            if (arg is Movie movieItem)
            {
                movieItem.AddGenres(AppData.Genres.Genre);
                return NavigationService.Navigate<MovieDetailsViewModel, Movie>(movieItem);
            }
            return Task.FromResult(false);
        }

        public IMvxCommand AddToFavoritesCommand => new MvxAsyncCommand<object>(AddToFavorites);

        private Task AddToFavorites(object arg)
        {
            if (arg is Movie movieItem)
            {
                if (AppData.Movies.Any(x => x.Equals(movieItem)))
                {
                    AppData.Movies.Remove(movieItem);
                }
                else
                {
                    AppData.Movies.Add(movieItem);
                }
            }
            return Task.FromResult(false);
        }

        public IMvxCommand ReloadDataCommand => new MvxAsyncCommand(ReloadData);
        private async Task ReloadData()
        {
            if (IsRefreshing) return;
            IsRefreshing = true;
            CurrentPage = 0;
            await LoadPageData(true);
        }

        #endregion End Commands

        #region Constructor
        public MoviesListingBaseViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService,
            IMovieService movieService) : 
            base(logProvider, navigationService, movieService)
        {
        }

        #endregion End Constructor

        #region Override Methods

        public override Task Initialize()
        {
            return base.Initialize();
        }

        #endregion End Override Methods

        #region ProtectedMethods
        protected abstract Task LoadPageData(bool isPaging);

        #endregion

        #region Public Methods

        #endregion End Public Methods

        #region Private Methods

        #endregion End Private Methods
    }
}
