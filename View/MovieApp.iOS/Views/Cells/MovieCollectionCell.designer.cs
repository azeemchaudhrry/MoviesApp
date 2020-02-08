// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MovieApp.iOS.Views.Cells
{
	[Register ("MovieCollectionCell")]
	partial class MovieCollectionCell
	{
		[Outlet]
		public UIKit.UIImageView favoriteImageView { get; set; }

		[Outlet]
		FFImageLoading.Cross.MvxCachedImageView posterImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel ratingLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIImageView starImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel titleLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel yearLabel { get; set; }

		[Action ("favButtonTapped:")]
		partial void favButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (posterImageView != null) {
				posterImageView.Dispose ();
				posterImageView = null;
			}

			if (ratingLabel != null) {
				ratingLabel.Dispose ();
				ratingLabel = null;
			}

			if (starImageView != null) {
				starImageView.Dispose ();
				starImageView = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (yearLabel != null) {
				yearLabel.Dispose ();
				yearLabel = null;
			}

			if (favoriteImageView != null) {
				favoriteImageView.Dispose ();
				favoriteImageView = null;
			}
		}
	}
}
