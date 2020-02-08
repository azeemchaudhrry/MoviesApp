using Foundation;
using MovieApp.Core.ViewModels;
using MovieApp.iOS.Utils;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using SkiaRate;
using System;
using UIKit;

namespace MovieApp.iOS
{
    [MvxFromStoryboard("Main")]
    public partial class MovieReviewView : MvxViewController<MovieReviewViewModel>
        , IMvxOverridePresentationAttribute
    {
        RatingView view;
        public MovieReviewView()
        {

        }

        public MovieReviewView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.Title = "Add Review";
            UpdateFonts();
            AddStartRatingView();


            var set = this.CreateBindingSet<MovieReviewView, MovieReviewViewModel>();
            set.Bind(titleTextField).For(p => p.Text).To(vm => vm.Title);
            set.Bind(commentsTextField).For(p => p.Text).To(vm => vm.Review);
            set.Bind(view).For(p => p.Value).To(vm => vm.Rating);
            set.Bind(ratingCount).For(p => p.Text).To(vm => vm.Rating).Mode(MvvmCross.Binding.MvxBindingMode.OneWay);
            set.Apply();

            SetupNavigationBar();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            writeReviewButton.BackgroundColor = UIColorUtils.GetAppThemeColor();
            writeReviewButton.Layer.CornerRadius = 10;
            writeReviewButton.Layer.MasksToBounds = true;

            cancelButton.BackgroundColor = UIColorUtils.GetAppThemeColor();
            cancelButton.Layer.CornerRadius = 10;
            cancelButton.Layer.MasksToBounds = true;

            View.AddGestureRecognizer(new UITapGestureRecognizer(DismissKeyboard));
        }

        private void DismissKeyboard()
        {
            View.EndEditing(true);
        }

        private void UpdateFonts()
        {
            titleTextField.Font = FontUtils.GetNormalFont(17);
            commentsTextField.Font = FontUtils.GetNormalFont(17);
            writeReviewButton.Font = FontUtils.GetNormalFont(14);

            ratingCount.Font = FontUtils.GetNormalFont(14);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ViewModel.SelectedMediaFile)))
            {
                AddAttachment();
            }
        }

        private void AddAttachment()
        {
            if (ViewModel.SelectedMediaFile != null)
            {
                attachmentImageView.Image = UIImage.LoadFromData(NSData.FromStream(ViewModel.SelectedMediaFile.GetStream()));
            }
        }

        private void AddStartRatingView()
        {
            view = new RatingView();
            view.Frame = starRating.Bounds;
            view.Path = PathConstants.Star;
            view.ColorOn = UIColorUtils.GetAppThemeColor();
            view.OutlineOffColor = UIColorUtils.GetAppTextDarkColor();
            view.Value = ViewModel.Rating == null ? 0 : (double)ViewModel.Rating;
            view.ValueChanged += View_ValueChanged;

            starRating.AddSubview(view);
        }

        private void View_ValueChanged(object sender, EventArgs e)
        {
            ratingCount.Text = (view.Value * 2.0).ToString("0.00");
        }

        private void SetupNavigationBar()
        {
            var backButton = new UIBarButtonItem(UIBarButtonSystemItem.Close, CloseCurrent);
            backButton.TintColor = UIColor.Black;
            NavigationItem.LeftBarButtonItem = backButton;
        }

        partial void saveReviewButtonTapped(Foundation.NSObject sender)
        {
            SaveReview();
        }

        private void SaveReview()
        {
            if (string.IsNullOrEmpty(titleTextField.Text)) 
            {
                titleTextField.Layer.BorderColor = UIColor.Red.CGColor;
                return; 
            }
            else
            {
                titleTextField.Layer.BorderColor = UIColor.Gray.CGColor;
            }
            if (string.IsNullOrEmpty(commentsTextField.Text)) 
            {
                commentsTextField.Layer.BorderColor = UIColor.Red.CGColor;
                return; 
            }
            else
            {
                commentsTextField.Layer.BorderColor = UIColor.Gray.CGColor;
            }
            ViewModel.Rating = (float?)Math.Round(view.Value * 2.0, 2);
            ViewModel.SaveReviewCommand.Execute();
        }

        private void CloseCurrent(object sender, EventArgs e)
        {
            ViewModel.CloseCommand.Execute();
        }

        partial void uploadMemoryBtnTapped(NSObject sender)
        {
            var alert = UIAlertController.Create(title: "Select source?", "", UIAlertControllerStyle.ActionSheet);

            alert.AddAction(UIAlertAction.Create(title: "Gallary", UIAlertActionStyle.Default, (handler) => { ViewModel.SelectFromGalleryCommand.Execute(); }));
            alert.AddAction(UIAlertAction.Create(title: "Camera", UIAlertActionStyle.Default, (handler) => { ViewModel.SelectFromCameraCommand.Execute(); }));
            alert.AddAction(UIAlertAction.Create(title: "Cancel", style: UIAlertActionStyle.Cancel, handler: null));

            this.PresentViewController(alert, animated: true, null);
        }

        partial void cancelButtonTapped(Foundation.NSObject sender)
        {
            ViewModel.CloseCommand.Execute();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                return new MvxModalPresentationAttribute()
                {
                    WrapInNavigationController = true,
                    ModalPresentationStyle = UIModalPresentationStyle.Popover
                };
            }
            else
            {
                return new MvxModalPresentationAttribute()
                {
                    WrapInNavigationController = true
                };
            }
        }
    }
}