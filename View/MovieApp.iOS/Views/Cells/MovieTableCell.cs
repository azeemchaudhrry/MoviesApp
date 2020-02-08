using System;
using Foundation;
using MovieApp.Core.Contracts;
using MovieApp.Entities;
using MovieApp.iOS.Utils;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MovieApp.iOS.Views.Cells
{
    public partial class MovieTableCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("MovieTableCell");
        public static readonly UINib Nib;

        public IFavoriteButtonImplementor<Movie> favoriteButtonImplementor;

        static MovieTableCell()
        {
            Nib = UINib.FromName("MovieTableCell", NSBundle.MainBundle);
        }

        public static MovieTableCell Create()
        {
            return (MovieTableCell)Nib.Instantiate(null,null)[0];
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            posterImageView.Layer.CornerRadius = 10;
            posterImageView.Layer.MasksToBounds = true;

            UpdateFonts();
        }

        private void UpdateFonts()
        {
            titleLabel.Font = FontUtils.GetBoldFont(17);
            genreLabel.Font = FontUtils.GetNormalFont(14);
            ratingLabel.Font = FontUtils.GetBoldFont(14);
        }

        protected MovieTableCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var bindings = this.CreateBindingSet<MovieTableCell, Movie>();
                bindings.Bind(titleLabel).For(p => p.Text).To(vm => vm.Title);
                bindings.Bind(posterImageView).For(p => p.ImagePath).To(vm => vm.PosterUrl);
                bindings.Bind(genreLabel).For(p => p.Text).To(vm => vm.ReleaseDate);
                bindings.Bind(ratingLabel).For(p => p.Text).To(vm => vm.VoteAverage);
                bindings.Apply();
            });
        }

        partial void favButtonTapped(Foundation.NSObject sender)
        {
            if (DataContext is Movie movie)
            {
                movie.IsFavorite = !movie.IsFavorite;
                favoriteButtonImplementor.FavoriteButtonTapped((Movie)DataContext);
            }
        }
    }
}
