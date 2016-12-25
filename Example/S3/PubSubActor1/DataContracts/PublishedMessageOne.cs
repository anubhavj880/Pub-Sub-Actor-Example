using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataContracts
{
    [DataContract]
    public class PublishedMessageOne
    {
        [DataMember]
        public string Content1 { get; set; }
    }
}
