using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BierenServiceLibrary
{
    [DataContract]
    public class Bier
    {
        [DataMember]
        public int BierNr { get; set; }
        [DataMember]
        public string Naam { get; set; }
        [DataMember]
        public decimal Alcohol { get; set; }
    }
}
