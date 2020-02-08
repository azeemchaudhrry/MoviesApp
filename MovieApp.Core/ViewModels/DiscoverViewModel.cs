using MovieApp.Contracts;
using MovieApp.Core.Contracts;
using MovieApp.Core.Models;
using MovieApp.Core.Utilities;
using MovieApp.Entities;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieApp.Core.ViewModels
{
    public class DiscoverViewModel : MoviesListingBaseViewModel
        , IInfiniteScrollImplementor
    {
        #region Properties
        
        public ViewType ViewType { get; set; }
        public int? SelectedGenreId { get; set; }
        public MvxObservableCollection<Movie> ItemsSource { get; set; }
        #endregion

        #region Commands
        public ICommand LoadMoreDataCommand { get; set; }
        
        public IMvxCommand SearchCommand => new MvxAsyncCommand(Search);
        private Task Search()
        {
            return NavigationService.Navigate<SearchViewModel>();
        }

        public IMvxCommand<Genre> FilterSelectionCommand => new MvxAsyncCommand<Genre>(FilterSelection);
        private Task FilterSelection(Genre selectedItem)
        {
            foreach (var item in GenreSource)
            {
                item.IsSelected = false;
                if (item.Equals(selectedItem))
                {
                    item.IsSelected = true;
                }
            }
            RaisePropertyChanged(nameof(GenreSource));
            if(selectedItem.Id == -100)
            {
                SelectedGenreId = null;
            }
            else
            {
                SelectedGenreId = selectedItem.Id;
            }
            return LoadPageData();
        }
        #endregion

        #region Constructors
        public DiscoverViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService,
            IMovieService movieService) :
            base(logProvider, navigationService, movieService)
        {
            LoadMoreDataCommand = new MvxAsyncCommand(LoadMoreData);
        }

        private async Task LoadMoreData()
        {
            if (IsRefreshing) return;
            CurrentPage++;
            IsRefreshing = true;
            await LoadPageData(true);
        }
        #endregion

        public override void ViewAppeared()
        {
            if(GenreSource == null)
            {
                MvxNotifyTask.Create(LoadMovieGenre());
            }
            else
            {
                MvxNotifyTask.Create(LoadPageData());
            }
            base.ViewAppeared();
        }

        private async Task LoadMovieGenre()
        {
            Exception exception = null;
            try
            {
                var response = await MovieService.GetGenres();
                if (response != null)
                {
                    AppData.Genres = response;
                    GenreSource = new ObservableCollection<Genre>(response.Genre);
                    GenreSource.Insert(0, new Genre() { Id = -100, IsSelected = true, Name = "All" });
                }
                await LoadPageData();
            }
            catch (Exception exp)
            {
                exception = exp;
            }
            finally
            {
                LogProvider.GetLogFor(typeof(DiscoverViewModel)).ErrorException("", exception);
            }
        }

        protected override async Task LoadPageData(bool isPaging = false)
        {
            Exception exception = null;
            try
            {
                if(!IsRefreshing)
                {
                    IsLoading = true;
                }

                if (isPaging)
                {
                    CurrentPage += 1;
                }

                var request = new MovieRequest() 
                { 
                    Page = CurrentPage, 
                };

                if (SelectedGenreId.HasValue)
                {
                    request.Genres = $"{SelectedGenreId.Value}";
                }

                var response = await MovieService.DiscoverMovies(request);
                if(response.TotalResults > 0)
                {
                    TotalCount = response.TotalResults;
                    foreach (var item in response.Data)
                    {
                        item.Genres = AppData.Genres.Genre.Where(x => item.GenreIds.Contains(x.Id)).Select(x => x.Name).ToArray();
                        item.IsFavorite = AppData.Movies.Any(x => x.Id == item.Id);
                    }
                    if(isPaging && CurrentPage > 1)
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
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                LogProvider.GetLogFor(typeof(DiscoverViewModel)).ErrorException("", exception);
                if (IsRefreshing) IsRefreshing = false;
                if (IsLoading) IsLoading = false;
            }
        }
    }
}
