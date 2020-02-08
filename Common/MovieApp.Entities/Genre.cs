using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieApp.Entities
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }

        public override bool Equals(object obj)
        {
            return Id == ((Genre)obj).Id;
        }
    }

    public class Genres
    {
        [JsonProperty("genres")]
        public List<Genre> Genre { get; set; }
    }
}
