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
    public class FavoritesView : MvxFragment<FavoritesViewModel>
        , IMvxOverridePresentationAttribute
    {
        View view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _ = base.OnCreateView(inflater, container, savedInstanceState);

            view = this.BindingInflate(Resource.Layout.favorites_listing, null);

            return view;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxTabLayoutPresentationAttribute()
            {
                TabLayoutResourceId = Resource.Id.tabLayout,
                ViewPagerResourceId = Resource.Id.viewPager,
                Title = "Favorites",
                ActivityHostViewModelType = typeof(HomeViewModel)
            };
        }
    }
}