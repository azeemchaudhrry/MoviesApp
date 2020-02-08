using Acr.UserDialogs;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MovieApp.Core;
using MovieApp.Droid.Extensions;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MovieApp.Droid.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class HomeView : MvxAppCompatActivity<HomeViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_layout);

            //UserDialogs.Init(this);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            toolbar.Title = "Movie App";

            for (int i = 0; i < toolbar.ChildCount; i++)
            {
                View child = toolbar.GetChildAt(i);
                if (child is TextView toolbarTitle)
                {
                    toolbarTitle.SetTypeface(FontUtils.GetBoldFont(this), Android.Graphics.TypefaceStyle.Normal);
                    break;
                }
            }

            if (savedInstanceState == null)
            {
                ViewModel.ShowInitialViewModelsCommand.Execute();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}