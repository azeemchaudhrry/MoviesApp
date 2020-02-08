using Android.OS;
using Android.Views;
using MovieApp.Core;
using MovieApp.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace MovieApp.Droid.Views
{
    public class NoInternetView : MvxFragment<NoInternetViewModel>
        , IMvxOverridePresentationAttribute
    {
        View view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _ = base.OnCreateView(inflater, container, savedInstanceState);
            view = this.BindingInflate(Resource.Layout.no_internet_layout, null);
            return view;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxFragmentPresentationAttribute()
            {
                ActivityHostViewModelType = typeof(HomeViewModel),
                FragmentContentId = Resource.Id.bottom_tabs_content_frame
            };
        }
    }
}