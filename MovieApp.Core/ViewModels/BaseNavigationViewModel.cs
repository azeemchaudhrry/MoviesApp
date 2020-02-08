using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using MovieApp.Contracts;
using PropertyChanged;
using MvvmCross.Commands;

namespace MovieApp.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseNavigationViewModel : MvxNavigationViewModel
    {
        #region Properties
        public bool IsLoading { get; set; }
        public bool IsRefreshing { get; set; }
        public bool NoDataAvailable { get; set; }
        public IMovieService MovieService { get; set; }
        #endregion End Properties

        #region Commands

        public IMvxCommand CloseCommand => new MvxCommand(() => NavigationService.Close(this));
        #endregion End Commands

        #region Constructor
        public BaseNavigationViewModel(
            IMvxLogProvider logProvider, 
            IMvxNavigationService navigationService,
            IMovieService movieService) : base(logProvider, navigationService)
        {
            MovieService = movieService;
        }

        #endregion End Constructor

        #region overridden methods

        public override Task Initialize()
        {
            return base.Initialize();
        }

        #endregion End Override Methods

        #region Public methods

        #endregion End Public Methods

        #region Private methods

        #endregion End Private Methods
    }
}
