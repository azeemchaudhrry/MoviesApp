using Android.Content;
using Android.Support.V7.Widget;
using Com.Like;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace MovieApp.Droid.Views.Adapters
{
    public class SearchViewRecyclerAdapter : MvxRecyclerAdapter
        , Com.Like.IOnLikeListener
    {
        IMvxAndroidBindingContext bindingContext;
        SearchViewModel ViewModel => bindingContext.DataContext as SearchViewModel;
        Context context => bindingContext.LayoutInflaterHolder.LayoutInflater.Context;

        public SearchViewRecyclerAdapter(IMvxAndroidBindingContext bindingContext)
            : base(bindingContext)
        {
            this.bindingContext = bindingContext;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            base.OnBindViewHolder(holder, position);
            var dataItem = GetItem(position) as Movie;

            var favoriteButton = holder.ItemView.FindViewById<LikeButton>(Resource.Id.likeButton);
            if(favoriteButton != null)
            {
                favoriteButton.SetTag(Resource.Id.favorite_item_tag_key, holder.AdapterPosition);
                favoriteButton.SetLiked(new Java.Lang.Boolean(dataItem.IsFavorite));
                favoriteButton.SetOnLikeListener(this);
            }
        }

        public void Liked(LikeButton p0)
        {
            var adapterPosition = (int)p0.GetTag(Resource.Id.favorite_item_tag_key);
            if (adapterPosition > -1)
            {
                ViewModel.ItemsSource[adapterPosition].IsFavorite = true;
                ViewModel.AddToFavoritesCommand.Execute(ViewModel.ItemsSource[adapterPosition]);
            }
        }

        public void UnLiked(LikeButton p0)
        {
            var adapterPosition = (int)p0.GetTag(Resource.Id.favorite_item_tag_key);
            if (adapterPosition > -1)
            {
                ViewModel.ItemsSource[adapterPosition].IsFavorite = false;
                ViewModel.AddToFavoritesCommand.Execute(ViewModel.ItemsSource[adapterPosition]);
            }
        }
    }
}