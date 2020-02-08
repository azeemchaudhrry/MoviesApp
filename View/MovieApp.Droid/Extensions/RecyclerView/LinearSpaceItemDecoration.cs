using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;

namespace MovieApp.Droid.Extensions.RecyclerViewExt
{
    /// <summary>
    /// vertical spacing between item in horizontal linear layout
    /// </summary>
    public class LinearSpaceItemDecoration : RecyclerView.ItemDecoration
    {
        private int verticalSpace;
        private int horizontalSpace;
        public LinearSpaceItemDecoration(int _verticalSpace, int _horizontalSpace)
        {
            verticalSpace = _verticalSpace;
            horizontalSpace = _horizontalSpace;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            outRect.Right = verticalSpace;
            outRect.Top = horizontalSpace;
            outRect.Bottom = horizontalSpace;

            if(parent.GetChildAdapterPosition(view) == 0)
            {
                outRect.Left = verticalSpace;
            }
        }
    }
}