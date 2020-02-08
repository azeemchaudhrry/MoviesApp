// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MovieApp.iOS
{
	[Register ("MovieReviewView")]
	partial class MovieReviewView
	{
		[Outlet]
		UIKit.UIImageView attachmentImageView { get; set; }

		[Outlet]
		UIKit.UIButton cancelButton { get; set; }

		[Outlet]
		UIKit.UITextField commentsTextField { get; set; }

		[Outlet]
		UIKit.UILabel ratingCount { get; set; }

		[Outlet]
		UIKit.UIView starRating { get; set; }

		[Outlet]
		UIKit.UITextField titleTextField { get; set; }

		[Outlet]
		UIKit.UIButton writeReviewButton { get; set; }

		[Action ("cancelButtonTapped:")]
		partial void cancelButtonTapped (Foundation.NSObject sender);

		[Action ("saveReviewButtonTapped:")]
		partial void saveReviewButtonTapped (Foundation.NSObject sender);

		[Action ("uploadMemoryBtnTapped:")]
		partial void uploadMemoryBtnTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (attachmentImageView != null) {
				attachmentImageView.Dispose ();
				attachmentImageView = null;
			}

			if (commentsTextField != null) {
				commentsTextField.Dispose ();
				commentsTextField = null;
			}

			if (starRating != null) {
				starRating.Dispose ();
				starRating = null;
			}

			if (titleTextField != null) {
				titleTextField.Dispose ();
				titleTextField = null;
			}

			if (writeReviewButton != null) {
				writeReviewButton.Dispose ();
				writeReviewButton = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}

			if (ratingCount != null) {
				ratingCount.Dispose ();
				ratingCount = null;
			}
		}
	}
}
