using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using MovieApp.Core.ViewModels;
using MovieApp.Droid.Extensions;
using MovieApp.Droid.Extensions.RecyclerViewExt;
using MovieApp.Droid.Views.Adapters;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace MovieApp.Droid.Views
{
    [Activity(Label = "SearchView", Theme = "@style/AppTheme")]
    public class SearchView : MvxAppCompatActivity<SearchViewModel>
        , ITextWatcher
        , View.IOnClickListener
    {
        private Timer timer;
        private EditText searchEditText;
        private Android.Support.V7.Widget.Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.search_layout);

            InitComponents();

            ManageToolbar();
        }

        private void InitComponents()
        {
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            //Movie RecyclerView
            var mvxRecyclerView = FindViewById<MvxRecyclerView>(Resource.Id.search_layout_recyclerview);
            var layoutManager = new GridLayoutManager(this, 2);
            var adapter = new SearchViewRecyclerAdapter((IMvxAndroidBindingContext)BindingContext);
            mvxRecyclerView.AddItemDecoration(new GridSpacingItemDecoration(2, 16, true, 0));

            searchEditText = FindViewById<EditText>(Resource.Id.search_bar_edit_text);
            searchEditText.AddTextChangedListener(this);
            searchEditText.Typeface = FontUtils.GetNormalFont(this);

            //Infinite Scrolling
            var onScrollListener = new EndlessRecyclerOnScrollListener();
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
            {
                ViewModel.LoadMoreDataCommand.Execute(true);
            };
            mvxRecyclerView.AddOnScrollListener(onScrollListener);
            mvxRecyclerView.SetLayoutManager(layoutManager);
            mvxRecyclerView.Adapter = adapter;

            RecyclerView.ItemAnimator animator = mvxRecyclerView.GetItemAnimator();
            if (animator is DefaultItemAnimator)
            {
                ((DefaultItemAnimator)animator).SupportsChangeAnimations = false;
            }
        }

        private void ManageToolbar()
        {
            if (toolbar == null) return;
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_action_back);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            toolbar.SetNavigationOnClickListener(this);
        }

        #region ITextWatcher
        public void AfterTextChanged(IEditable s)
        {
            timer = new Timer();
            timer.Schedule(new CustomTimerTask(this, s.ToString()), 700);
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {

        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            timer?.Cancel();
        }

        public void OnClick(View v)
        {
            ViewModel.CloseCommand.Execute();
        }

        public class CustomTimerTask : TimerTask
        {
            private SearchView searchView;
            private string searchKey;
            public CustomTimerTask(SearchView _ref, string _searchKey)
            {
                searchView = _ref;
                searchKey = _searchKey;
            }
            public override void Run()
            {
                searchView.RunOnUiThread(() =>
                {
                    searchView.ViewModel.SearchCommand.Execute(searchKey);
                });
            }
        }
        #endregion


    }
}