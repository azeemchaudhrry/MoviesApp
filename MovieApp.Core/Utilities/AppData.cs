using MovieApp.Entities;
using System.Collections.Generic;

namespace MovieApp.Core.Utilities
{
    public static class AppData
    {
        public static List<MovieReview> MovieReviews { get; set; }
        public static List<Movie> Movies { get; set; }
        public static Genres Genres { get; set; }
    }
}
