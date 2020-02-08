using System;
using System.Linq;
using Foundation;
using MovieApp.Core.Contracts;
using MovieApp.Core.Models;
using MovieApp.Core.Utilities;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MovieApp.iOS.Utils;
using MovieApp.iOS.Views.Cells;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace MovieApp.iOS.MvxExtensions
{
    public class MovieListingCollectionViewSource : MvxCollectionViewSource
        , IFavoriteButtonImplementor<Movie>
        , IItemSelectionChange<Genre>
    {
        IMvxViewModel currentViewModel;
        public MovieListingCollectionViewSource(UICollectionView collectionView) : base(collectionView)
        {
        }

        public MovieListingCollectionViewSource(UICollectionView collectionView, NSString defaultCellIdentifier, IMvxViewModel _currentViewModel) : base(collectionView, defaultCellIdentifier)
        {
            currentViewModel = _currentViewModel;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return base.GetItemsCount(collectionView, section);
        }

        public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            var hw = (HeaderCollectionView)collectionView.DequeueReusableSupplementaryView(elementKind, HeaderCollectionView.Key, indexPath);
            if (hw is IMvxDataConsumer dataConsumer && AppData.Genres != null)
            {
                var genres = AppData.Genres;
                if(!genres.Genre.Any(x => x.Id == -100))
                {
                    genres.Genre.Insert(0, new Genre() { Id = -100, IsSelected = !genres.Genre.Any(x => x.IsSelected), Name = "All Genre" });
                }
                dataConsumer.DataContext = genres;
                hw.GenreSelection = this;
            }
            return hw;
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            if (currentViewModel is DiscoverViewModel discoverViewModel)
            {
                if (discoverViewModel.ViewType == ViewType.List)
                {
                    var listCell = (MovieListCell)collectionView.DequeueReusableCell(MovieListCell.Key, indexPath);
                    listCell.favoriteButtonImplementor = this;
                    if (((Movie)item).IsFavorite)
                    {
                        listCell.favoriteImageView.Image = UIImage.FromBundle("favorite-filled").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                        listCell.favoriteImageView.TintColor = UIColor.Red;
                    }
                    else
                    {
                        listCell.favoriteImageView.Image = UIImage.FromBundle("favorite-gray").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                        listCell.favoriteImageView.TintColor = UIColorUtils.GetAppTextLightColor();
                    }
                    return listCell;
                }
            }

            var cell = (MovieCollectionCell)base.GetOrCreateCellFor(collectionView, indexPath, item);
            cell.favoriteButtonImplementor = this;
            var movie = (Movie)item;
            if (movie.IsFavorite)
            {
                cell.favoriteImageView.Image = UIImage.FromBundle("favorite-filled").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                cell.favoriteImageView.TintColor = UIColor.Red;
            }
            else
            {
                cell.favoriteImageView.Image = UIImage.FromBundle("favorite-gray").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                cell.favoriteImageView.TintColor = UIColorUtils.GetAppTextLightColor();
            }
            return cell;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            var offsetY = scrollView.ContentOffset.Y;
            var contentHeight = scrollView.ContentSize.Height;
            if (offsetY > contentHeight - scrollView.Frame.Size.Height)
            {
                if (currentViewModel is IInfiniteScrollImplementor infiniteScrollImplementor)
                {
                    infiniteScrollImplementor.LoadMoreDataCommand.Execute(true);
                }
            }
        }

        public void FavoriteButtonTapped(Movie obj)
        {
            if (currentViewModel is MoviesListingBaseViewModel baseViewModel)
            {
                baseViewModel.AddToFavoritesCommand.Execute(obj);
            }
        }

        public void ItemSelectionChanged(Genre obj)
        {
            if(currentViewModel is DiscoverViewModel viewmodel)
            {
                viewmodel.FilterSelectionCommand.Execute(obj);
            }
        }
    }
}