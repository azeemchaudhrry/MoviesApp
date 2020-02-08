using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using MovieApp.Core.Models;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MovieApp.iOS.CollectionViewExtensions;
using MovieApp.iOS.MvxExtensions;
using MovieApp.iOS.Utils;
using MovieApp.iOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace MovieApp.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Discover", TabIconName = "discover", WrapInNavigationController = true)]
    public partial class DiscoverView : MvxViewController<DiscoverViewModel>
    {
        static NSTimer timer;
        MvxUIRefreshControl mvxUIRefreshControl;
        MovieListingCollectionViewSource collectionViewSource;
        UISearchController searchController;

        public DiscoverView()
        {

        }

        public DiscoverView(IntPtr handle) : base(handle)
        {
            mvxUIRefreshControl = new MvxUIRefreshControl();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupCollectionView();

            var set = this.CreateBindingSet<DiscoverView, DiscoverViewModel>();
            set.Bind(collectionViewSource).For(p => p.ItemsSource).To(vm => vm.ItemsSource);
            set.Bind(collectionViewSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            set.Bind(mvxUIRefreshControl).For(r => r.IsRefreshing).To(vm => vm.IsRefreshing);
            set.Bind(mvxUIRefreshControl).For(r => r.RefreshCommand).To(vm => vm.ReloadDataCommand);
            set.Apply();

            SetupNavigationBar();

            SetupSearchBarController(); 

            AddRightBarButtonItem();
        }

        private void SetupCollectionView()
        {
            collectionView.RegisterNibForCell(MovieCollectionCell.Nib, MovieCollectionCell.Key);
            collectionView.RegisterNibForCell(MovieListCell.Nib, MovieListCell.Key);
            collectionView.RefreshControl = mvxUIRefreshControl;

            collectionViewSource = new MovieListingCollectionViewSource(collectionView, MovieCollectionCell.Key, ViewModel);
            var layout = new MoviesCollectionFlowLayout(ViewModel);
            layout.MinimumInteritemSpacing = 16;
            layout.MinimumLineSpacing = 16;
            layout.SectionInset = new UIEdgeInsets(16, 16, 0, 16);
            layout.HeaderReferenceSize = new CGSize(View.Frame.Width, 70);
            layout.SectionHeadersPinToVisibleBounds = true;
            collectionView.SetCollectionViewLayout(layout, true);
            collectionView.Source = collectionViewSource;

            collectionView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
        }

        private void SetupSearchBarController()
        {
            //UIViewController viewController = null;
            var vmRequest = MvxViewModelRequest.GetDefaultRequest(typeof(SearchViewModel));
            var searchView = MvxCanCreateIosViewExtensions.CreateViewControllerFor<SearchViewModel>(this, vmRequest) as SearchView;

            searchController = new UISearchController(searchView)
            {
                DimsBackgroundDuringPresentation = false,
                HidesNavigationBarDuringPresentation = true,
                SearchBar = { Placeholder = "Search Movies"},
            };

            NavigationItem.SearchController = searchController;
            NavigationItem.HidesSearchBarWhenScrolling = true;

            searchController.SearchResultsUpdater = searchView;
            //searchController.Delegate = new DiscoverListingSearchDelegate(ViewModel, this);

            //searchController.SearchBar.CancelButtonClicked += (sender, e) =>
            //{
            //    ViewModel.SearchCommand.Execute(string.Empty);
            //};

            var textfield = (UITextField)searchController.SearchBar.ValueForKey(new NSString("searchField"));
            if (textfield != null)
            {
                textfield.Font = FontUtils.GetNormalFont();
            }
        }

        private void SetupNavigationBar()
        {
            NavigationController.NavigationBar.Translucent = true;
            NavigationController.NavigationBar.PrefersLargeTitles = true;

            NavigationItem.Title = "Discover";
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
            NavigationController.NavigationBar.BackgroundColor = UIColor.White;
            NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationController.NavigationBar.ShadowImage = new UIImage();

            AutomaticallyAdjustsScrollViewInsets = false;
            ExtendedLayoutIncludesOpaqueBars = true;
            DefinesPresentationContext = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TabBarController.NavigationController.NavigationBarHidden = true;
            View.SetNeedsLayout();
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

        private void AddRightBarButtonItem()
        {
            var viewTypeButton = new UIBarButtonItem(title: "List", UIBarButtonItemStyle.Done, viewTypeButtonPressed);
            viewTypeButton.Tag = 0;
            NavigationItem.SetRightBarButtonItem(viewTypeButton, true);
            var iconButton = new UIBarButtonItem(title: "IEG Test", UIBarButtonItemStyle.Bordered, null);
            iconButton.TintColor = UIColor.Black;
            iconButton.SetTitleTextAttributes( new UITextAttributes() { Font = FontUtils.GetNormalFont() } , UIControlState.Normal);
            NavigationItem.SetLeftBarButtonItem(iconButton, true);
        }

        private void viewTypeButtonPressed(object sender, EventArgs e)
        {
            if (sender is UIBarButtonItem button)
            {
                if (button.Tag == 0)
                {
                    button.Tag = 1;
                    button.Title = "Grid";
                    ViewModel.ViewType = ViewType.List;
                }
                else if (button.Tag == 1)
                {
                    button.Tag = 0;
                    button.Title = "List";
                    ViewModel.ViewType = ViewType.Collection;
                }
                collectionView.ReloadData();
            }
        }
        //public void Reload()
        //{
        //    collectionView.ReloadData();
        //}
    }

    
    //public class GenreCollectionSource : MvxCollectionViewSource
    //{
    //    public GenreCollectionSource(UICollectionView collectionView) : base(collectionView)
    //    {
    //    }

    //    public GenreCollectionSource(UICollectionView collectionView, NSString defaultCellIdentifier) : base(collectionView, defaultCellIdentifier)
    //    {
    //    }

    //    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    //    {
    //        //var label = new UILabel();
    //        //label.BackgroundColor = "";
    //        //label.TextColor = "";
    //        //return label;
    //        return base.GetCell(collectionView, indexPath);
    //    }
    //}

    //public class GenreCollectionViewCell : UICollectionViewCell
    //{
    //    public string Label { get; set; }
    //    public GenreCollectionViewCell(IntPtr handle) : base(handle)
    //    {
    //    }


    //}

    //public class DiscoverListingSearchDelegate : UISearchControllerDelegate
    //{
    //    DiscoverViewModel discoverViewModel;
    //    DiscoverView View;

    //    public DiscoverListingSearchDelegate(DiscoverViewModel _discoverViewModel, DiscoverView _view)
    //    {
    //        discoverViewModel = _discoverViewModel;
    //        View = _view;
    //    }

    //    public override void DidPresentSearchController(UISearchController searchController)
    //    {
    //        View.Reload();
    //    }

    //    public override void DidDismissSearchController(UISearchController searchController)
    //    {
    //        View.Reload();
    //    }
    //}
}