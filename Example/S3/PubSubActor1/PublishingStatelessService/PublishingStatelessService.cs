using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using DataContracts;
using ServiceFabric.PubSubActors.Helpers;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;

namespace PublishingStatelessService
{
 
    internal sealed class PublishingStatelessService : StatelessService, IPublishingStatelessService
    {

        private readonly IPublisherServiceHelper _publisherServiceHelper;

        public PublishingStatelessService(StatelessServiceContext context)
            : base(context)
        {

            _publisherServiceHelper = new PublisherServiceHelper(new BrokerServiceLocator());
        }

        public async Task<string> PublishMessageOneAsync()
        {
            ServiceEventSource.Current.ServiceMessage(this.Context, "==============Publishing Message================");

            await _publisherServiceHelper.PublishMessageAsync(this, new PublishedMessageOne
            {
                Content1 = "Hello PubSub World, from Service, using Broker Service!"
            });

            return "Message published to broker service";
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            yield return new ServiceInstanceListener(context => new FabricTransportServiceRemotingListener(context, this), "StatelessFabricTransportServiceRemotingListener");
        }

 
    }
}
