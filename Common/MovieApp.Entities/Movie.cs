using MovieApp.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Entities
{
    public class Movie
    {
        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonIgnore]
        public string Year 
        { 
            get 
            { 
                if(DateTime.TryParse(ReleaseDate, out DateTime dateTime))
                {
                    return $"{dateTime.Year}";
                }
                return ReleaseDate;
            } 
        }

        [JsonIgnore]
        public string PosterUrl 
        { 
            get 
            {
                return $"{Configurations.ImageBaseUrl}{PosterPath}";
            } 
        }

        [JsonIgnore]
        public bool IsFavorite { get; set; }

        [JsonIgnore]
        public string[] Genres { get; set; }

        [JsonIgnore]
        public string GenreString 
        { 
            get 
            { 
                if(Genres.Length > 1)
                {
                    return string.Join(",", Genres[0], Genres[1]);
                }
                return Genres[0];
            } 
        }
        public override bool Equals(object obj)
        {
            return Id.Equals(((Movie)obj).Id);
        }

        public void AddGenres(List<Genre> genres)
        {
            Genres = new string[GenreIds.Length];
            for (int i = 0; i < GenreIds.Length; i++)
            {
                Genres[i] = genres.First(x => x.Id.Equals(GenreIds[i])).Name;
            }
        }
    }
}
