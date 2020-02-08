using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;

namespace MovieApp.iOS.Utils
{
    public static class FontUtils
    {
        public static UIFont GetNormalFont(int size = 17)
        {
            return UIFont.FromName("FuturaPT-Book", size);
        }
        public static UIFont GetBoldFont(int size = 17)
        {
            return UIFont.FromName("FuturaPT-Medium", size);
        }
    }
}