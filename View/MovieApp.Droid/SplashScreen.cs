using Android.App;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MovieApp.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity //MvxSplashScreenActivity<MvxAndroidSetup<Core.App>, Core.App>
    {
        public SplashScreen() : base(Resource.Layout.splash_layout)
        {

        }
    }
}