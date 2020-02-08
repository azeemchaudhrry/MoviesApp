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
    public partial class MovieListCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("MovieListCell");
        public static readonly UINib Nib;

        public IFavoriteButtonImplementor<Movie> favoriteButtonImplementor;

        static MovieListCell()
        {
            Nib = UINib.FromName("MovieListCell", NSBundle.MainBundle);
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
            releaseDateLabel.Font = FontUtils.GetNormalFont(14);
            ratingLabel.Font = FontUtils.GetBoldFont(14);
        }

        protected MovieListCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var bindings = this.CreateBindingSet<MovieListCell, Movie>();
                bindings.Bind(titleLabel).For(p => p.Text).To(vm => vm.Title);
                bindings.Bind(posterImageView).For(p => p.ImagePath).To(vm => vm.PosterUrl);
                bindings.Bind(ratingLabel).For(p => p.Text).To(vm => vm.VoteAverage);
                bindings.Bind(releaseDateLabel).For(p => p.Text).To(vm => vm.ReleaseDate);
                bindings.Apply();

                UpdateMovieFavoriteStatus(favButotn);
            });
        }

        partial void favButtonTapped(Foundation.NSObject sender)
        {
            if(sender is UIButton button)
            {
                UpdateMovieFavoriteStatus(button);
            }
        }

        private void UpdateMovieFavoriteStatus(UIButton button)
        {
            if (DataContext is Movie movie)
            {
                if (movie.IsFavorite)
                {
                    button.SetImage(UIImage.FromBundle("favorite"), UIControlState.Normal);
                }
                else
                {
                    button.SetImage(UIImage.FromBundle("favorite-filled"), UIControlState.Normal);
                }
                favoriteButtonImplementor.FavoriteButtonTapped(movie);
            }
        }
    }
}