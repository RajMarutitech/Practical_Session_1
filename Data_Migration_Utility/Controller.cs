using DatabaseOperaionAPI;
using DataModel;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;

namespace Data_Migration_Utility
{
    public class Controller
    {
        Dictionary<string, int> StatusRecords = new();
        public void AddToSource(Databaseoperations databaseoperations)
        {
            Random random = new Random();
            Console.WriteLine("Inserting....");
            for (int i = 0; i < 10000; i++)
            {
                List<SourceSchema> sources = new List<SourceSchema>();
                for (int j = 0; j < 100; j++)
                {
                    sources.Add(new SourceSchema(j + 1, random.Next(1, 10000), random.Next(1, 10000)));
                }
                databaseoperations.InsertToSourceTable(sources);
            }
            Console.WriteLine("Inserted Successfully.\n");
        }

        public void DataMigration(Databaseoperations databaseoperations)
        {
            
            Console.WriteLine("Enter the range from start to end");

            int start = Convert.ToInt32(Console.ReadLine());
            int end = Convert.ToInt32(Console.ReadLine());
            bool check = CheckRange(start, end);

            if(!check)
            {
                Console.WriteLine("InCorrect Range!!");
                return;
            }
            List<SourceSchema> Data = databaseoperations.GetData(start, end);
            if(Data.Count == 0)
            {
                Console.WriteLine("No Data Found in range!");
                return;
            }
            StatusRecords["Completed"] = 0;
            StatusRecords["Canceled"] = 0;
            StatusRecords["Ongoing"] = (end - start) + 1;
            string status = "";
            
            Thread t1 = new Thread(() => CreateBatch(Data,databaseoperations, ref StatusRecords));
            CheckCancel(out status);
            t1.Start();
            if(status.ToLower().Equals("cancel"))
            {
                Console.WriteLine("canceled successfully!");
                return;
            }
        }

        public void ClearDataFromDestinationTable(Databaseoperations databaseoperations)
        {
            string tablename = "DestinationTable";
            databaseoperations.DeleteData(tablename);

        }

        public void ClearDataFromSourceTable(Databaseoperations databaseoperations)
        {
            string tablename = "SourceTable";
            databaseoperations.DeleteData(tablename);
        }
        public void CheckStatus()
        {
            Console.WriteLine("Completed " + StatusRecords["Completed"]);
            Console.WriteLine("Canceled " + (StatusRecords["Ongoing"] - StatusRecords["Completed"]));
            StatusRecords["Ongoing"] = 0;
            Console.WriteLine("Ongoing " + StatusRecords["Ongoing"]);
        }


        private void CreateBatch(List<SourceSchema> Data, Databaseoperations databaseoperations, ref Dictionary<string, int> StatusRecords)
        {
            int count = 1, idc = 1;
            int i = 0;
            int size = Data.Count;
            while (i < size)
            {
                count = 1;
                List<SourceSchema> sourceList = new();
                for (i = 0; i < size && count <= 100; i++)
                {
                    sourceList.Add(Data[i]);
                }
                databaseoperations.InsertOneDataToDestinationTable(sourceList, ref idc, ref StatusRecords);
            }
        }

        private void CheckCancel(out string status)
        {
            Console.WriteLine("Type Cancel to stop");
            status = Console.ReadLine();
        }

        private bool CheckRange(int start, int end)
        {
            if (start <= 0 || start >= 1000000 || end <= 0 || end >= 1000000 || start > end) return false;
            else return true;
        }
    }
}
