using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Platforms.Android;

namespace MovieApp.Droid.Extensions
{
    [Register("spica.smartmedia.droid.mvxextensions.MvxBottomNavigationView")]
    public class MvxBottomNavigationView : BottomNavigationView, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private readonly Dictionary<IMenuItem, Type> _lookup = new Dictionary<IMenuItem, Type>();
        public IMenuItem CurrentItem = null;
        public ICommand HandleNavigate { get; set; }

        public MvxBottomNavigationView(Context context) : base(context)
        {
            SetOnNavigationItemSelectedListener(this);
        }

        public MvxBottomNavigationView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            SetOnNavigationItemSelectedListener(this);
        }

        public MvxBottomNavigationView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            SetOnNavigationItemSelectedListener(this);
        }

        protected MvxBottomNavigationView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public void AddItem(IMenuItem item, Type viewModel, string icon)
        {
            _lookup.Add(item, viewModel);

            // The first item is autoselected
            if (_lookup.Count == 1)
            {
                OnNavigationItemSelected(item);
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (item.Equals(CurrentItem)) return false;

            CurrentItem = item;

            UpdateTitles(item);

            var viewModelType = this.FindItemByMenuItem(item);

            if (viewModelType != null && HandleNavigate.CanExecute(viewModelType))
            {
                HandleNavigate.Execute(viewModelType);
                return true;
            }

            return false;
        }

        private void UpdateTitles(IMenuItem selectedMenuItem)
        {
            foreach (var item in _lookup)
            {
                var menuItem = item.Key;
                SpannableString titleString = new SpannableString(menuItem.TitleFormatted);
                titleString.SetSpan(new CustomTypeFaceSpan("",
                    FontUtils.GetNormalFont(Context),
                    color: Android.Graphics.Color.ParseColor("#888888")),
                    0,
                    titleString.Length(), SpanTypes.InclusiveInclusive);
                menuItem.SetTitle(titleString);
            }
            SpannableString spannableString = new SpannableString(selectedMenuItem.TitleFormatted);
            spannableString.SetSpan(new CustomTypeFaceSpan("",
                FontUtils.GetNormalFont(Context),
                color: Android.Graphics.Color.ParseColor("#de3900")),
                0,
                spannableString.Length(), SpanTypes.InclusiveInclusive);
            selectedMenuItem.SetTitle(spannableString);
        }

        public bool ChangeToFirstTab()
        {
            if (Menu.GetItem(0) != CurrentItem)
            {
                OnNavigationItemSelected(_lookup.First().Key);
                return true;
            }
            return false;
        }

        public IMenuItem FindItemByViewModel(Type viewModel)
        {
            return _lookup.FirstOrDefault(i => i.Value == viewModel).Key;
        }

        public Type FindItemByMenuItem(IMenuItem item)
        {
            return _lookup[item];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SetOnNavigationItemSelectedListener(null);
            }

            base.Dispose(disposing);
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MvxBottomNavigationViewPresentationAttribute :
        MvvmCross.Platforms.Android.Presenters.Attributes.MvxFragmentPresentationAttribute
    {
        public MvxBottomNavigationViewPresentationAttribute()
        {
        }

        public MvxBottomNavigationViewPresentationAttribute(int bottomNavigationResourceId, string title, int drawableItemId = int.MinValue,
            Type activityHostViewModelType = null, int fragmentContentId = int.MinValue, bool addToBackStack = false, Type fragmentHostViewType = null,
            bool isCacheableFragment = true, string tag = null)
            : base(activityHostViewModelType, fragmentContentId, addToBackStack,
                  int.MinValue, int.MinValue, int.MinValue, int.MinValue, int.MinValue, fragmentHostViewType,
                  isCacheableFragment, tag)
        {
            BottomNavigationResourceId = bottomNavigationResourceId;
            DrawableItemId = drawableItemId;
            Title = title;
        }

        public MvxBottomNavigationViewPresentationAttribute(string bottomNavigationResourceName, string title, int drawableItemId = int.MinValue,
            Type activityHostViewModelType = null, bool addToBackStack = false, Type fragmentHostViewType = null,
            bool isCacheableFragment = true, string tag = null)
            : base(activityHostViewModelType, null, addToBackStack, null, null, null,
                null, null, fragmentHostViewType, isCacheableFragment, tag)
        {
            var context = Mvx.IoCProvider.Resolve<IMvxAndroidGlobals>().ApplicationContext;

            BottomNavigationResourceId = !string.IsNullOrEmpty(bottomNavigationResourceName)
                ? context.Resources.GetIdentifier(bottomNavigationResourceName, "id", context.PackageName)
                : global::Android.Resource.Id.Content;
            DrawableItemId = drawableItemId;
            Title = title;
        }

        /// <summary>
        ///     The resource used to get the BottomNavigation from the view
        /// </summary>
        public int BottomNavigationResourceId { get; set; }

        /// <summary>
        ///     The resource used to get the menu item from the view
        /// </summary>
        public int DrawableItemId { get; set; }

        /// <summary>
        ///     The title for the bottom navigation menu item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// This unicode for fontawesome icon
        /// </summary>
        public string UniCode { get; set; }
    }
}