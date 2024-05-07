using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerInclusaoConsulta
{
    public class WorkerManager : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WorkerManager> _logger;
        private Task _executingTask;
        private CancellationTokenSource _cts;

        public WorkerManager(IServiceProvider serviceProvider, ILogger<WorkerManager> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _executingTask = ExecuteAsync(_cts.Token);

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        private async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var worker = scope.ServiceProvider.GetRequiredService<Worker>();
                    await worker.StartAsync(cancellationToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken); // Intervalo configurável conforme necessário
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            _cts.Cancel();

            await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
