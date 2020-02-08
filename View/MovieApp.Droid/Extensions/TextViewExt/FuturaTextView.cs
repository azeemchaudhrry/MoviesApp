using System;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace MovieApp.Droid.Extensions.TextViewExt
{
    [Register("movieapp.droid.extensions.textviewext.FuturaTextView")]
    public class FuturaTextView : TextView
    {
        public FuturaTextView(Context context) : base(context)
        {
        }

        public FuturaTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitTextView(context, attrs);
        }

        public FuturaTextView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            InitTextView(context, attrs);
        }

        public FuturaTextView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            InitTextView(context, attrs);
        }

        protected FuturaTextView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        private void InitTextView(Context context, IAttributeSet attrs)
        {
            TypedArray a = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.FuturaTextView, 0, 0);
            try
            {
                var textStyle = a.GetInteger(Resource.Styleable.FuturaTextView_textStyle, 0);
                if(textStyle == 0)
                {
                    var assetManager = context.ApplicationContext.Assets;
                    var typeFace = Android.Graphics.Typeface.CreateFromAsset(assetManager, "fonts/FuturaPT-Book.ttf");
                    SetTypeface(typeFace, Android.Graphics.TypefaceStyle.Normal);
                }
                else if (textStyle == 1)
                {
                    var assetManager = context.ApplicationContext.Assets;
                    var typeFace = Android.Graphics.Typeface.CreateFromAsset(assetManager, "fonts/FuturaPT-Medium.ttf");
                    SetTypeface(typeFace, Android.Graphics.TypefaceStyle.Normal);
                }
            }
            catch (Exception exp)
            {
                Console.Write(exp);
            }
            finally
            {
                a.Recycle();
            }
        }
    }
}