using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Android.Text;
using Android.Views;
using MovieApp.Droid.Extensions;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;

namespace MovieApp.Droid.MvxExtensions
{
    public class MvxCustomPresenter : MvxAppCompatViewPresenter
    {
        public MvxCustomPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        public override void RegisterAttributeTypes()
        {
            base.RegisterAttributeTypes();
            AttributeTypesToActionsDictionary.Register<MvxBottomNavigationViewPresentationAttribute>(ShowBottomNavigationFragment, CloseFragment);
        }

        protected virtual Task<bool> ShowBottomNavigationFragment(
            Type view,
            MvxBottomNavigationViewPresentationAttribute attribute,
            MvxViewModelRequest request)
        {
            var bottomNavigationView = FindMvxBottomNavigationViewInFragmentPresentation(attribute);
            if (bottomNavigationView == null)
            {
                throw new Exception("MvxBottomNavigationView not found");
            }
            SpannableString spannableString = new SpannableString(attribute.Title);
            spannableString.SetSpan(new CustomTypeFaceSpan("",
                FontUtils.GetNormalFont(bottomNavigationView.Context),
                color: Android.Graphics.Color.ParseColor("#888888")), 
                0, 
                spannableString.Length(), SpanTypes.InclusiveInclusive);
            var menuItem = bottomNavigationView.Menu.Add(spannableString);
            if (attribute.DrawableItemId != int.MinValue)
            {
                menuItem.SetIcon(attribute.DrawableItemId);
            }
            bottomNavigationView.AddItem(menuItem, request.ViewModelType, attribute.UniCode);
            return Task.FromResult(true);
        }

        private MvxBottomNavigationView FindMvxBottomNavigationViewInFragmentPresentation(MvxBottomNavigationViewPresentationAttribute pagerFragmentAttribute)
        {
            MvxBottomNavigationView bottomNavigationView = null;

            // check for a ViewPager inside a Fragment
            if (pagerFragmentAttribute.FragmentHostViewType != null)
            {
                var fragment = GetFragmentByViewType(pagerFragmentAttribute.FragmentHostViewType);
                bottomNavigationView = fragment.View.FindViewById<MvxBottomNavigationView>(pagerFragmentAttribute.BottomNavigationResourceId);
            }

            // check for a ViewPager inside an Activity
            if (bottomNavigationView == null && pagerFragmentAttribute.ActivityHostViewModelType != null)
            {
                bottomNavigationView = CurrentActivity.FindViewById<MvxBottomNavigationView>(pagerFragmentAttribute.BottomNavigationResourceId);
            }

            return bottomNavigationView;
        }

        public override Task<bool> ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxPagePresentationHint pagePresentationHint)
            {
                var request = new MvxViewModelRequest(pagePresentationHint.ViewModel);
                var attribute = GetPresentationAttribute(request);
                if (attribute is MvxBottomNavigationViewPresentationAttribute bottomNavigationAttribute)
                {
                    MvxBottomNavigationView bottomNavigationView = FindMvxBottomNavigationViewInFragmentPresentation(bottomNavigationAttribute);
                    if (bottomNavigationView != null)
                    {
                        var menuItem = bottomNavigationView.FindItemByViewModel(pagePresentationHint.ViewModel);

                        if (menuItem == null)
                        {
                            //MvvmCross.MvxAndroidLog.Instance.Trace("Did not find a menu item which corresponds to {0}, skipping presentation change...",                                 pagePresentationHint.ViewModel);

                            return Task.FromResult(false);
                        }

                        // If we are deeper in our back stack, we want to make sure that we remove the stack
                        if (CurrentFragmentManager.BackStackEntryCount > 0)
                        {
                            var name = CurrentFragmentManager.GetBackStackEntryAt(0).Name;
                            CurrentFragmentManager.PopBackStackImmediate(name, 1);
                        }

                        PerformShowFragmentTransaction(CurrentFragmentManager, bottomNavigationAttribute, new MvxViewModelRequest(pagePresentationHint.ViewModel));

                        var item = bottomNavigationView.FindItemByViewModel(pagePresentationHint.ViewModel);

                        item.SetChecked(true);

                        return Task.FromResult(true);
                    }
                }
            }
            return base.ChangePresentation(hint);
        }
    }
}