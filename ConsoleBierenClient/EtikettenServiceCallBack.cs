using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBierenClient.EtikettenServiceReference;

namespace ConsoleBierenClient
{
    class EtikettenServiceCallBack : IEtikettenServiceCallback
    {
        public void EtikettenZijnVerwijderd(string[] etiketten)
        {
            Console.WriteLine("Volgende etiketten zijn verwijderd");
            foreach (var etiket in etiketten)
            {
                Console.WriteLine(etiket);
            }
        }
    }
}
