using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using SubscribingActors.Interfaces;
using ServiceFabric.PubSubActors.Interfaces;
using ServiceFabric.PubSubActors.SubscriberActors;
using DataContracts;
using ServiceFabric.PubSubActors.Helpers;

namespace SubscribingActors
{
    [ActorService(Name = nameof(ISubscribingActors))]
    [StatePersistence(StatePersistence.None)]
    internal class SubscribingActors : Actor, ISubscribingActors
    {
        private readonly ISubscriberActorHelper subscriberActorHelper;
        public SubscribingActors(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
            subscriberActorHelper = new SubscriberActorHelper(new BrokerServiceLocator());
        }
       
        public  Task RegisterWithBrokerServiceAsync()
        {
            
            return subscriberActorHelper.RegisterMessageTypeAsync(this, typeof(PublishedMessageOne));
            
        }
        public Task UnregisterWithBrokerServiceAsync()
        {
            return subscriberActorHelper.UnregisterMessageTypeAsync(this, typeof(PublishedMessageOne), true);
           
        }
        public Task ReceiveMessageAsync(MessageWrapper message)
        {
            var payload = this.Deserialize<PublishedMessageOne>(message);
            ActorEventSource.Current.ActorMessage(this, $"Received message: {payload.Content1}");
            return Task.FromResult(true);
        }
    }
}
