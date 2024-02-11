using MySqlConnector;


namespace sino.CollectNews;

/// <summary>
/// 
/// </summary>
public class dbConnector
{
    private MySqlConnection? connection;


    /// <summary>
    /// Connects the specified server.
    /// </summary>
    /// <param name="server">The server.</param>
    /// <param name="dbName">Name of the database.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    public void Connect(string server = "localhost", 
                        string dbName = "test", 
                        string userName = "claus",
                        string password = "74412127" , 
                        string port = "3306")
    {
        string connectionString = $"Server={server};Database={dbName};User={userName};Password={password};Port={port};Connection Timeout=120";
        connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();
            cLog.info("Connection succeeded.", modul: System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
        catch (Exception ex)
        {
            cLog.error($"Connection error: {ex.Message}", modul: System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }


    public MySqlDataReader query(string query)
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.CommandTimeout = 500;
            try
            {
                MySqlDataReader reader = cmd.ExecuteReader();
                return reader;                 
            }
            catch (MySqlException mex)
            {
                cLog.error($"MySqlException error: {mex.Message}", modul: System.Reflection.MethodBase.GetCurrentMethod().Name );
            }
        }
        catch (Exception ex)
        {
            cLog.error($"Connection error: {ex.Message}", modul: System.Reflection.MethodBase.GetCurrentMethod().Name );
        }

        return null;
    }

    public string readLastUpdateNews(int provider = 1 )
    {
        string sReturn = "";
        string sql =
            "SELECT  DATE_SUB( IFNULL(MAX(TIMESTAMP(`lastupdated`)), TIMESTAMP(DATE(NOW() - INTERVAL 3 DAY))), INTERVAL 0 HOUR ) AS `LastDate` " +
            " FROM " + connection.Database + ".news " +
            "WHERE provider = " + provider.ToString();
        try
        {
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sReturn = reader.GetDateTime("LastDate").ToString("yyyy-MM-dd HH:mm:ss");
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            cLog.error($"Connection error: {ex.Message}", modul: System.Reflection.MethodBase.GetCurrentMethod().Name );
        }
        return sReturn;
    }

    /// <summary>
    /// Inserts the finnhub.
    /// </summary>
    /// <param name="fhn">The FHN.</param>
    public void insertNews( News fhn)
    {
        try
        {
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText =
                " INSERT INTO " + connection.Database + ".news ( provider, providerid, ts, lastupdated, productcodes, headline, body, keywords, source, priority, country) " +
                " VALUES " +
                " ( @provider , @providerid , @ts ,@lastupdated , @productcodes, @headline, @body , @keywords , @source , @priority , @country )  " +
                " ON DUPLICATE KEY UPDATE " +
                "       headline = @headline ," +
                "       body = @body ," +
                "       keywords = @keywords ";

            cmd.Prepare();
            cmd.Parameters.AddWithValue("@provider", fhn.provider);
            cmd.Parameters.AddWithValue("@providerid", fhn.providerid);
            cmd.Parameters.AddWithValue("@ts", DateTime.Parse(fhn.ts));
            cmd.Parameters.AddWithValue("@lastupdated", DateTime.Parse(fhn.lastupdated));
            cmd.Parameters.AddWithValue("@productcodes", fhn.productcodes);
            cmd.Parameters.AddWithValue("@headline", fhn.headline);
            cmd.Parameters.AddWithValue("@body", fhn.body);
            cmd.Parameters.AddWithValue("@keywords", fhn.keywords);
            cmd.Parameters.AddWithValue("@source", fhn.source);
            cmd.Parameters.AddWithValue("@priority", fhn.priority);
            cmd.Parameters.AddWithValue("@country", fhn.country);
            cmd.ExecuteNonQuery();

        }
        catch (MySqlException ex)
        {
            cLog.error($"Connection error: {ex.Message}", modul: System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }

    /// <summary>
    /// Closes this instance.
    /// </summary>
    public void Close()
    {
        connection.Close();
    }
}