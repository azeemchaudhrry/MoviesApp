using Android.App;
using Android.OS;
using Android.Views;
using MovieApp.Core;
using Android.Graphics;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Com.Like;
using Android.Support.Design.Chip;
using Android.Widget;
using Android.Support.V4.Graphics.Drawable;
using MovieApp.Droid.Extensions;

namespace MovieApp.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "MovieDetailsView", Theme = "@style/AppTheme")]
    public class MovieDetailsView : MvxAppCompatActivity<MovieDetailsViewModel>
        , View.IOnClickListener
        , IOnLikeListener
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        Android.Support.Design.Widget.CollapsingToolbarLayout collapsingToolbar;
        LikeButton likeButton;
        Refractored.Controls.CircleImageView attachmentImageView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.movie_details_layout);

            InitComponents();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ViewModel.CurrentMovieReview)))
            {
                AddAttachment();
            }
        }

        private void AddAttachment()
        {
            if (ViewModel.CurrentMovieReview != null && ViewModel.CurrentMovieReview.Attachment != null)
            {
                var bitmapSource = BitmapFactory.DecodeByteArray(ViewModel.CurrentMovieReview.Attachment, 0, ViewModel.CurrentMovieReview.Attachment.Length);
                attachmentImageView.Visibility = ViewStates.Visible;
                attachmentImageView.SetImageBitmap(bitmapSource);
            }
        }

        private void InitComponents()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetNavigationOnClickListener(this);
            SetSupportActionBar(toolbar);
            if(SupportActionBar != null)
            {
                SupportActionBar.Title = string.Empty;
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_back);
                for (int i = 0; i < toolbar.ChildCount; i++)
                {
                    View child = toolbar.GetChildAt(i);
                    if (child is TextView toolbarTitle)
                    {
                        toolbarTitle.SetTypeface(FontUtils.GetBoldFont(this), Android.Graphics.TypefaceStyle.Normal);
                        break;
                    }
                }
            }

            collapsingToolbar = FindViewById<Android.Support.Design.Widget.CollapsingToolbarLayout>(Resource.Id.collapsingtoolbar_layout);
            collapsingToolbar.SetExpandedTitleTextAppearance(Resource.Style.ExpandedAppBar);
            collapsingToolbar.SetCollapsedTitleTextAppearance(Resource.Style.CollapsedAppBar);

            likeButton = FindViewById<LikeButton>(Resource.Id.likeButton);
            likeButton.SetOnLikeListener(this);
            
            if (ViewModel.MovieDetails.IsFavorite)
            {
                likeButton.SetLiked(Java.Lang.Boolean.True);
            }

            var chipGroup = FindViewById<ChipGroup>(Resource.Id.genre_chipGroup);
            AddGenreChips(chipGroup);

            var writeareview = FindViewById<Button>(Resource.Id.writeareview_button);
            writeareview.SetTypeface(FontUtils.GetNormalFont(this), TypefaceStyle.Normal);

            attachmentImageView = FindViewById<Refractored.Controls.CircleImageView>(Resource.Id.attachment_imageView);
            if(ViewModel.CurrentMovieReview != null)
            {
                AddAttachment();
            }
        }

        private void AddGenreChips(ChipGroup chipGroup)
        {
            if (chipGroup.ChildCount > 0) chipGroup.RemoveAllViews();
            foreach (var item in ViewModel.MovieDetails.Genres)
            {
                var chip = BuildChip(item);
                chipGroup.AddView(chip);
            }
        }

        private Chip BuildChip(string text)
        {
            var view = LayoutInflater.Inflate(Resource.Layout.default_chip_layout, null);
            var chip = view.FindViewById<Chip>(Resource.Id.default_chip_layout_item);
            chip.Text = text;
            chip.Typeface = FontUtils.GetNormalFont(this);
            return chip;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if(item.ItemId == Android.Resource.Id.Home)
            {
                ViewModel.CloseCommand.Execute();
            }
            return base.OnOptionsItemSelected(item);
        }

        public void OnClick(View v)
        {
            ViewModel.CloseCommand.Execute();
        }

        public void Liked(LikeButton p0)
        {
            ViewModel.MovieDetails.IsFavorite = true;
            ViewModel.AddToFavoritesCommand.Execute(ViewModel.MovieDetails);
        }

        public void UnLiked(LikeButton p0)
        {
            ViewModel.MovieDetails.IsFavorite = false;
            ViewModel.AddToFavoritesCommand.Execute(ViewModel.MovieDetails);
        }
    }
}