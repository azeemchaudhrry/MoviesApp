using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;

namespace MovieApp.Droid.Extensions.RecyclerViewExt
{
    public class GridSpacingItemDecoration : RecyclerView.ItemDecoration
    {
        private int spanCount;
        private int spacing;
        private bool includeEdge;
        private int headerNum;

        public GridSpacingItemDecoration(int spanCount, int spacing, bool includeEdge, int headerNum)
        {
            this.spanCount = spanCount;
            this.spacing = spacing;
            this.includeEdge = includeEdge;
            this.headerNum = headerNum;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            int position = parent.GetChildAdapterPosition(view) - headerNum; // item position

            if (position >= 0)
            {
                int column = position % spanCount; // item column

                if (includeEdge)
                {
                    outRect.Left = spacing - column * spacing / spanCount; // spacing - column * ((1f / spanCount) * spacing)
                    outRect.Right = (column + 1) * spacing / spanCount; // (column + 1) * ((1f / spanCount) * spacing)

                    if (position < spanCount)
                    { // top edge
                        outRect.Top = spacing;
                    }
                    outRect.Bottom = spacing; // item bottom
                }
                else
                {
                    outRect.Left = column * spacing / spanCount; // column * ((1f / spanCount) * spacing)
                    outRect.Right = spacing - (column + 1) * spacing / spanCount; // spacing - (column + 1) * ((1f /    spanCount) * spacing)
                    if (position >= spanCount)
                    {
                        outRect.Top = spacing; // item top
                    }
                }
            }
            else
            {
                outRect.Left = 0;
                outRect.Right = 0;
                outRect.Top = 0;
                outRect.Bottom = 0;
            }
        }
    }
}