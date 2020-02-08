using System;
using CoreGraphics;
using MovieApp.Core.Models;
using MovieApp.Core.ViewModels;
using MvvmCross.ViewModels;
using UIKit;

namespace MovieApp.iOS.CollectionViewExtensions
{
    public class MoviesCollectionFlowLayout : UICollectionViewFlowLayout
    {
        nfloat spacing = 16f;
        nfloat numberOfItemsInRow = 2;
        nfloat spacingBetweenCells = 16f;
        IMvxViewModel currentViewModel;
        public MoviesCollectionFlowLayout(IMvxViewModel _viewModel)
        {
            currentViewModel = _viewModel;
        }

        protected internal MoviesCollectionFlowLayout(IntPtr handle) : base(handle)
        {
        }

        public override CGSize ItemSize 
        {
            get 
            {
                if (currentViewModel is DiscoverViewModel viewModel)
                {
                    if (viewModel.ViewType == ViewType.Collection)
                    {
                        var totalSpacing = (2 * spacing) + ((numberOfItemsInRow - 1) + spacingBetweenCells);
                        var width = (CollectionView.Bounds.Width - totalSpacing) / numberOfItemsInRow;
                        var cellHeight = 240;
                        return new CGSize(width, cellHeight);
                    }
                    if (viewModel.ViewType == ViewType.List)
                    {
                        var cellWidth = CollectionView.Frame.Width;
                        var cellHeight = 120;
                        return new CGSize(cellWidth, cellHeight);
                    }
                }
                if(currentViewModel is SearchViewModel)
                {
                    var totalSpacing = (2 * spacing) + ((numberOfItemsInRow - 1) + spacingBetweenCells);
                    var width = (CollectionView.Bounds.Width - totalSpacing) / numberOfItemsInRow;
                    var cellHeight = 240;
                    return new CGSize(width, cellHeight);
                }
                return base.ItemSize; 
            }
            set => base.ItemSize = value; 
        }

        public override CGSize HeaderReferenceSize
        {
            get => new CGSize(this.CollectionView.Frame.Width, 70);
            set => base.HeaderReferenceSize = value;
        }
    }
}