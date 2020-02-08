using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using System;

namespace MovieApp.Droid.Extensions
{
    public static class FontUtils
    {
        public static Typeface GetNormalFont(Context context)
        {
            var assetManager = context.ApplicationContext.Assets;
            var typeFace = Android.Graphics.Typeface.CreateFromAsset(assetManager, "fonts/FuturaPT-Book.ttf");
            return typeFace;
        }

        public static Typeface GetBoldFont(Context context)
        {
            var assetManager = context.ApplicationContext.Assets;
            var typeFace = Typeface.CreateFromAsset(assetManager, "fonts/FuturaPT-Medium.ttf");
            return typeFace;
        }
    }

    public class CustomTypeFaceSpan : TypefaceSpan
    {
        private Typeface newType;
        private Color color;

        public CustomTypeFaceSpan(string family, Typeface type, Color color) : base(family)
        {
            this.color = color;
            newType = type;
        }

        protected CustomTypeFaceSpan(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void UpdateDrawState(TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ApplyCustomFonts(ds, newType);
        }

        public override void UpdateMeasureState(TextPaint paint)
        {
            base.UpdateMeasureState(paint);
            ApplyCustomFonts(paint, newType);
        }

        public override int SpanTypeId => base.SpanTypeId;

        private void ApplyCustomFonts(Paint paint, Typeface tf)
        {
            paint.Color = color;
            paint.SetTypeface(tf);
        }
    }
}