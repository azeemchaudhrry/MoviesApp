using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MovieApp.Core.ViewModels;
using MovieApp.Droid.Extensions;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace MovieApp.Droid.Views
{
    [MvxDialogFragmentPresentation]
    [Register(nameof(MovieReviewView))]
    public class MovieReviewView : MvxDialogFragment<MovieReviewViewModel>
        , Android.Support.V7.Widget.Toolbar.IOnMenuItemClickListener
        , View.IOnClickListener
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        View view;
        ImageView memoryImageView;
        RatingBar ratingBar;
        string[] options = { "Camera", "Gallery" };

        public MovieReviewView()
        {

        }

        protected MovieReviewView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _ = base.OnCreateView(inflater, container, savedInstanceState);

            view = this.BindingInflate(Resource.Layout.movie_review_layout, null);

            InitComponents();

            ManageToolbar();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            return view;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ViewModel.SelectedMediaFile)))
            {
                if(ViewModel.SelectedMediaFile != null)
                {
                    Bitmap bitmap = BitmapFactory.DecodeStream(ViewModel.SelectedMediaFile.GetStream());
                    memoryImageView.Visibility = ViewStates.Visible;
                    memoryImageView.SetImageBitmap(bitmap);
                }
            }
        }

        private void InitComponents()
        {
            ratingBar = view.FindViewById<RatingBar>(Resource.Id.review_ratingBar);
            ratingBar.RatingBarChange += RatingBar_RatingBarChange;

            memoryImageView = view.FindViewById<ImageView>(Resource.Id.memory_imageView);
            memoryImageView.Click += MemoryImageView_Click;

            var saveButton = view.FindViewById<Button>(Resource.Id.save_button);
            saveButton.Typeface = FontUtils.GetNormalFont(Context);
            saveButton.Click += SaveButton_Click;

            view.FindViewById<TextInputEditText>(Resource.Id.title_textInputEditText).Typeface = FontUtils.GetNormalFont(Context);
            view.FindViewById<TextInputLayout>(Resource.Id.title_textInputLayout).Typeface = FontUtils.GetNormalFont(Context);

            view.FindViewById<TextInputEditText>(Resource.Id.review_textInputEditText).Typeface = FontUtils.GetNormalFont(Context);
            view.FindViewById<TextInputLayout>(Resource.Id.review_textInputLayout).Typeface = FontUtils.GetNormalFont(Context);
        }

        private void RatingBar_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            if (e.FromUser)
            { 
                view.FindViewById<TextView>(Resource.Id.rating_textView).Text = $"{e.Rating * 2}"; 
            }
        }

        private void MemoryImageView_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
            builder.SetTitle("Please select source");
            builder.SetItems(options, SourceSelection);
            builder.Show();
        }

        private void SourceSelection(object sender, DialogClickEventArgs e)
        {
            if (options[e.Which].Equals(options[0]))
            {
                ViewModel.SelectFromCameraCommand.Execute();
            }
            else if (options[e.Which].Equals(options[1]))
            {
                ViewModel.SelectFromGalleryCommand.Execute();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string error = null;
            if (string.IsNullOrEmpty(ViewModel.Title))
            {
                view.FindViewById<TextInputEditText>(Resource.Id.title_textInputEditText).Error = "Required";
                error = "error";
            }
            else
            {
                view.FindViewById<TextInputEditText>(Resource.Id.title_textInputEditText).Error = null;
            }
            if (string.IsNullOrEmpty(ViewModel.Review))
            {
                view.FindViewById<TextInputEditText>(Resource.Id.review_textInputEditText).Error = "Required";
                error += "error"; ;
            }
            else
            {
                view.FindViewById<TextInputEditText>(Resource.Id.review_textInputEditText).Error = null;
            }
            if (string.IsNullOrEmpty(error))
            {
                ViewModel.Rating = ratingBar.Rating * 2;
                ViewModel.SaveReviewCommand.Execute();
            }
        }

        private void ManageToolbar()
        {
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetNavigationIcon(Resource.Drawable.ic_action_close);
            toolbar.SetNavigationOnClickListener(this);
            toolbar.SetOnMenuItemClickListener(this);
            toolbar.SetTitle(Resource.String.movie_review_add_review);

            for (int i = 0; i < toolbar.ChildCount; i++)
            {
                View child = toolbar.GetChildAt(i);
                if (child is TextView toolbarTitle)
                {
                    toolbarTitle.SetTypeface(FontUtils.GetBoldFont(Context), Android.Graphics.TypefaceStyle.Normal);
                    break;
                }
            }
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var dialog = base.OnCreateDialog(savedInstanceState);
            try
            {
                RelativeLayout root = new RelativeLayout(this.Context);
                root.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                dialog.SetContentView(root);

                dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));
                dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                dialog.Window.Attributes.WindowAnimations = Resource.Style.DialogFragmentAnimation;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return dialog;
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_save:
                    ViewModel.SaveReviewCommand.Execute(true);
                    break;
                case Android.Resource.Id.Home:
                    ViewModel.CloseCommand.Execute(true);
                    break;
            }
            return true;
        }

        public void OnClick(View v)
        {
            ViewModel.CloseCommand.Execute(true);
        }
    }
}