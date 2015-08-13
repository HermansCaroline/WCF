using ConsoleBierenClient.BierenServiceReference;
using ConsoleBierenClient.EtikettenServiceReference;
using ConsoleBierenClient.RadenServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBierenClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //var etikettenServiceCallBack = new EtikettenServiceCallBack();
            //using (var etikettenServiceClient =
            //    new EtikettenServiceClient(new InstanceContext(etikettenServiceCallBack)))
            //{
            //    etikettenServiceClient.VerwittigAlsEtikettenVerwijderdZijn();
            //    Console.Write("Datum tijd (druk s om te stoppen):");
            //    var antwoord = Console.ReadLine();
            //    while (antwoord != "s")
            //    {
            //        var datum = DateTime.Parse(antwoord);
            //        etikettenServiceClient.VerwijderEtikettenOuderDan(datum);
            //        Console.Write("Datum tijd (druk s om te stoppen):");
            //        antwoord = Console.ReadLine();
            //    }
            //    etikettenServiceClient.StopVerwittigAlsEtikettenVerwijderdZijn();
            //}

            //using (var radenServiceClient = new RadenServiceClient())
            //{
            //    Console.WriteLine("Raad het alcohol% van Duvel");
            //    var alcohol = decimal.Parse(Console.ReadLine());
            //    var antwoord = radenServiceClient.RaadDuvelAlcohol(alcohol);
            //    while (antwoord.Hint != Hint.Correct)
            //    {
            //        Console.WriteLine("{0}, {1} beurt(en)", antwoord.Hint, antwoord.Beurten);
            //        alcohol = decimal.Parse(Console.ReadLine());
            //        antwoord = radenServiceClient.RaadDuvelAlcohol(alcohol);
            //    }
            //    Console.WriteLine("{0}, {1} beur(en)", antwoord.Hint, antwoord.Beurten);
            //    Console.WriteLine("Beste score:{0}", antwoord.BesteScore);
            //}
            //Console.ReadLine();

            var nogEenKeer = true;
            while (nogEenKeer)
            {
                var bierenServiceClient = new BierenServiceClient("httpBieren");
                try
                {
                    //Console.WriteLine("Aantal bieren: {0}", bierenServiceClient.GetTotaalAantalBieren());
                    Console.Write("Van alcohol: ");
                    var van = decimal.Parse(Console.ReadLine());
                    Console.Write("Tot alcohol: ");
                    var tot = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Aantal bieren: {0}", bierenServiceClient.GetAantalBierenTussenAlcohol(van, tot));
                    nogEenKeer = false;
                    //Console.Write("Woord: ");
                    //var woord = Console.ReadLine();
                    //var bieren = bierenServiceClient.GetBierenMetWoord(woord);
                    //foreach (var bier in bieren)
                    //{
                    //    Console.WriteLine("{0} {1} {2}%", bier.BierNr, bier.Naam, bier.Alcohol);
                    //}
                    //Console.WriteLine();
                    //foreach (var bier in bierenServiceClient.GetStrafsteBieren())
                    //{
                    //    Console.WriteLine("{0} {1} {2}%", bier.BierNr, bier.Naam, bier.Alcohol);
                    //}
                }
                catch (FaultException<AlcoholFout> ex)
                {
                    Console.WriteLine(ex.Reason);
                    Console.Write("Verkeerde invoer:");
                    foreach(var verkeerdeParameter in ex.Detail.VerkeerdeParameters){
                        Console.Write(verkeerdeParameter);
                        Console.Write(' ');
                    }
                    Console.WriteLine();
                    Console.WriteLine("Nog een keer proberen (j/n): ");
                    nogEenKeer = Console.ReadLine() == "j";
                }
                catch (FaultException ex)
                {
                    Console.WriteLine("Technisch probleem bij het ophalen van de bieren");
                    nogEenKeer = false;
                }
                finally
                {
                    if (bierenServiceClient.State == CommunicationState.Faulted)
                        bierenServiceClient.Abort();
                    else
                        bierenServiceClient.Close();
                }
            }
        }
    }
}
