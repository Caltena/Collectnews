using System;
using System.Threading;
using System.Xml;
using Websocket.Client;
using Newtonsoft.Json;



namespace sino.CollectNews;

public class ReadBenzingaWSS

{
    private dbConnector dbC;
    
    /// <summary>
    /// 
    /// </summary>
    public void Deconstruct()
    {
        dbC.Close();
    }



    public ReadBenzingaWSS()
    {

       
    }



    public void ReadFromSocket()
    {
        dbC = new dbConnector();
        dbC.Connect();
        
        var exitEvent = new ManualResetEvent(false);
        var url = new Uri("wss://api.benzinga.com/api/v1/news/stream?token=587a90035a44413fa90169ac3ef7470b");
       
        using (var client = new WebsocketClient(url))
        {
            client.ReconnectTimeout = TimeSpan.FromSeconds(30);
            client.ReconnectionHappened.Subscribe(info =>
            {
                cLog.info($"Reconnection happened, type: {info.Type}", System.Reflection.MethodBase.GetCurrentMethod().Name);
            });

            
             client.MessageReceived.Subscribe(msg =>
            {
                cLog.info($"Message received: {msg}", System.Reflection.MethodBase.GetCurrentMethod().Name);
                BenzingaWSS benzingaWS = JsonConvert.DeserializeObject<BenzingaWSS>(msg.ToString());
                dbC.insertNews(new News(benzingaWS));
            });


            cLog.info("Start Task ... ", System.Reflection.MethodBase.GetCurrentMethod().Name);
            client.Start();
            cLog.info("Started ", System.Reflection.MethodBase.GetCurrentMethod().Name);

            
            Console.WriteLine("Client Start");
            exitEvent.WaitOne();
        }
    }
    
    
    
    
    
    
    
    private static async Task StartSendingPing(IWebsocketClient client)
    {
        while (true)
        {
            await Task.Delay(1000);

            if (!client.IsRunning)
                continue;

            client.Send("ping");
        }
    }
}