using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MovieApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace MovieApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "IEG Test", 
        Theme = "@style/AppTheme",
        WindowSoftInputMode = SoftInput.AdjustPan,
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop)]
    public class HomeView : MvxAppCompatActivity<HomeViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Movie App";

            if (savedInstanceState == null)
            {
                ViewModel.ShowInitialViewModelsCommand.Execute();
            }
        }
    }
}