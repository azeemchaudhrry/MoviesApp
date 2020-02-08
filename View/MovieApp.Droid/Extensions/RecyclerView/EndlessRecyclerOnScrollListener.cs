using System;
using Android.Support.V7.Widget;

namespace MovieApp.Droid.Extensions.RecyclerViewExt
{
    public class EndlessRecyclerOnScrollListener : RecyclerView.OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;

        private int previousTotal = 0;
        private bool loading = true;

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            int visibleItemCount = recyclerView.ChildCount;
            int totalItemCount = recyclerView.GetLayoutManager().ItemCount;
            int firstVisibleItem = ((LinearLayoutManager)recyclerView.GetLayoutManager()).FindFirstVisibleItemPosition();

            if (loading)
            {
                if (totalItemCount > previousTotal)
                {
                    loading = false;
                    previousTotal = totalItemCount;
                }
            }
            int visibleThreshold = 5;
            if (!loading && (totalItemCount - visibleItemCount)
                    <= (firstVisibleItem + visibleThreshold))
            {
                LoadMoreEvent(this, null);

                loading = true;
            }
        }
    }
}