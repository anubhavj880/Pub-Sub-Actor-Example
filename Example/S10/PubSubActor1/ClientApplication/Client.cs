using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using PublishingActor.Interfaces;

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
            var applicationName = "fabric:/PubSubActor1";

            
            var pubActor = GetPublishingActor(applicationName);

          
       
            while (true) {

                Console.WriteLine("Hit 1 to send a message one to the BrokerActor, using an Actor.");
                Console.WriteLine("Hit escape to exit.");
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        {
                           
                            pubActor.PublishMessageOneAsync().GetAwaiter().GetResult();
                            Console.WriteLine("Sent message one from Actor using Broker Actor!");
                        }
                        break;

                    case ConsoleKey.Escape:
                        return;
                }

            }
            

           
          
        }
        private static IPublishingActor GetPublishingActor(string applicationName)
        {
            IPublishingActor pubActor = null;
            var actorId = new ActorId("PubActor");

            while (pubActor == null)
            {
                try
                {
                    pubActor = ActorProxy.Create<IPublishingActor>(actorId, applicationName);
                   
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
