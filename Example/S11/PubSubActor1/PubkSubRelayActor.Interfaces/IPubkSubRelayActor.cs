using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using ServiceFabric.PubSubActors.Interfaces;

namespace PubkSubRelayActor.Interfaces
{
    public interface IPubkSubRelayActor : IRelayBrokerActor, IActor
    {


    }
}
