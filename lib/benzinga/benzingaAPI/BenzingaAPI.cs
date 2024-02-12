using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace sino.CollectNews
{
    /// <summary>
    /// 
    /// </summary>
    public class BenzingaAPI : TimeDate
    {


        
        public int id { get; set; }
        
        public string? author { get; set; }

        private string? _created;
        public string created
        {
            get { return getGMT(_created);}
            set { _created = value; }
        }

        private string? _updated; 
        public string updated 
        {
            get { return getGMT(_updated);}
            set { _updated = value; }
        } 

        private string _title;
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

        public string teaser { get; set; } = "";
        public string body { get; set; } = "";
        
        public string url { get; set; } = "";
        
        public List<BenzingaImage>? image { get; set; }
        public List<BenzingaChannel>? channels { get; set; }
        public List<BenzingaStock>? stocks { get; set; }
        public List<BenzingaTag>? tags { get; set; }

        /// <summary>
        ///  Format tags
        /// </summary>
        /// <returns></returns>
        public string getTagsAsString()
        {   string sReturn = "";
            foreach (BenzingaTag cT in tags)
                sReturn += cT.name + " , ";
            return sReturn.Trim( new Char[] { ' ', ',', '.' } );
        }
        

        
        /// <summary>
        /// Format Channel
        /// </summary>
        /// <returns></returns>
        public string getChannel()
        {   string sReturn = "";
            foreach (BenzingaChannel cC in channels)
                sReturn += "<a href='http://" +  cC.name.Trim()  + "' title='" + cC.name.Trim()  + "' >" + cC.name.Trim() + "</a>" + " , ";
            return sReturn.Trim( new Char[] { ' ', ',', '.' } );
        }
        
        public string getChannelAsString()
        {   string sReturn = "";
            foreach (BenzingaChannel cC in channels)
                sReturn +=  cC.name.Trim()   + " , ";
            return sReturn.Trim( new Char[] { ' ', ',', '.' } );
        }        
        
        /// <summary>
        ///  Format Stock
        /// </summary>
        /// <returns></returns>
        public string getStocks()
        {   string sReturn = "";
            foreach (BenzingaStock cS in stocks)
                sReturn +=  "<A HREF='http://symb/" +  cS.name.Trim()  + "' >" + cS.name.Trim() + "</A>" + " , ";
            return sReturn.Trim( new Char[] { ' ', ',', '.' } );
        }
        
        public string getStocksAsString()
        {   string sReturn = "";
            foreach (BenzingaStock cS in stocks)
                sReturn +=   cS.name.Trim()   + " , ";
            return sReturn.Trim( new Char[] { ' ', ',', '.' } );
        }
        
    }



}
