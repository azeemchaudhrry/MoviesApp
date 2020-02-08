using System;
using Foundation;
using MovieApp.Core.Contracts;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MovieApp.iOS.Views.Cells;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MovieApp.iOS.MvxExtensions
{
    public class MvxFavoritesTableSource : MvxSimpleTableViewSource
        , IFavoriteButtonImplementor<Movie>
    {
        FavoritesViewModel ViewModel;
        public MvxFavoritesTableSource(UITableView tableView, Type cellType, FavoritesViewModel _viewModel, string cellIdentifier = null) :
            base(tableView, cellType, cellIdentifier)
        {
            ViewModel = _viewModel;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = (MovieTableCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);
            if (cell == null)
                cell = MovieTableCell.Create();
            if (cell is IMvxDataConsumer dataConsumer)
            {
                dataConsumer.DataContext = item;
            }
            cell.favoriteButtonImplementor = this;
            return cell;
        }

        public void FavoriteButtonTapped(Movie obj)
        {
            ViewModel.RemoveFavoriteItemCommand.Execute(obj);
        }
    }
}