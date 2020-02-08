using MovieApp.Constants;
using MovieApp.Contracts;
using MovieApp.Entities;
using Refit;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly IMovieAPI _movieApi;
        public MovieService()
        {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri(Configurations.ApiBaseUrl) };
            _movieApi = RestService.For<IMovieAPI>(_httpClient);
        } 

        public Task<ResponseObj<Movie>> DiscoverMovies(MovieRequest request)
        {
            return _movieApi.Discover(Configurations.ApiKey, request.Page, request.Genres);
        }

        public Task<ResponseObj<Movie>> SearchMovies(MovieRequest request)
        {
            return _movieApi.Search(Configurations.ApiKey, request.Page, request.Query);
        }

        public Task<Genres> GetGenres()
        {
            return _movieApi.GetGenres(Configurations.ApiKey);
        }
    }

    public interface IMovieAPI
    {
        [Get("/3/discover/movie?api_key={apiKey}&language=en-US&page={pageNumber}&sort_by=popularity.desc&with_genres={genres}")]
        Task<ResponseObj<Movie>> Discover(string apiKey, int pageNumber, string genres);

        [Get("/3/search/movie?api_key={apiKey}&language=en-US&page={pageNumber}&query={query}")]
        Task<ResponseObj<Movie>> Search(string apiKey, int pageNumber, string query);
        
        [Get("/3/genre/movie/list?api_key={apiKey}&language=en-US")]
        Task<Genres> GetGenres(string apiKey);
    }

    [DebuggerStepThrough]
    public class HttpClientDiagnosticsHandler : DelegatingHandler
    {
        public HttpClientDiagnosticsHandler(HttpMessageHandler innerHandler) : base(innerHandler)
        {
        }

        public HttpClientDiagnosticsHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var totalElapsedTime = Stopwatch.StartNew();

            if (request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            var responseElapsedTime = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            responseElapsedTime.Stop();

            totalElapsedTime.Stop();

            return response;
        }
    }
}
