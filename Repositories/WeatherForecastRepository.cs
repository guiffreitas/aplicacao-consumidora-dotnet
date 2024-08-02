using System.Text.Json;

namespace aplicacao_consumidora_dotnet.Repositories
{
    public class WeatherForecastRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecast(string accessToken)
        {
            var client = _httpClientFactory.CreateClient("ApiServidora");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await client.GetAsync("WeatherForecast");

            var responseObject = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(responseObject);
        }
    }
}
