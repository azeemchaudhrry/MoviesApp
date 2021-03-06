// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using MovieApp.Entities;
using MovieApp.iOS.Utils;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MovieApp.iOS
{
	public partial class GenreCollectionCell : MvxCollectionViewCell
	{
		public static readonly NSString Key = new NSString("GenreCollectionCell");
        public static readonly UINib Nib;

        static GenreCollectionCell()
        {
            Nib = UINib.FromName("GenreCollectionCell", NSBundle.MainBundle);
        }

        public static GenreCollectionCell Create()
        {
            return (GenreCollectionCell)Nib.Instantiate(null, null)[0];
        }

        public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			Layer.CornerRadius = Frame.Height / 3;
			Layer.MasksToBounds = false;

            titleLabel.Font = FontUtils.GetNormalFont(17);
        }

		public GenreCollectionCell (IntPtr handle) : base (handle)
		{
			this.DelayBind(() => 
			{
                if(DataContext is Genre)
                {
                    var set = this.CreateBindingSet<GenreCollectionCell, Genre>();
                    set.Bind(titleLabel).For(prop => prop.Text).To(vm => vm.Name);
                    set.Apply();
                }
                else
                {
                    titleLabel.Text = DataContext as string;
                }
			});
		}

		public void UpdateLabelColor(UIColor color)
		{
			titleLabel.TextColor = color;
		}

		//public  void UpdateShadow()
		//{
		//	ContentView.Layer.BorderWidth = 0f;
		//	ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
		//	ContentView.Layer.ShadowColor = UIColor.Gray.CGColor;
		//	ContentView.Layer.ShadowOffset = new CoreGraphics.CGSize(2.0, 4.0);
		//	ContentView.Layer.ShadowRadius = 2.0f;
		//	ContentView.Layer.ShadowOpacity = 1.0f;
		//	ContentView.Layer.MasksToBounds = false;
		//}
	}
}
