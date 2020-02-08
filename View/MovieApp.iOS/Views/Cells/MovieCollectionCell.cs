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
    public partial class MovieCollectionCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("MovieCollectionCell");
        public static readonly UINib Nib;

        public IFavoriteButtonImplementor<Movie> favoriteButtonImplementor;

        static MovieCollectionCell()
        {
            Nib = UINib.FromName("MovieCollectionCell", NSBundle.MainBundle);
        }

        public static MovieCollectionCell Create()
        {
            return (MovieCollectionCell)Nib.Instantiate(null,null)[0];
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            posterImageView.Layer.CornerRadius = 10;
            posterImageView.Layer.MasksToBounds = true;
        }

        protected MovieCollectionCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var bindings = this.CreateBindingSet<MovieCollectionCell, Movie>();
                bindings.Bind(titleLabel).For(p => p.Text).To(vm => vm.Title);
                bindings.Bind(posterImageView).For(p => p.ImagePath).To(vm => vm.PosterUrl);
                bindings.Bind(yearLabel).For(p => p.Text).To(vm => vm.Year);
                bindings.Bind(ratingLabel).For(p => p.Text).To(vm => vm.VoteAverage);
                bindings.Apply();
            });
        }

        partial void favButtonTapped(NSObject sender)
        {
            if (DataContext is Movie movie)
            {
                movie.IsFavorite = !movie.IsFavorite;
                UpdateStatus(favoriteImageView, movie);
                favoriteButtonImplementor.FavoriteButtonTapped(movie);
            }
        }

        private void UpdateStatus(UIImageView favoriteImage, Movie movie)
        {
            if (movie.IsFavorite)
            {
                favoriteImage.Image = UIImage.FromBundle("favorite-filled").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                favoriteImage.TintColor = UIColor.Red;
            }
            else
            {
                favoriteImage.Image = UIImage.FromBundle("favorite-gray").ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                favoriteImage.TintColor = UIColorUtils.GetAppTextLightColor();
            }
        }
    }
}