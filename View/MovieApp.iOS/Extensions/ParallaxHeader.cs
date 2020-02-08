using System;
using FFImageLoading.Cross;
using Foundation;
using UIKit;

namespace MovieApp.iOS.Extensions
{
    public class ParallaxHeader : UIScrollView//, IUIScrollViewDelegate
    {
        public MvxCachedImageView ImageView;

        public ParallaxHeader(string imageUrl, CoreGraphics.CGRect frame)
            : base(frame)
        {
            this.AutosizesSubviews = true;
            this.ImageView = new MvxCachedImageView(new CoreGraphics.CGRect(0, 0, frame.Width, frame.Height));
            this.ImageView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
            this.ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            this.ImageView.ImagePath = imageUrl;
            this.ImageView.ClipsToBounds = true;
            UIEdgeInsets newInset = this.ContentInset;
            newInset.Top = -UIApplication.SharedApplication.StatusBarFrame.Height;
            this.ContentInset = newInset;
            this.AddSubview(this.ImageView);
        }

        [Export("scrollViewDidScroll:")]
        public new void Scrolled(UIScrollView scrollView)
        {
            nfloat scrollOffset = scrollView.ContentOffset.Y;
            CoreGraphics.CGRect headerImageFrame = ImageView.Frame;

            if (scrollOffset < 0)
            {
                headerImageFrame.Height = 300 - scrollOffset;
                headerImageFrame.Y = 0;
            }
            else
            {
                headerImageFrame.Height = 300;
                headerImageFrame.Y = 0 - ((scrollOffset / 2));
            }

            ImageView.Frame = headerImageFrame;
        }

        //[Export("scrollViewDidScroll:")]
        //public void Scrolled(UIScrollView scrollView)
        //{
        //    float scrollOffset = (float)scrollView.ContentOffset.Y;
        //    CoreGraphics.CGRect headerImageFrame = parallax.Frame;

        //    if (scrollOffset < 0)
        //    {
        //        headerImageFrame.Height = originalHeight + (scrollOffset * -1);
        //        headerImageFrame.Y = 0 - (scrollOffset * -1);
        //    }
        //    else
        //    {
        //        headerImageFrame.Height = originalHeight;
        //        headerImageFrame.Y = 0 + scrollOffset / 2;
        //    }

        //    parallax.Frame = headerImageFrame;
        //}
    }
}