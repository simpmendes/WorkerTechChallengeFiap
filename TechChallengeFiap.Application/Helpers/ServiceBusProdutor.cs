using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechChallengeFiap.Application.Helpers
{
    public static class ServiceBusProdutor
    {
        const string conString = "Endpoint=sb://wallaceborges.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JR8CnHrGnaabbT52ZFRgFXLb9iUTLLqjX+ASbK0HfpA=";

        public static async void EnviaMensagem(string mensagem)
        {
            try
            {
                await using var cliente = new ServiceBusClient(conString);
                ServiceBusSender sender = cliente.CreateSender("filapedidos");
                await sender.SendMessageAsync(new ServiceBusMessage(mensagem));
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
