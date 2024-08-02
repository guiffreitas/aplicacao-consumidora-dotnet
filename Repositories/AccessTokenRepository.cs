using Microsoft.Identity.Client;

namespace aplicacao_consumidora_dotnet.Repositories
{
    public class AccessTokenRepository
    {
        private readonly IConfiguration _configuration;

        public AccessTokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetAuthToken()
        {
            //Id do diretório dos aplicativos no Entra ID
            var tenantId = _configuration.GetValue<string>("EntraID:TenantID");

            //Secret do aplicativo da API consumidor
            var secret = _configuration.GetValue<string>("EntraID:Consumidor:Secret");

            //Id do aplicativo da API consumidora
            var clientIdConsumidor = _configuration.GetValue<string>("EntraID:Consumidor:Id");

            //Id do aplicativo da API servidora
            var clientIdServidor = _configuration.GetValue<string>("EntraID:Servidor:Id");

            var authorityUrl = $"https://login.microsoftonline.com/{tenantId}";

            var scopes = new string[] { $"{clientIdServidor}/.default" };

            IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create(clientIdConsumidor)
                .WithClientSecret(secret)
                .WithAuthority(new Uri(authorityUrl))
                .Build();

            AuthenticationResult result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

            return result.AccessToken;
        }
    }
}
