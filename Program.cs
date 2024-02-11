using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Globalization;
using System.Net;
// **************************************
using RestSharp;




namespace sino.CollectNews
{

  

    /// <summary>
    /// 
    /// </summary>
 

    public class Program
    {

        private static bool HellFreezeOver = false;
        
        public static void Main()
        {

            SyncDatabase sync = new SyncDatabase();
            sync.Sync();
 
            cLog.ShowOnConsole = true;
            cLog.Logfile = "log.txt";
            
            ReadBenzingaAPI readBenzingaApi = new ReadBenzingaAPI();
            ReadbenzingaOffset( readBenzingaApi );
/*
            ReadBenzingaWSS rbw = new ReadBenzingaWSS();
            Thread backgroundWorker = new Thread(new ThreadStart(  rbw.ReadFromSocket ));
            backgroundWorker.Start();
*/
            while (!HellFreezeOver)
            {
                cLog.info("Todo: Einlesen der DB tick-ts", System.Reflection.MethodBase.GetCurrentMethod().Name);
/*              sync.Sync(); 
*/
                Thread.Sleep(5000);
            }

        }

        public static void ReadbenzingaOffset( ReadBenzingaAPI readBenzingaApi)
        {
            int i = 0;
            int ii = 0;
            cLog.info("Offset (read hist. data ", System.Reflection.MethodBase.GetCurrentMethod().Name);
            while ((i = readBenzingaApi.readBenzingaFromLastDate()) == 100)
            {
                cLog.info($"Read News Nr. {i} to {ii} ", System.Reflection.MethodBase.GetCurrentMethod().Name);
                ii = i;
            }
            cLog.info("Offset end", System.Reflection.MethodBase.GetCurrentMethod().Name);            
        }
        
        
        
   
     

            
    }
    

}