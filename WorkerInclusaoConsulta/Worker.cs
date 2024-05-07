using Azure.Messaging.ServiceBus;
using System.Text.Json;
using TechChallengeFiap.Application.DTOs;
using TechChallengeFiap.Application.Interfaces;

namespace WorkerInclusaoConsulta
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ServiceBusClient _client;
        private ServiceBusProcessor _processor;
        //private readonly ICotacoesAcoesService _cotacaoAcoesService;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _client = new ServiceBusClient("Endpoint=sb://masstransitsimp.servicebus.windows.net/;SharedAccessKeyName=acesso;SharedAccessKey=g4erc2R5EkKEJ/4BNB1t0l7/LyMNNhL+r+ASbBIAdaA=;EntityPath=fila");
            _processor = _client.CreateProcessor("fila", new ServiceBusProcessorOptions());
            //_cotacaoAcoesService = cotacoesAcoesService;
            _scopeFactory = scopeFactory;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var cotacoesAcoesService = scope.ServiceProvider.GetRequiredService<ICotacoesAcoesService>();
                var messageBody = args.Message.Body.ToString();
                var pedidoDto = JsonSerializer.Deserialize<PedidoAcaoDTO>(messageBody);
                if (pedidoDto == null) throw new InvalidOperationException("Pedido DTO deserializado é nulo.");

                var resultado = await cotacoesAcoesService.EfetuaPedido(pedidoDto);
                _logger.LogInformation(resultado);
                await args.CompleteMessageAsync(args.Message);
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError($"Erro no processador do Service Bus: {args.Exception.ToString()}");
            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await _processor.StopProcessingAsync(stoppingToken);
            await _client.DisposeAsync();
        }
    }
}

