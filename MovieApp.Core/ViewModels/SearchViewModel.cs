using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using MvvmCross.Plugin.Messenger;
using MovieApp.Contracts;
using System;
using MvvmCross.ViewModels;
using MovieApp.Entities;
using MovieApp.Core.Utilities;
using MovieApp.Core.Contracts;
using System.Windows.Input;

namespace MovieApp.Core.ViewModels
{
    public class SearchViewModel : MoviesListingBaseViewModel
        , IInfiniteScrollImplementor
    {
        #region Properties
        public string SearchKey { get; set; }
        public MvxObservableCollection<Movie> ItemsSource { get; set; }

        #endregion End Properties

        #region Commands
        public IMvxCommand<string> SearchCommand => new MvxAsyncCommand<string>(SearchMovies);

        private async Task SearchMovies(string obj)
        {
            if (string.IsNullOrEmpty(obj) && string.IsNullOrWhiteSpace(obj)) return;
            SearchKey = obj;
            CurrentPage = 1;
            if (ItemsSource != null) ItemsSource.Clear();
            await LoadPageData(false);
        }

        public ICommand LoadMoreDataCommand { get; set; }

        private async Task LoadMoreData()
        {
            if (IsRefreshing) return;
            CurrentPage++;
            await LoadPageData(true);
        }
        #endregion End Commands

        #region Constructor
        public SearchViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService,
            IMovieService movieService) : 
            base(logProvider, navigationService, movieService)
        {
            LoadMoreDataCommand = new MvxAsyncCommand(LoadMoreData);
        }

        #endregion End Constructor

        #region Override Methods

        public override Task Initialize()
        {
            return base.Initialize();
        }

        #endregion End Override Methods

        #region Public Methods

        #endregion End Public Methods

        #region Private Methods
        protected override async Task LoadPageData(bool isPaging = false)
        {
            Exception exception = null;
            try
            {
                IsRefreshing = true;
             
                if (isPaging)
                {
                    CurrentPage += 1;
                }

                var request = new MovieRequest() { Page = CurrentPage, Query = SearchKey };

                var response = await MovieService.SearchMovies(request);
                if (response.TotalResults > 0)
                {
                    TotalCount = response.TotalResults;
                    foreach (var item in response.Data)
                    {
                        item.IsFavorite = AppData.Movies.Contains(item);
                    }
                    if (isPaging && CurrentPage > 1)
                    {
                        ItemsSource.AddRange(response.Data);
                    }
                    else
                    {
                        ItemsSource = new MvxObservableCollection<Movie>(response.Data);
                    }
                }
                else
                {
                    NoDataAvailable = true;
                }
            }
            catch (Exception exp)
            {
                exception = exp;
            }
            finally
            {
                if (IsRefreshing) IsRefreshing = false;
            }
        }
        #endregion End Private Methods
    }
}
