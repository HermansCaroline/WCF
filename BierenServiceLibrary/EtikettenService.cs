using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BierenServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class EtikettenService : IEtikettenService
    {
        private List<IEtikettenServiceCallBack> callBackChannels = new List<IEtikettenServiceCallBack>();
        public void VerwittigAlsEtikettenVerwijderdZijn()
        {
            callBackChannels.Add(OperationContext.Current.GetCallbackChannel<IEtikettenServiceCallBack>());
        }
        public void StopVerwittigAlsEtikettenVerwijderdZijn()
        {
            callBackChannels.Remove(OperationContext.Current.GetCallbackChannel<IEtikettenServiceCallBack>());
        }
        public void VerwijderEtikettenOuderDan(DateTime datum)
        {
            var verwijderdeBestandsNamen = new List<string>();
            foreach (var bestandsnaam in Directory.GetFiles(@"c:\etiketten"))
            {
                if (File.GetLastWriteTime(bestandsnaam) < datum)
                {
                    File.Delete(bestandsnaam);
                    verwijderdeBestandsNamen.Add(bestandsnaam);
                }
            }
            if (verwijderdeBestandsNamen.Count != 0)
            {
                var callBackChannelsDieNietMeerReageren = new List<IEtikettenServiceCallBack>();
                foreach (var callBackChannel in callBackChannels)
                {
                    try
                    {
                        callBackChannel.EtikettenZijnVerwijderd(verwijderdeBestandsNamen);
                    }
                    catch
                    {
                        callBackChannelsDieNietMeerReageren.Add(callBackChannel);
                    }
                }
                foreach (var callBackChannelDatNietMeerReageert in callBackChannelsDieNietMeerReageren)
                {
                    callBackChannels.Remove(callBackChannelDatNietMeerReageert);
                }
            }
        }
    }
}
