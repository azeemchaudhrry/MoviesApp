using Newtonsoft.Json;
using System.Text;

namespace MovieApp.Entities
{
    public class MovieRequest
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        
        [JsonProperty("query")]
        public string Query { get; set; }

        public string Genres { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if(Page > -1)
            {
                stringBuilder.Append($"page={Page}");
            }
            if (string.IsNullOrEmpty(Query))
            {
                stringBuilder.Append($"query={Query}");
            }
            return stringBuilder.ToString();
        }
    }
}