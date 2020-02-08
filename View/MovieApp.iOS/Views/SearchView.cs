using CoreGraphics;
using Foundation;
using MovieApp.Core.ViewModels;
using MovieApp.iOS.CollectionViewExtensions;
using MovieApp.iOS.MvxExtensions;
using MovieApp.iOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace MovieApp.iOS
{
    [MvxFromStoryboard("Main")]
    public partial class SearchView : MvxViewController<SearchViewModel>
        , IUISearchResultsUpdating
    {
        static NSTimer timer;
        MovieListingCollectionViewSource collectionViewSource;
        public SearchView()
        {

        }
        public SearchView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupCollectionView();

            var set = this.CreateBindingSet<SearchView, SearchViewModel>();
            set.Bind(collectionViewSource).For(p => p.ItemsSource).To(vm => vm.ItemsSource);
            set.Bind(collectionViewSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Apply();
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            timer?.Invalidate();
            timer = NSTimer.CreateScheduledTimer(0.7, false, nsTimer =>
            {
                var query = searchController?.SearchBar.Text;
                ViewModel.SearchCommand.Execute(query);
            });
        }

        private void SetupCollectionView()
        {
            collectionView.RegisterNibForCell(MovieCollectionCell.Nib, MovieCollectionCell.Key);
            collectionViewSource = new MovieListingCollectionViewSource(collectionView, MovieCollectionCell.Key, ViewModel);
            var layout = new MoviesCollectionFlowLayout(ViewModel);
            layout.MinimumInteritemSpacing = 16;
            layout.MinimumLineSpacing = 16;
            layout.SectionInset = new UIEdgeInsets(16, 16, 0, 16);
            collectionView.SetCollectionViewLayout(layout, true);
            collectionView.Source = collectionViewSource;

            collectionView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
        }
    }
}