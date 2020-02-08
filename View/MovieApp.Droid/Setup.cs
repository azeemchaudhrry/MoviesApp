using System.Collections.Generic;
using System.Reflection;
using MovieApp.Droid.MvxExtensions;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MovieApp.Droid
{
    public class Setup : MvxAppCompatSetup<Core.App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => base.AndroidViewAssemblies;

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxCustomPresenter(AndroidViewAssemblies);
        }
    }
}