using System;
using MovieApp.Core.ViewModels;
using MovieApp.iOS.MvxExtensions;
using MovieApp.iOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace MovieApp.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Favorites", TabIconName = "favorite", WrapInNavigationController = true)]
    public partial class FavoritesView : MvxViewController<FavoritesViewModel>
    {
        public FavoritesView()
        {

        }

        public FavoritesView(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AutomaticallyAdjustsScrollViewInsets = false;
            tableView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
            
            if (NavigationItem != null)
            {
                NavigationItem.Title = "Favorite";
                NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
                NavigationController.NavigationBar.BackgroundColor = UIColor.White;
                NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                NavigationController.NavigationBar.ShadowImage = new UIImage();
            }

            var tableViewSource = new MvxFavoritesTableSource(
                tableView,
                typeof(MovieTableCell),
                ViewModel,
                MovieTableCell.Key);
            tableView.RowHeight = 120;
            tableView.Source = tableViewSource;

            tableView.RegisterNibForCellReuse(MovieTableCell.Nib, MovieTableCell.Key);

            var bindings = this.CreateBindingSet<FavoritesView, FavoritesViewModel>();
            bindings.Bind(tableViewSource).For(p => p.ItemsSource).To(vm => vm.ItemsSource);
            bindings.Bind(tableViewSource).For(p => p.SelectionChangedCommand).To(vm => vm.ItemSelectedCommand);
            bindings.Apply();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TabBarController.NavigationController.NavigationBarHidden = true;
            View.LayoutSubviews();
        }
    }
}
