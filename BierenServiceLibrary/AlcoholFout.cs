using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BierenServiceLibrary
{
    [DataContract]
    public class AlcoholFout
    {
        public AlcoholFout()
        {
            VerkeerdeParameters = new List<string>();
        }
        [DataMember]
        public List<string> VerkeerdeParameters { get; set; }
    }
}
