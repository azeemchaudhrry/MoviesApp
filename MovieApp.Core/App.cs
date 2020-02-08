using Acr.UserDialogs;
using MovieApp.Contracts;
using MovieApp.Core.Utilities;
using MvvmCross;
using MvvmCross.ViewModels;
using Services;
using System.Collections.Generic;

namespace MovieApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.IoCProvider.RegisterSingleton<IMovieService>(new MovieService());

            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            AppData.MovieReviews = new List<Entities.MovieReview>();
            AppData.Movies = new List<Entities.Movie>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}