using System.Globalization;

namespace sino.CollectNews;

public class TimeDate
{

    public string getGMT(string sDatetime)
    {
        DateTime parsedDate;
        IFormatProvider provider = new CultureInfo("de-DE");
        bool isValidDate = DateTime.TryParse(sDatetime, provider, DateTimeStyles.AssumeUniversal, out parsedDate);
        if (isValidDate)
            return parsedDate.ToString("yyyy-MM-dd HH:mm:ss");
        else
            return null;
    }
}