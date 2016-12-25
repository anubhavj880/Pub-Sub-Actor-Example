using DataContracts;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running the program");
            


            var pubService = GetPublishingService(new Uri("fabric:/PubSubActor1/PublishingStatelessService"));



            while (true)
            {

                Console.WriteLine("Hit 1 to send a message one to the BrokerActor, using an Service.");
                Console.WriteLine("Hit escape to exit.");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        {

                            pubService.PublishMessageOneAsync().GetAwaiter().GetResult();
                            Console.WriteLine("Sent message one from Service using Broker Actor!");
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                }

            }




        }
        private static IPublishingStatelessService GetPublishingService(Uri serviceName)
        {
            IPublishingStatelessService pubService = null;

            while (pubService == null)
            {
                try
                {
                    pubService = ServiceProxy.Create<IPublishingStatelessService>(serviceName);
                }
                catch
                {
                    Thread.Sleep(200);
                }
            }
            return pubService;
        }

    }
}
