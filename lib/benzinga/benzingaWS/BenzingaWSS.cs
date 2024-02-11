using System.Globalization;

namespace sino.CollectNews;

public class BenzingaWSS
{
    public string api_version { get; set; }
    public string kind { get; set; }
    public cBenzingaWSS_Data WssData { get; set; }   
}

public class cBenzingaWSS_Data
{
    public string action { get; set; }
    public int id { get; set; }
    public cBenzingaWSS_Content WssContent { get; set; }
    public DateTime timestamp { get; set; }
}


public class cBenzingaWSS_Content
{
    public int id { get; set; }
    
    public int revision_id { get; set; }
    
    public string type { get; set; }
    
    private string? _title;
    public string title {
        get
        {
            if (String.IsNullOrEmpty(this.body))
                return _title.ToUpper();
            else
                return _title;
        }
        set { _title = value; }

    } 
    public string body { get; set; }
    
    public List<string> authors { get; set; }
    
    public string teaser { get; set; }
    public List<string> tags { get; set; }
    public List<BenzingaSecurity> securities { get; set; }
    public List<string> channels { get; set; }
    
    public List<BenzingaImage> image { get; set; }

    private string? _created_at;
    public string created_at
    {
        get { return getGMT(_created_at);}
        set { _created_at = value; }
    }

    private string? _updated_at; 
    public string updated_at 
    {
        get { return getGMT(_updated_at);}
        set { _updated_at = value; }
    }     
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string getSecurities()
    {   string sReturn = "";
        foreach (BenzingaSecurity cC in securities)
            sReturn +=  "<A HREF='http://symb/" +  cC.symbol.Trim()  + "' >" + cC.symbol.Trim() + "</A>" + " , ";
        return sReturn.Trim( new Char[] { ' ', ',', '.' } );
    }
    public string getSecuritiesAsString()
    {   string sReturn = "";
        foreach (BenzingaSecurity cC in securities)
            sReturn +=   cC.symbol.Trim()  + " , ";
        return sReturn.Trim( new Char[] { ' ', ',', '.' } );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string getChannels()
    {   string sReturn = "";
        foreach (string cC in channels)
         sReturn += "<a href='http://" +  cC.Trim()  + "' title='" + cC.Trim()  + "' >" + cC.Trim() + "</a>" + " , ";
        return sReturn.Trim( new Char[] { ' ', ',', '.' } );
    }
    public string getChannelsAsString()
    {   string sReturn = "";
        foreach (string cC in channels)
            sReturn +=  cC.Trim()  + " , ";
        return sReturn.Trim( new Char[] { ' ', ',', '.' } );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string getTags()
    {   string sReturn = "";
        foreach (string cC in tags)
            sReturn += "<a href='http://" +  cC.Trim()  + "' title='" + cC.Trim()  + "' >" + cC.Trim() + "</a>" + " , ";
        return sReturn.Trim( new Char[] { ' ', ',', '.' } );
    }
    public string getTagsAsString()
    {   string sReturn = "";
        foreach (string cC in tags)
            sReturn +=  cC.Trim()  + " , ";
        return sReturn.Trim( new Char[] { ' ', ',', '.' } );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sDatetime"></param>
    /// <returns></returns>
    private string  getGMT(string sDatetime)
    {
        DateTime parsedDate;
        IFormatProvider provider = new CultureInfo("de-DE");
        bool isValidDate = DateTime.TryParse(sDatetime, provider , DateTimeStyles.AssumeUniversal, out parsedDate);
        if (isValidDate)
            return parsedDate.ToString("yyyy-MM-dd HH:mm:ss"); 
        else
            return null;
    }
    
}




