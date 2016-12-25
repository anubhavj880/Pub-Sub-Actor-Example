using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PublishingActor.Interfaces;
using ServiceFabric.PubSubActors;
using ServiceFabric.PubSubActors.PublisherActors;
using DataContracts;
using ServiceFabric.PubSubActors.Helpers;

namespace PublishingActor
{
    [StatePersistence(StatePersistence.None)]
    internal class PublishingActor : Actor, IPublishingActor
    {
               public PublishingActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
       
        }
        async Task<string> IPublishingActor.PublishMessageOneAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "==============Publishing Message================");
            Console.WriteLine("Inside Publisher method");
            await this.PublishMessageAsync(new PublishedMessageOne {
                Content1 = "Hello PubSub World, from Actor, using Broker Actor!"
            });

            return "Message published to broker actor";
        }
    }
}