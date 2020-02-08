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
	[Register ("MovieDetailsView")]
	partial class MovieDetailsView
	{
		[Outlet]
		UIKit.UIImageView attachmentImageView { get; set; }

		[Outlet]
		UIKit.UIButton favoriteButton { get; set; }

		[Outlet]
		UIKit.UIImageView favoriteImageView { get; set; }

		[Outlet]
		UIKit.UINavigationBar navigationItem { get; set; }

		[Outlet]
		UIKit.UITextView overviewLabel { get; set; }

		[Outlet]
		UIKit.UIImageView posterImageView { get; set; }

		[Outlet]
		UIKit.UILabel ratingLabel { get; set; }

		[Outlet]
		UIKit.UILabel releaseDateLabel { get; set; }

		[Outlet]
		UIKit.UITextView reviewDetailsTextView { get; set; }

		[Outlet]
		UIKit.UILabel reviewRatingView { get; set; }

		[Outlet]
		UIKit.UILabel reviewTitleLabel { get; set; }

		[Outlet]
		UIKit.UIView reviewView { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		UIKit.UITableView tableView { get; set; }

		[Outlet]
		UIKit.UICollectionView tagsCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel titleLabel { get; set; }

		[Outlet]
		UIKit.UILabel votesLabel { get; set; }

		[Outlet]
		UIKit.UIButton writeReviewButton { get; set; }

		[Action ("backButtonTapped:")]
		partial void backButtonTapped (Foundation.NSObject sender);

		[Action ("favoriteButtonTapped:")]
		partial void favoriteButtonTapped (Foundation.NSObject sender);

		[Action ("writeReviewBtnTapped:")]
		partial void writeReviewBtnTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (attachmentImageView != null) {
				attachmentImageView.Dispose ();
				attachmentImageView = null;
			}

			if (favoriteButton != null) {
				favoriteButton.Dispose ();
				favoriteButton = null;
			}

			if (navigationItem != null) {
				navigationItem.Dispose ();
				navigationItem = null;
			}

			if (overviewLabel != null) {
				overviewLabel.Dispose ();
				overviewLabel = null;
			}

			if (posterImageView != null) {
				posterImageView.Dispose ();
				posterImageView = null;
			}

			if (ratingLabel != null) {
				ratingLabel.Dispose ();
				ratingLabel = null;
			}

			if (releaseDateLabel != null) {
				releaseDateLabel.Dispose ();
				releaseDateLabel = null;
			}

			if (reviewDetailsTextView != null) {
				reviewDetailsTextView.Dispose ();
				reviewDetailsTextView = null;
			}

			if (reviewRatingView != null) {
				reviewRatingView.Dispose ();
				reviewRatingView = null;
			}

			if (reviewTitleLabel != null) {
				reviewTitleLabel.Dispose ();
				reviewTitleLabel = null;
			}

			if (reviewView != null) {
				reviewView.Dispose ();
				reviewView = null;
			}

			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}

			if (tagsCollectionView != null) {
				tagsCollectionView.Dispose ();
				tagsCollectionView = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (votesLabel != null) {
				votesLabel.Dispose ();
				votesLabel = null;
			}

			if (writeReviewButton != null) {
				writeReviewButton.Dispose ();
				writeReviewButton = null;
			}

			if (favoriteImageView != null) {
				favoriteImageView.Dispose ();
				favoriteImageView = null;
			}
		}
	}
}
