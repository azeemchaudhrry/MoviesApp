using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MovieApp.Core;
using MovieApp.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using MovieApp.Droid.Extensions.RecyclerViewExt;
using System;
using MovieApp.Droid.Views.Adapters;
using MovieApp.Droid.Extensions;

namespace MovieApp.Droid.Views
{
    public class DiscoverView : MvxFragment<DiscoverViewModel>
        , IMvxOverridePresentationAttribute
    {
        MvxRecyclerView mvxRecyclerView;
        MvxRecyclerView genreRecyclerView;
        DiscoverRecyclerAdapter adapter;
        LinearLayoutManager layoutManager;
        View view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _ = base.OnCreateView(inflater, container, savedInstanceState);

            view = this.BindingInflate(Resource.Layout.discover_main, null);

            HasOptionsMenu = true;

            InitComponents();

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_main, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_list_module)
            {
                if(item.IsChecked)
                {
                    item.SetChecked(false);
                    ViewModel.ViewType = Core.Models.ViewType.Collection;
                    item.SetIcon(Resource.Drawable.ic_action_view_list);
                }
                else
                {
                    item.SetChecked(true);
                    ViewModel.ViewType = Core.Models.ViewType.List;
                    item.SetIcon(Resource.Drawable.ic_action_view_module);
                }
                UpdateLayout();
                return true;
            }
            if(id == Resource.Id.action_search)
            {
                ViewModel.SearchCommand.Execute();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void InitComponents()
        {
            //Movie RecyclerView
            mvxRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.movies_recyclerView);
            layoutManager = new GridLayoutManager(this.Context, 2);
            adapter = new DiscoverRecyclerAdapter((IMvxAndroidBindingContext)BindingContext);
            mvxRecyclerView.AddItemDecoration(new GridSpacingItemDecoration(2, Utils.DpToPx(12), true, 0));

            //Infinite Scrolling
            var onScrollListener = new EndlessRecyclerOnScrollListener();
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) => {
                ViewModel.LoadMoreDataCommand.Execute(true);
            };
            mvxRecyclerView.AddOnScrollListener(onScrollListener);
            mvxRecyclerView.SetLayoutManager(layoutManager);
            mvxRecyclerView.Adapter = adapter;

            //Genre RecyclerView
            genreRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.genre_recyclerView);
            var genreAdapter = new FiltersRecyclerAdapter((IMvxAndroidBindingContext)BindingContext);
            var genreLayoutManager = new LinearLayoutManager(this.Context, LinearLayoutManager.Horizontal, false);
            genreRecyclerView.SetLayoutManager(genreLayoutManager);
            genreRecyclerView.AddItemDecoration(new LinearSpaceItemDecoration(Utils.DpToPx(16), Utils.DpToPx(6)));
            genreRecyclerView.Adapter = genreAdapter;
        }

        public void UpdateLayout()
        {
            layoutManager = new GridLayoutManager(this.Context, ViewModel.ViewType == Core.Models.ViewType.List ? 1 : 2);
            mvxRecyclerView.SetLayoutManager(layoutManager);
            adapter.NotifyDataSetChanged();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxBottomNavigationViewPresentationAttribute()
            {
                BottomNavigationResourceId = Resource.Id.bottom_navigation,
                Title = "Discover",
                FragmentContentId = Resource.Id.bottom_tabs_content_frame,
                DrawableItemId = Resource.Drawable.ic_action_discover,
                ActivityHostViewModelType = typeof(HomeViewModel),
                IsCacheableFragment = true
            };
        }
    }
}