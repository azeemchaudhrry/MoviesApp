using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MovieApp.Droid.Extensions
{
    public static class Utils
    {
        public static int PxToDp(int px)
        {
            return (int)(px / Resources.System.DisplayMetrics.Density);
        }

        public static float PxToDp(float px)
        {
            return px / Resources.System.DisplayMetrics.Density;
        }

        public static int DpToPx(int dp)
        {
            return (int)(dp * Resources.System.DisplayMetrics.Density);
        }

        public static float DpToPx(float dp)
        {
            return dp * Resources.System.DisplayMetrics.Density;
        }
    }
}