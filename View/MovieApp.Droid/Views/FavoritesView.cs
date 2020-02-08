using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MovieApp.Core;
using MovieApp.Core.ViewModels;
using MovieApp.Droid.Extensions;
using MovieApp.Droid.Views.Adapters;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace MovieApp.Droid.Views
{
    public class FavoritesView : MvxFragment<FavoritesViewModel>
        , IMvxOverridePresentationAttribute
    {
        MvxRecyclerView mvxRecyclerView;
        LinearLayoutManager layoutManager;
        View view;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _ = base.OnCreateView(inflater, container, savedInstanceState);

            view = this.BindingInflate(Resource.Layout.favorites_main, null);

            InitComponents();

            return view;
        }

        private void InitComponents()
        {
            //Favorite Movie RecyclerView
            mvxRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.movies_recyclerView);
            layoutManager = new LinearLayoutManager(this.Context, LinearLayoutManager.Vertical, false);
            var adapter = new FavoriteRecyclerAdapter((IMvxAndroidBindingContext)BindingContext);
            mvxRecyclerView.Adapter = adapter;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxBottomNavigationViewPresentationAttribute()
            {
                BottomNavigationResourceId = Resource.Id.bottom_navigation,
                Title = "Favorites",
                FragmentContentId = Resource.Id.bottom_tabs_content_frame,
                DrawableItemId = Resource.Drawable.ic_action_favorite,
                ActivityHostViewModelType = typeof(HomeViewModel),
                IsCacheableFragment = true
            };
        }
    }
}