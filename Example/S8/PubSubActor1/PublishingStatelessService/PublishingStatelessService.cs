using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using ServiceFabric.PubSubActors.PublisherServices;
using DataContracts;

namespace PublishingStatelessService
{

    internal sealed class PublishingStatelessService : StatelessService, IPublishingStatelessService
    {

        

        public PublishingStatelessService(StatelessServiceContext context)
            : base(context)
        {

          
        }

        public async Task<string> PublishMessageOneAsync()
        {
            ServiceEventSource.Current.ServiceMessage(this.Context, "==============Publishing Message================");

            await this.PublishMessageAsync(new PublishedMessageOne
            {
                Content1 = "Hello PubSub World, from Service, using Broker Actor!"
            });

            return "Message published to broker actor";
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            yield return new ServiceInstanceListener(context => new FabricTransportServiceRemotingListener(context, this), "StatelessFabricTransportServiceRemotingListener");
        }


    }
}
