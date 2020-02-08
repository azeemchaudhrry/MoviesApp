using Acr.UserDialogs;
using MovieApp.Core.Utilities;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Core
{
    [AddINotifyPropertyChangedInterface]
    public class MovieDetailsViewModel : MvxNavigationViewModel<Movie>
    {
        #region Properties
        public Movie MovieDetails { get; set; }
        public MovieReview CurrentMovieReview { get; set; }
        public IUserDialogs UserDialogs { get; set; }
        public bool IsReviewAvailable 
        { 
            get 
            { 
                return CurrentMovieReview != null; 
            } 
            set 
            { 
                _ = value; 
            } 
        }
        #endregion

        #region Commands
        public IMvxCommand CloseCommand => new MvxCommand(() => NavigationService.Close(this));

        public IMvxAsyncCommand ReviewCommand => new MvxAsyncCommand(UpsertReview);
        private async Task UpsertReview()
        {
            if (IsReviewAvailable) return;
            var currentReview = AppData.MovieReviews.FirstOrDefault(x => x.MovieId == MovieDetails.Id);
            if(currentReview == null)
            {
                currentReview = new MovieReview();
                currentReview.MovieId = MovieDetails.Id;
            }
            var response = await NavigationService.Navigate<MovieReviewViewModel, MovieReview, MovieReview>(currentReview);
            if(response != null)
            {
                currentReview = response;
                CurrentMovieReview = response;
                await RaisePropertyChanged(nameof(CurrentMovieReview));
            }
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
        #endregion

        #region Constructor

        public MovieDetailsViewModel(
            IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IUserDialogs userDialogs) :
            base(logProvider, navigationService)
        {
            UserDialogs = userDialogs;
        }
        #endregion

        #region Overridden Methods
        public override void Prepare(Movie parameter)
        {
            MovieDetails = parameter;
            var currentReview = AppData.MovieReviews.FirstOrDefault(x => x.MovieId == MovieDetails.Id);
            if(currentReview != null)
            {
                CurrentMovieReview = currentReview;
            }
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }
        #endregion

        #region Public Methods

        #endregion End Public Methods

        #region Private Methods

        #endregion End Private Methods
    }
}
