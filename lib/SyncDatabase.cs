using MySqlConnector;


namespace sino.CollectNews;

public class SyncDatabase
{
    private dbConnector dbTarget;
    private dbConnector dbSource;

    public SyncDatabase()
    {
        this.dbTarget = new dbConnector();
        string[] a = Config.dbTarget;
        this.dbTarget.Connect(Config.dbTarget[0], Config.dbTarget[1], Config.dbTarget[2], Config.dbTarget[3]);
        this.dbSource = new dbConnector();
        this.dbSource.Connect(Config.dbSource[0], Config.dbSource[1], Config.dbSource[2], Config.dbSource[3]);
    }


    public void Sync()
    {
        int i = 0;
        int ii = 0;
        cLog.info("Offset (read tt hist. data ", System.Reflection.MethodBase.GetCurrentMethod().Name);
        while ((i = this.Sync100()) == 100)
        {
            cLog.info($"Read tt News Nr. {ii} to {i + ii} ", System.Reflection.MethodBase.GetCurrentMethod().Name);
            ii = i;
        }
        cLog.info("Offset tt end", System.Reflection.MethodBase.GetCurrentMethod().Name);
    }
    
    public int Sync100()
    {
        int iReturn = 0;
        string LastUpdate = this.dbTarget.readLastUpdateNews(0);
        
        string query =
             @" SELECT providerid , ts , lastupdated , productcodes, headline, body , keywords, source , priority , country " +
              "  FROM quotedb.news " +
              " WHERE lastupdated > '" + LastUpdate + "' " +
              " AND source IN ('DPA-AFX', 'DJ') and provider = 0 " +
              " LIMIT 100";
        
               MySqlDataReader rdr = this.dbSource.query(query);
               cLog.info("Query: " + query , System.Reflection.MethodBase.GetCurrentMethod().Name);
               if (rdr != null)
               {
                   while (rdr.Read())
                   {
                       iReturn++;
                       News news = new News();
                       news.provider = 0;
                       news.providerid = rdr.GetString(0);
                       news.ts = rdr.GetDateTime(1).ToString("yyyy-MM-dd HH:mm:ss");
                       news.lastupdated = rdr.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss");
                       news.productcodes = rdr.GetString(3);
                       news.headline = rdr.GetString(4);
                       news.body = rdr.GetString(5);
                       news.keywords = rdr.GetString(6);
                       news.source = rdr.GetString(7);
                       news.priority = rdr.GetDecimal(8);
                       news.country = rdr.GetString(9);
                       this.dbTarget.insertNews( news);
                   }
               }
               else
               {
                   iReturn = 0;
               }
               rdr.Close();
      
        return iReturn;
    }
    
  
    
}

