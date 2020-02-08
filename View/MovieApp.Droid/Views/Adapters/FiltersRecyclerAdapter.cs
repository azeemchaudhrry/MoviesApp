using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Widget;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace MovieApp.Droid.Views.Adapters
{
    public class FiltersRecyclerAdapter : MvxRecyclerAdapter
    {
        IMvxAndroidBindingContext bindingContext;
        public FiltersRecyclerAdapter(IMvxAndroidBindingContext _bindingContext) : base(_bindingContext)
        {
            bindingContext = _bindingContext;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);

            if (GetItem(holder.AdapterPosition) is Genre dataItem && dataItem.IsSelected)
            {
                GradientDrawable shape = new GradientDrawable();
                shape.SetCornerRadius(16);
                shape.SetColor(ContextCompat.GetColorStateList(holder.ItemView.Context, Resource.Color.colorAccent));
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).Background = shape;
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).SetTextColor(Android.Graphics.Color.White);
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).Elevation = 8;
            }
            else
            {
                GradientDrawable shape = new GradientDrawable();
                shape.SetCornerRadius(16);
                shape.SetColor(ContextCompat.GetColorStateList(holder.ItemView.Context, Resource.Color.tag_bg));
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).Background = shape;
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).SetTextColor(ContextCompat.GetColorStateList(holder.ItemView.Context, Resource.Color.textColorLight));
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).Elevation = 0;
            }

            if (!holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).HasOnClickListeners)
            {
                holder.ItemView.FindViewById<TextView>(Resource.Id.genre_button).Click += (sender, e) =>
                    {
                        if (bindingContext.DataContext is DiscoverViewModel discoverViewModel)
                        {
                            discoverViewModel.FilterSelectionCommand.Execute(discoverViewModel.GenreSource[holder.AdapterPosition]);
                            NotifyDataSetChanged();
                        }
                    };
            }
        }
    }
}