using DataModel;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace DatabaseOperaionAPI
{
    public class Databaseoperations
    {
        public void InsertToSourceTable(List<SourceSchema> sources)
        {
            using (SqlConnection connection = new(ENV.CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    int size = sources.Count;
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add(new DataColumn("ID", typeof(int)));
                    tbl.Columns.Add(new DataColumn("FirstNumber", typeof(float)));
                    tbl.Columns.Add(new DataColumn("SecondNumber", typeof(float)));
                    for (int i = 0; i < size; i++)
                    {
                        DataRow dr = tbl.NewRow();
                        dr["ID"] = sources[i].Id;
                        dr["FirstNumber"] = sources[i].FirstNumber;
                        dr["SecondNumber"] = sources[i].SecondNumber;
                        tbl.Rows.Add(dr);
                    }
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
                    bulkCopy.DestinationTableName = "SourceTable";
                    bulkCopy.ColumnMappings.Add("ID", "ID");
                    bulkCopy.ColumnMappings.Add("FirstNumber", "FirstNumber");
                    bulkCopy.ColumnMappings.Add("SecondNumber", "SecondNumber");
                    bulkCopy.WriteToServer(tbl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<SourceSchema> GetData(int start, int end)
        {
            List<SourceSchema> Data = new();
            try
            {
                using (SqlConnection connection = new(ENV.CONNECTION_STRING))
                {
                    connection.Open();
                    SqlCommand cmd;
                    SqlDataReader reader;
                    string sql = $"select * from sourcetable where id >= {start} and id <= {end}";
                    cmd = new SqlCommand(sql, connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Data.Add(new SourceSchema(Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), Convert.ToInt32(reader.GetValue(2))));
                    }
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            return Data;
        }

        public void InsertOneDataToDestinationTable(List<SourceSchema> Data, ref int count, ref Dictionary<string, int> StatusCount)
        {
            try
            {
                using (SqlConnection connection = new(ENV.CONNECTION_STRING))
                {
                    connection.Open();
                    SqlCommand cmd;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    string sql = "insert into DestinationTable values (@ID, @Foreign_Key, @Sum)";

                    for (int i = 0; i < Data.Count; i++)
                    {
                        cmd = new SqlCommand(sql, connection);
                        int sum = Sum(Data[i].FirstNumber, Data[i].SecondNumber);
                        cmd.Parameters.AddWithValue("@ID", count++);
                        cmd.Parameters.AddWithValue("@Foreign_Key", Data[i].Id);
                        cmd.Parameters.AddWithValue("@Sum", sum);
                        cmd.ExecuteNonQuery();
                        StatusCount["Completed"]++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
            

        public void DeleteData(string tablename)
        {
            try
            {
                using (SqlConnection connection = new(ENV.CONNECTION_STRING))
                {
                    connection.Open();
                    SqlCommand cmd;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string sql = $"Truncate table {tablename}";
                    cmd = new SqlCommand(sql, connection);
                    adapter.InsertCommand = new SqlCommand(sql, connection);
                    adapter.InsertCommand.ExecuteNonQuery();
                    Console.WriteLine("Records Deleted Successfully");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public int Sum(int first, int second)
        {
            int res = first + second;
            Thread.Sleep(50);
            return res;
        }
        //public void InsertToDestinationTable(List<DestinationSchema> destinatonData)
        //{
        //    using (connection = new SqlConnection(ENV.CONNECTION_STRING))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            int size = destinatonData.Count;
        //            DataTable tbl = new DataTable();
        //            tbl.Columns.Add(new DataColumn("ID", typeof(int)));
        //            tbl.Columns.Add(new DataColumn("Foreign_Key", typeof(int)));
        //            tbl.Columns.Add(new DataColumn("Sum", typeof(int)));
        //            for (int i = 0; i < size; i++)
        //            {
        //                DataRow dr = tbl.NewRow();
        //                dr["ID"] = destinatonData[i].Id;
        //                dr["Foreign_Key"] = destinatonData[i].Foreign_Key;
        //                dr["Sum"] = destinatonData[i].Sum;
        //                tbl.Rows.Add(dr);
        //            }
        //            SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
        //            bulkCopy.DestinationTableName = "DestinationTable";
        //            bulkCopy.ColumnMappings.Add("ID", "ID");
        //            bulkCopy.ColumnMappings.Add("Foreign_Key", "Foreign_Key");
        //            bulkCopy.ColumnMappings.Add("Sum", "Sum");
        //            bulkCopy.WriteToServer(tbl);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}
    }

    
}