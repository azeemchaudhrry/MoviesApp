using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Plugin.CurrentActivity;

namespace MovieApp.Droid
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, Core.App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {   
            base.OnCreate();

            CrossCurrentActivity.Current.Init(this);
            UserDialogs.Init(this);
        }
    }
}