using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using PubkSubRelayActor.Interfaces;
using ServiceFabric.PubSubActors.Interfaces;
using ServiceFabric.PubSubActors;

namespace PubkSubRelayActor
{
    [StatePersistence(StatePersistence.None)]
    [ActorService(Name = nameof(IRelayBrokerActor))]
    internal class PubkSubRelayActor : RelayBrokerActor, IPubkSubRelayActor
    {
        public PubkSubRelayActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
            ActorEventSourceMessageCallback = message => ActorEventSource.Current.ActorMessage(this, message+"@@@@@@@@@@@@@@@@#######RELAY########@@@@@@@@@@@@@@@@@@");
        }
    }
}
