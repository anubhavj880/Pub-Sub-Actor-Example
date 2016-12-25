using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using PublishingActor.Interfaces;
using SubscribingService;
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


            var pubActor = GetPublishingActor();


           
            while (true)
            {

                Console.WriteLine("Hit 1 to send a message one to the BrokerService, using an Actor.");
                Console.WriteLine("Hit escape to exit.");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        {

                            pubActor.PublishMessageOneAsync().GetAwaiter().GetResult();
                            Console.WriteLine("Sent message one from Actor using Broker Service!");
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                }

            }




        }
        private static IPublishingActor GetPublishingActor()
        {
            IPublishingActor pubActor = null;
            var actorId = new ActorId("PubActor");

            while (pubActor == null)
            {
                try
                {
                    pubActor = ActorProxy.Create<IPublishingActor>(actorId, "fabric:/PubSubActor1");

                }
                catch
                {
                    Console.WriteLine("excetion in the Ipublishing actor");
                    Thread.Sleep(200);
                }
            }
            return pubActor;
        }
        
    }
}
