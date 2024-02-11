namespace sino.CollectNews;

public class News
{
    public int provider = 1;
    public string providerid = "";
    public string ts;
    public string lastupdated;
    public string headline = "";
    public string productcodes = "";
    public string body = "";
    public string keywords = "";
    public string source = "";
    public Decimal priority = 0;
    public string country = "US";

    public News()
    {
    }
    
    public News(BenzingaAPI bapi)
    {
        this.provider = 1;
        this.providerid = bapi.id.ToString();
        this.ts = bapi.created;
        this.lastupdated = bapi.updated;
        this.productcodes = bapi.getChannelAsString();
        this.headline = bapi.title;
        this.body = bapi.body;
        this.keywords = bapi.getStocksAsString();
        this.source = "BENZINGA";
        this.priority = 0;
        this.country = "US";
    }
    
    public News(BenzingaWSS bapi)
    {
        this.provider = 1;
        this.providerid = bapi.WssData.WssContent.id.ToString();
        this.ts = bapi.WssData.WssContent.created_at;
        this.lastupdated = bapi.WssData.WssContent.updated_at;
        this.productcodes = bapi.WssData.WssContent.getChannelsAsString();
        this.headline = bapi.WssData.WssContent.title;
        this.body = bapi.WssData.WssContent.body;
        this.keywords = bapi.WssData.WssContent.getSecuritiesAsString();
        this.source = "BENZINGA";
        this.priority = 0;
        this.country = "US";
    }
}