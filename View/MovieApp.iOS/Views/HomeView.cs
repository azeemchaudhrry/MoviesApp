using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MovieApp.Core;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace MovieApp.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class HomeView : MvxTabBarViewController<HomeViewModel>
    {
        private bool isPresentedFirstTime = true;
        public HomeView()
        {

        }

        public HomeView(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
            //NavigationController.NavigationBar.PrefersLargeTitles = true;
            //NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;
            NavigationController.NavigationBarHidden = true;

            AutomaticallyAdjustsScrollViewInsets = false;

            if(ViewModel != null && isPresentedFirstTime)
            {
                isPresentedFirstTime = false;
                ViewModel.ShowInitialViewModelsCommand.Execute();
            }

            //TabBarController = UIColor.Yellow;
        }

        public override bool ShowChildView(UIViewController viewController)
        {
            return false;
        }
    }
}