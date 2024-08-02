using aplicacao_consumidora_dotnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aplicacao_consumidora_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerWeatherForecastController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ConsumerWeatherForecastController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var forecastRepository = new WeatherForecastRepository(_httpClientFactory);

            var accessTokenRepository = new AccessTokenRepository(_configuration);

            var accessToken = await accessTokenRepository.GetAuthToken();

            var result = await forecastRepository.GetForecast(accessToken);

            return result;
        }
    }
}
