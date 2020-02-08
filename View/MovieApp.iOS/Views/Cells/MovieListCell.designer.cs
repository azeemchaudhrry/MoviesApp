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
	[Register ("MovieListCell")]
	partial class MovieListCell
	{
		[Outlet]
		UIKit.UIButton favButotn { get; set; }

		[Outlet]
		public UIKit.UIImageView favoriteImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		FFImageLoading.Cross.MvxCachedImageView posterImageView { get; set; }

		[Outlet]
		UIKit.UILabel ratingLabel { get; set; }

		[Outlet]
		UIKit.UILabel releaseDateLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UILabel titleLabel { get; set; }

		[Action ("favButtonTapped:")]
		partial void favButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (favButotn != null) {
				favButotn.Dispose ();
				favButotn = null;
			}

			if (ratingLabel != null) {
				ratingLabel.Dispose ();
				ratingLabel = null;
			}

			if (releaseDateLabel != null) {
				releaseDateLabel.Dispose ();
				releaseDateLabel = null;
			}

			if (posterImageView != null) {
				posterImageView.Dispose ();
				posterImageView = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (favoriteImageView != null) {
				favoriteImageView.Dispose ();
				favoriteImageView = null;
			}
		}
	}
}
