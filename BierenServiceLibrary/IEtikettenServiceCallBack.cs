using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BierenServiceLibrary
{
    public interface IEtikettenServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void EtikettenZijnVerwijderd(List<string> etiketten);
    }
}
