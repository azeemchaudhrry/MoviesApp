using MvvmCross.Commands;
using MovieApp.Entities;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Plugin.Media;
using System;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using MvvmCross;
using System.IO;
using MovieApp.Core.Utilities;

namespace MovieApp.Core.ViewModels
{
    public class MovieReviewViewModel : MvxNavigationViewModel<MovieReview, MovieReview>
    {
        #region Properties
        public IUserDialogs UserDialogs { get; set; }
        public MovieReview ExistingReview { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public float? Rating { get; set; }
        public byte[] Attachement 
        { 
            get 
            {
                if (SelectedMediaFile == null) return null;
                using (var memoryStream = new MemoryStream())
                {
                    SelectedMediaFile.GetStream().CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        public MediaFile SelectedMediaFile { get; set; }

        #endregion End Properties

        #region Commands
        public IMvxCommand CloseCommand => new MvxCommand(() => NavigationService.Close(this));
        public IMvxAsyncCommand SaveReviewCommand => new MvxAsyncCommand(SaveReview);
        private Task SaveReview()
        {
            var review = new MovieReview();
            review.MovieId = ExistingReview.MovieId;
            review.Title = string.IsNullOrEmpty(Title) ? ExistingReview.Title : Title;
            review.Review = string.IsNullOrEmpty(Review) ? ExistingReview.Review : Review;
            review.Rating = Rating.HasValue ? Rating.Value : ExistingReview.Rating;
            review.Attachment = Attachement != null ? Attachement : ExistingReview.Attachment;
            AppData.MovieReviews.Add(review);
            return NavigationService.Close(this, review);
        }
        public IMvxCommand SelectFromGalleryCommand => new MvxAsyncCommand(SelectFromGallery);

        private async Task SelectFromGallery()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await UserDialogs.AlertAsync("Photos Not Supported", ":( Permission not granted to photos.");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    SaveMetaData = true
                });


                if (file == null)
                    return;

                SelectedMediaFile = file;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }

        public IMvxCommand SelectFromCameraCommand => new MvxAsyncCommand(SelectFromCamera);

        private async Task SelectFromCamera()
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await UserDialogs.AlertAsync("No Camera", ":( No camera available.");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Test",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                    return;

                SelectedMediaFile = file;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
        }
        #endregion End Commands

        #region Constructor
        public MovieReviewViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService,
            IUserDialogs userDialogs) : base(logProvider, navigationService)
        {
            UserDialogs = userDialogs;
        }

        #endregion End Constructor

        #region Overridden Methods

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void Prepare(MovieReview parameter)
        {
            ExistingReview = parameter;
        }

        #endregion End Override Methods

        #region Public Methods

        #endregion End Public Methods

        #region Private Methods

        #endregion End Private Methods
    }
}
