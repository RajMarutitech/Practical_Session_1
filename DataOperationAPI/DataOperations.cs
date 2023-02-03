using System.Data;
using System.Data.SqlClient;

namespace DataOperationAPI
{
    public class DataOperations {

        SqlConnection? connection;

      
        public void InsertBulkRecord(List<Source> sources)
        {
            connection = new SqlConnection(Global.CONNECTION_STRING);
            
            
        }


    }

}