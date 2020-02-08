using System;
using Android.App;
using Android.Runtime;
using MovieApp.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MovieApp.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}