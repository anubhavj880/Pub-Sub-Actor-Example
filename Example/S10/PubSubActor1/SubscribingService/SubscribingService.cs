using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using ServiceFabric.PubSubActors.SubscriberServices;
using ServiceFabric.PubSubActors.Interfaces;
using DataContracts;
using Microsoft.ServiceFabric.Actors;

namespace SubscribingService
{
    internal sealed class SubscribingService : StatefulService, ISubscriberService
    {
        private const string WellKnownRelayBrokerId = "WellKnownRelayBroker";



        public SubscribingService(StatefulServiceContext context)
            : base(context)
        {


        }
        public SubscribingService(StatefulServiceContext serviceContext, IReliableStateManagerReplica reliableStateManagerReplica) : base(serviceContext, reliableStateManagerReplica)
        {

        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            yield return new ServiceReplicaListener(p => new SubscriberCommunicationListener(this, p), "StatefulSubscriberCommunicationListener");

        }


        protected override async Task OnOpenAsync(ReplicaOpenMode openMode, CancellationToken cancellationToken)
        {
            await TryRegisterAsync();
        }

        private async Task TryRegisterAsync()
        {
            int retries = 0;
            const int maxRetries = 10;
            Thread.Yield();
            while (true)
            {
                try
                {
                    await RegisterAsync();
                    ServiceEventSource.Current.ServiceMessage(this.Context, $"Registered Service:'{nameof(SubscribingService)}' Replica:'{Context.ReplicaId}' as Subscriber.");
                    break;
                }
                catch (Exception ex)
                {
                    if (retries++ < maxRetries)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(500));
                        continue;
                    }
                    ServiceEventSource.Current.ServiceMessage(this.Context, $"Failed to register Service:'{nameof(SubscribingService)}' Replica:'{Context.ReplicaId}' as Subscriber. Error:'{ex}'");
                    break;
                }
            }
        }



        public  Task RegisterAsync()
        {

            //register with BrokerRelayActor:
            return this.RegisterMessageTypeWithRelayBrokerAsync(typeof(PublishedMessageOne), new ActorId(WellKnownRelayBrokerId), null);

        }

        public Task UnregisterAsync()
        {

            //unregister with BrokerRelayActor:
            return this.UnregisterMessageTypeWithRelayBrokerAsync(typeof(PublishedMessageOne), new ActorId(WellKnownRelayBrokerId), null, true);
        }


        public Task ReceiveMessageAsync(MessageWrapper message)
        {
            var payload = this.Deserialize<PublishedMessageOne>(message);
            ServiceEventSource.Current.ServiceMessage(this.Context, $"Received message: {payload.Content1}");
            //TODO: handle message
            return Task.FromResult(true);
        }
    }
}
