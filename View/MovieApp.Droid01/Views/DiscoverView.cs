using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Bumptech.Glide;
using MovieApp.Constants;
using MovieApp.Core;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using System;

namespace MovieApp.Droid.Views
{
    public class DiscoverView : MvxFragment<DiscoverViewModel>
        , IMvxOverridePresentationAttribute
    {
        MvxRecyclerView mvxRecyclerView;
        DiscoverRecyclerAdapter adapter;
        LinearLayoutManager layoutManager;
        View view;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _ = base.OnCreateView(inflater, container, savedInstanceState);

            view = this.BindingInflate(Resource.Layout.discover_listing, null);

            InitComponents();

            return view;
        }

        private void InitComponents()
        {
            mvxRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.movies_recyclerView);
            layoutManager = new GridLayoutManager(this.Context, 2);
            adapter = new DiscoverRecyclerAdapter(this.Context);
            mvxRecyclerView.SetLayoutManager(layoutManager);
            mvxRecyclerView.Adapter = adapter;
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            return new MvxTabLayoutPresentationAttribute()
            {
                TabLayoutResourceId = Resource.Id.tabLayout,
                ViewPagerResourceId = Resource.Id.viewPager,
                Title = "Discover",
                ActivityHostViewModelType = typeof(HomeViewModel)
            };
        }
    }

    public class DiscoverRecyclerAdapter : MvxRecyclerAdapter
    {
        Context context;
        public DiscoverRecyclerAdapter(Context _context)
        {
            context = _context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            var dataItem = GetItem(position) as Movie;
            var imageView = holder.ItemView.FindViewById<AppCompatImageView>(Resource.Id.poster_imageView);

            Glide
                .With(context)
                .Load($"{Configurations.ImageBaseUrl}{dataItem.poster_path}")
                .Into(imageView);
        }
    }
}