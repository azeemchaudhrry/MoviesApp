using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace MovieApp.iOS.Utils
{
    public static class UIColorUtils
    {
        public static UIColor UIColorFromHex(uint rgbValue, float alpha = 1.0f)
        {
            float red = ((rgbValue & 0xFF0000) >> 16) / 256.0f;
            float green = ((rgbValue & 0xFF00) >> 8) / 256.0f;
            float blue = (rgbValue & 0xFF) / 256.0f;

            return new UIColor(red: red, green: green, blue: blue, alpha: alpha);
        }

        public static UIColor GetAppThemeColor()
        {
            return UIColorFromHex(rgbValue: 0xde3900);
        }

        public static UIColor GetAppTextLightColor()
        {
            return UIColorFromHex(rgbValue: 0x888888);
        }

        public static UIColor GetAppTextDarkColor()
        {
            return UIColorFromHex(rgbValue: 0x000000);
        }

        public static UIColor GetBorderColor()
        {
            return UIColorFromHex(rgbValue: 0xf0f0f0);
        }
    }
}