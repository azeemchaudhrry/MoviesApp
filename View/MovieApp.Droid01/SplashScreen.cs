using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;

namespace MovieApp.Droid
{
    [Activity(Label = "IEG Test"
        , Theme = "@style/AppTheme"
        , MainLauncher = true
        , NoHistory = true)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, Core.App>
    {
        public SplashScreen() : base(Resource.Layout.splash_layout)
        {

        }
    }
}