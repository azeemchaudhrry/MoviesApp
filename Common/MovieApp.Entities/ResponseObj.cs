using Newtonsoft.Json;

namespace MovieApp.Entities
{
    public class ResponseObj<T>
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("results")]
        public T[] Data { get; set; }
    }
}
