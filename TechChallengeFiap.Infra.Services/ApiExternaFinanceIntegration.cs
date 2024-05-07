using Microsoft.Extensions.Logging;
using TechChallengeFiap.Domain.Interfaces;

namespace TechChallengeFiap.Infra.Services
{
    public class ApiExternaFinanceIntegration: IApiExternaFinanceIntegration
    {
        private readonly ILogger<ApiExternaFinanceIntegration> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ApiExternaFinanceIntegration(ILogger<ApiExternaFinanceIntegration> logger, HttpClient httpClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://www.alphavantage.co/");
            _apiKey = Environment.GetEnvironmentVariable("ALPHA_VANTAGE_API_KEY") ?? throw new InvalidOperationException("Variável de ambiente ALPHA_VANTAGE_API_KEY não definida.");
        }
        public async Task<string> GetCotacaoBySimbol(string symbol)
        {
            try
            {
                string url = $"query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Falha ao obter cotação para {symbol}. Código de resposta: {response.StatusCode}");
                    throw new HttpRequestException($"Erro ao obter cotação da ação {symbol}.");
                }

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Cotação da ação {symbol}: {content}");
                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar cotação da ação {symbol}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetTopGainerAndLosers()
        {
            try
            {
                string url = $"query?function=TOP_GAINERS_LOSERS&apikey={_apiKey}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Falha ao obter maiores cotações do dia. Código de resposta: {response.StatusCode}");
                    throw new HttpRequestException("Erro ao obter as 10 maiores cotações do dia.");
                }

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Consulta das 10 ações que mais subiram no dia.");
                return content;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar as 10 ações que mais subiram no dia: {ex.Message}");
                throw;
            }
        }
    }
}
