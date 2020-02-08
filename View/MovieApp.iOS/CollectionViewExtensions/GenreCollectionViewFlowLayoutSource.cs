using CoreGraphics;
using Foundation;
using MovieApp.iOS.Utils;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MovieApp.iOS.CollectionViewExtensions
{
    public class GenreCollectionViewFlowLayoutSource : MvxCollectionViewSource, IUICollectionViewDelegateFlowLayout
    {
        public GenreCollectionViewFlowLayoutSource(UICollectionView collectionView) : base(collectionView)
        {
        }

        public GenreCollectionViewFlowLayoutSource(UICollectionView collectionView, NSString defaultCellIdentifier) : base(collectionView, defaultCellIdentifier)
        {
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
        {
            var cell = (GenreCollectionCell)base.GetOrCreateCellFor(collectionView, indexPath, item);
            if (cell is IMvxDataConsumer dataConsumer)
            {
                var dataItem = (string)item;
                dataConsumer.DataContext = dataItem;
                cell.BackgroundColor = UIColorUtils.GetBorderColor();
                cell.UpdateLabelColor(UIColorUtils.GetAppTextLightColor());
            }
            return cell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var item = GetItemAt(indexPath);
            var label = new UILabel();
            label.Text = ((string)item);
            label.Font = FontUtils.GetNormalFont(17);
            label.SizeToFit();
            return new CGSize(label.Frame.Width + 32, label.Frame.Height + 20);
        }
    }
}