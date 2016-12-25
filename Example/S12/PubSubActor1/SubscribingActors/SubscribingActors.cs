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
        private const string WellKnownRelayBrokerId = "WellKnownRelayBroker";

        public SubscribingActors(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {

        }

        public Task RegisterWithRelayAsync()
        {
            return this.RegisterMessageTypeWithRelayBrokerAsync(typeof(PublishedMessageOne), new ActorId(WellKnownRelayBrokerId), null);
        }

        public Task UnregisterWithRelayAsync()
        {
            return this.UnregisterMessageTypeWithRelayBrokerAsync(typeof(PublishedMessageOne), new ActorId(WellKnownRelayBrokerId), null, true);
        }


        public Task ReceiveMessageAsync(MessageWrapper message)
        {
            var payload = this.Deserialize<PublishedMessageOne>(message);
            ActorEventSource.Current.ActorMessage(this, $"Received message: {payload.Content1}");
            return Task.FromResult(true);
        }


    }
}
