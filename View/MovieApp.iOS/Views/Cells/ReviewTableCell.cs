using System;

using Foundation;
using UIKit;

namespace MovieApp.iOS.Views.Cells
{
    public partial class ReviewTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ReviewTableCell");
        public static readonly UINib Nib;

        static ReviewTableCell()
        {
            Nib = UINib.FromName("ReviewTableCell", NSBundle.MainBundle);
        }

        protected ReviewTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public static ReviewTableCell Create()
        {
            return (ReviewTableCell)Nib.Instantiate(null, null)[0];
        }

        public void SetData(string title, float? rating, string review)
        {
            titleLabel.Text = title;
            reviewTextView.Text = review;
            ratingLabel.Text = rating.ToString();

        }
    }
}
