using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;

namespace MovieApp.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NoInternetViewModel : MvxNavigationViewModel<string>
    {
        public string ErrorText { get; set; }
        public IMvxCommand CloseCommand => new MvxAsyncCommand(() => NavigationService.Close(this));

        public NoInternetViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : 
            base(logProvider, navigationService)
        {
        }

        public override void Prepare(string parameter)
        {
            ErrorText = parameter;
        }
    }
}
