using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using PublishingActor.Interfaces;
using SubscribingActors.Interfaces;
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

          
            RegisterSubscribers(applicationName);
       
            while (true) {

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
        private static void RegisterSubscribers(string applicationName)
        {
            var actorId = new ActorId("SubActor");
            ISubscribingActors subActor = null;
            try
            {
                subActor = ActorProxy.Create<ISubscribingActors>(actorId, applicationName, nameof(ISubscribingActors));
                
                subActor.RegisterWithBrokerServiceAsync().GetAwaiter().GetResult();
                
            }
            catch
            {
                Console.WriteLine("exception in the RegistringSubscribing");
                subActor = null;
                Thread.Sleep(200);
            }
        }
    }
}
