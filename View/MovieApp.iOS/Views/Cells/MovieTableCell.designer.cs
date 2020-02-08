// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MovieApp.iOS.Views.Cells
{
    [Register ("MovieTableCell")]
    partial class MovieTableCell
    {
        [Outlet]
        UIKit.UILabel genreLabel { get; set; }


        [Outlet]
        UIKit.UILabel ratingLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FFImageLoading.Cross.MvxCachedImageView posterImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel titleLabel { get; set; }


        [Action ("favButtonTapped:")]
        partial void favButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (posterImageView != null) {
                posterImageView.Dispose ();
                posterImageView = null;
            }

            if (titleLabel != null) {
                titleLabel.Dispose ();
                titleLabel = null;
            }
        }
    }
}