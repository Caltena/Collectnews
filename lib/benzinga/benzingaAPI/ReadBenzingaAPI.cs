
using RestSharp;

namespace sino.CollectNews;

/// <summary>
/// 
/// </summary>
public class ReadBenzingaAPI
{
    private dbConnector dbC;

    /// <summary>
    /// 
    /// </summary>
    public void Deconstruct()
    {
        dbC.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    public ReadBenzingaAPI()
    {
        dbC = new dbConnector();
        dbC.Connect();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateInput"></param>
    /// <returns></returns>
    private int readBenzingaFromDate(string dateInput)
    {
        DateTime currentTime = DateTime.Parse(dateInput);
        long unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
        return readBenzingaFromUnixTimestamp(unixTime.ToString());
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int readBenzingaFromLastDate()
    {
        return readBenzingaFromDate(dbC.readLastUpdateNews(1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sFrom"></param>
    /// <returns></returns>
    public int readBenzingaFromUnixTimestamp(string sFrom = "1707128776")
    {
        int iReturn = 0;
        try
        {
            RestClient client = new RestClient("https://api.benzinga.com/api/v2/news/?token=" + Config.token);
            RestRequest request = new RestRequest();
            //  request.AddParameter("from", "2024-02-01");
            //  request.AddParameter("to", "2024-02-03");
            request.AddParameter("publishedSince", sFrom);
            request.AddParameter("displayOutput", "full");
            request.AddParameter("pageSize", "100");

            RestResponse<List<BenzingaAPI>> response = client.Execute<List<BenzingaAPI>>(request);

            if (!response.IsSuccessful)
            {
                cLog.error($"API Connection error: {response.ErrorException?.Message}",
                    modul: System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
            else
            {
                var listBenzingaNews = response.Data.OrderBy(Root => Root.id);
                foreach (BenzingaAPI fhn in listBenzingaNews)
                {
                    cLog.info(String.Format(" Headline {0} \t {1}", fhn.id, fhn.title),
                        modul: System.Reflection.MethodBase.GetCurrentMethod().Name);
                    dbC.insertNews(new News(fhn));
                }
                iReturn = listBenzingaNews.Count();
            }
        }
        catch (Exception ex)
        {
            cLog.error($"API error: {ex.Message}", modul: System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        return iReturn;
    }
}