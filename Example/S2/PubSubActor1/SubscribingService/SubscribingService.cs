using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ServiceFabric.PubSubActors.SubscriberServices;
using ServiceFabric.PubSubActors.Helpers;
using ServiceFabric.PubSubActors.Interfaces;
using Microsoft.ServiceFabric.Data;
using DataContracts;

namespace SubscribingService
{

    internal sealed class SubscribingService : StatefulService, ISubscriberService
    {

        private readonly ISubscriberServiceHelper subscriberServiceHelper;

        public SubscribingService(StatefulServiceContext context)
            : base(context)
        {
            subscriberServiceHelper = new SubscriberServiceHelper(new BrokerServiceLocator());

        }
        public SubscribingService(StatefulServiceContext serviceContext, IReliableStateManagerReplica reliableStateManagerReplica) : base(serviceContext, reliableStateManagerReplica)
        {
            subscriberServiceHelper = new SubscriberServiceHelper(new BrokerServiceLocator());
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



        public Task RegisterAsync()
        {
           
            //register with BrokerService:
            return subscriberServiceHelper.RegisterMessageTypeAsync(this, typeof(PublishedMessageOne));

        }

        public Task UnregisterAsync()
        {

            //unregister with BrokerService:
            return subscriberServiceHelper.UnregisterMessageTypeAsync(this, typeof(PublishedMessageOne), true);
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
