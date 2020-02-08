using CoreGraphics;
using Foundation;
using MovieApp.Core.Contracts;
using MovieApp.Entities;
using MovieApp.iOS.Utils;
using MvvmCross.Base;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MovieApp.iOS.CollectionViewExtensions
{
	public class GenreCollectionViewSource : MvxCollectionViewSource, IUICollectionViewDelegateFlowLayout
	{
		public IItemSelectionChange<Genre> GenreSelection { get; set; }

		public GenreCollectionViewSource(UICollectionView collectionView) : base(collectionView)
		{
		}
		public GenreCollectionViewSource(UICollectionView collectionView, NSString defaultCellIdentifier) :
			base(collectionView, defaultCellIdentifier)
		{
		}

		protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
		{
			var cell = (GenreCollectionCell)base.GetOrCreateCellFor(collectionView, indexPath, item);
			if (cell is IMvxDataConsumer dataConsumer)
			{
				var dataItem = (Genre)item;
				dataConsumer.DataContext = dataItem;
				if (dataItem.IsSelected)
				{
					cell.BackgroundColor = UIColorUtils.GetAppThemeColor();
					cell.UpdateLabelColor(UIColor.White);
                    cell.Selected = true;
					cell.Layer.BorderWidth = 0f;
					cell.Layer.ShadowColor = UIColorUtils.GetAppThemeColor().CGColor;
					cell.Layer.ShadowOffset = new CGSize(1.0, 2.0);
					cell.Layer.ShadowRadius = 1.5f;
					cell.Layer.ShadowOpacity = 1.0f;
					cell.Layer.MasksToBounds = false;
				}
				else
				{
					cell.BackgroundColor = UIColorUtils.GetBorderColor();
					cell.UpdateLabelColor(UIColorUtils.GetAppTextLightColor());
					cell.Layer.ShadowOffset = new CGSize(0.0, 0.0);
					cell.Layer.ShadowRadius = 0.0f;
					cell.Layer.ShadowOpacity = 0.0f;
				}
			}
			return cell;
		}

		[Export("collectionView:layout:sizeForItemAtIndexPath:")]
		public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
		{
			var item = GetItemAt(indexPath);
			var label = new UILabel();
			label.Text = ((Genre)item).Name;
			label.Font = FontUtils.GetNormalFont(17);
			label.SizeToFit();
			return new CGSize(label.Frame.Width + 32, label.Frame.Height + 20);
		}


        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var item = (Genre)GetItemAt(indexPath);
            var firstItem = ItemsSource?.ElementAt(0);
            if(firstItem != null && firstItem != item)
            {
                (firstItem as Genre).IsSelected = false;
            }
            else
            {
                (firstItem as Genre).IsSelected = true;
            }
			GenreSelection.ItemSelectionChanged(item);
			collectionView.ReloadData();
		}
	}
}