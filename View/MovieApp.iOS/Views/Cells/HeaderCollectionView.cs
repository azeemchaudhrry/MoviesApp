// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using MovieApp.Core.Contracts;
using MovieApp.Entities;
using MovieApp.iOS.CollectionViewExtensions;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace MovieApp.iOS
{
	public partial class HeaderCollectionView : MvxCollectionReusableView
	{
		public static readonly NSString Key = new NSString("HeaderCollectionView");
		GenreCollectionViewSource collectionViewSource;
		IItemSelectionChange<Genre> _genreSelection;

		public IItemSelectionChange<Genre> GenreSelection 
		{
			get { return _genreSelection; }
			set
			{
				_genreSelection = value;
				if(collectionViewSource != null)
				{
					collectionViewSource.GenreSelection = value;
				}
			}
		}
		
		public HeaderCollectionView (IntPtr handle) : base (handle)
		{
			this.DelayBind(() => 
			{
                var set = this.CreateBindingSet<HeaderCollectionView, Genres>();
				set.Bind(collectionViewSource).For(p => p.ItemsSource).To(vm => vm.Genre);
				set.Apply();
			});
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

            collectionView.AllowsSelection = true;
            collectionView.AllowsMultipleSelection = false;
            collectionView.RegisterNibForCell(GenreCollectionCell.Nib, GenreCollectionCell.Key);
			collectionViewSource = new GenreCollectionViewSource(collectionView, GenreCollectionCell.Key);
			collectionView.Source = collectionViewSource;
			collectionView.Delegate = collectionViewSource;
		}
	}
}