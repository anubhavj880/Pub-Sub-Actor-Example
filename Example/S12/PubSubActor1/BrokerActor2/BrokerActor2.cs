using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using BrokerActor2.Interfaces;
using ServiceFabric.PubSubActors;
using ServiceFabric.PubSubActors.Interfaces;

namespace BrokerActor2
{

    [ActorService(Name = nameof(IBrokerActor))]
    internal class BrokerActor2 : BrokerActor, IBrokerActor2
    {
    
        public BrokerActor2(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {

            ActorEventSourceMessageCallback = message => ActorEventSource.Current.ActorMessage(this, message + "=================+++++++++++++++++==========");
        }
                  
        
    }
}
