using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ServiceFabric.PubSubActors;

namespace BrokerService1
{
    internal sealed class BrokerService1 : BrokerService
    {
        public BrokerService1(StatefulServiceContext context)
            : base(context)
        {
            
            ServiceEventSourceMessageCallback = (message) => ServiceEventSource.Current.ServiceMessage(Context, message+"=================+++++++++++++++++==========");
        }
    }
}
