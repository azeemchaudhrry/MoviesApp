using System.Linq;
using Android.Content;
using Android.Support.Design.Chip;
using Android.Support.V7.Widget;
using Android.Widget;
using Com.Like;
using MovieApp.Core.ViewModels;
using MovieApp.Entities;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using static Android.Widget.TextView;

namespace MovieApp.Droid.Views.Adapters
{
    public class FavoriteRecyclerAdapter : MvxRecyclerAdapter
        , Com.Like.IOnLikeListener
    {
        IMvxAndroidBindingContext bindingContext;
        FavoritesViewModel ViewModel => bindingContext.DataContext as FavoritesViewModel;
        Context context => bindingContext.LayoutInflaterHolder.LayoutInflater.Context;

        public FavoriteRecyclerAdapter(IMvxAndroidBindingContext bindingContext)
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
            if (adapterPosition > -1 && ViewModel.ItemsSource.Count > adapterPosition)
            {
                ViewModel.ItemsSource[adapterPosition].IsFavorite = true;
                ViewModel.RemoveFavoriteItemCommand.Execute(ViewModel.ItemsSource[adapterPosition]);
            }
        }

        public void UnLiked(LikeButton p0)
        {
            var adapterPosition = (int)p0.GetTag(Resource.Id.favorite_item_tag_key);
            if (adapterPosition > -1 && ViewModel.ItemsSource.Count > adapterPosition)
            {
                ViewModel.ItemsSource[adapterPosition].IsFavorite = false;
                ViewModel.RemoveFavoriteItemCommand.Execute(ViewModel.ItemsSource[adapterPosition]);
            }
        }
    }
}