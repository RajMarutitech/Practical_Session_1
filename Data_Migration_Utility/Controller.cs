using DatabaseOperaionAPI;
using DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Migration_Utility
{
    public class Controller
    {
        public void AddToSource(Databaseoperations dataOperation)
        {
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                List<SourceSchema> sources = new List<SourceSchema>();
                for (int j = 0; j < 100; j++)
                {
                    sources.Add(new SourceSchema(j + 1, random.Next(1, 10000), random.Next(1, 10000)));
                }
                dataOperation.InsertToSourceTable(sources);
            }
        }

        public void DataMigration(Databaseoperations dataOperation)
        {
            Console.WriteLine("Enter the range from start to end");

            int start = Convert.ToInt32(Console.ReadLine());
            int end = Convert.ToInt32(Console.ReadLine());

            List<SourceSchema> Data = dataOperation.GetData(start, end);
            //List<DestinationSchema> destinationData = new();
            //int count = 1;
            //Thread thread1 = new Thread();
            

            //foreach (sourceschema record in data)
            //{
            //    destinationdata.add(new destinationschema(count++, record.id, sumoperation(record.firstnumber, record.secondnumber)));
            //}
            sum(Data, dataOperation);
            //int size = destinationData.Count;
            //int i = 0;
            //while (i < size)
            //{
            //    count = 1;
            //    List<DestinationSchema> destinationList = new();
            //    for (i = 0; i < size && count <= 100; i++)
            //    {
            //        destinationList.Add(destinationData[i]);

            //    }
            //    dataOperation.InsertToDestinationTable(destinationData);
            //    Console.WriteLine("Batch Added");
            //}
            //Console.WriteLine("Data Added!");

        }

        public void ClearDataFromDestinationTable(Databaseoperations dataOperation)
        {
            string tablename = "DestinationTable";
            dataOperation.DeleteData(tablename);

        }

        public void ClearDataFromSourceTable(Databaseoperations dataOperation)
        {
            string tablename = "SourceTable";
            dataOperation.DeleteData(tablename);
        }

        private void sum(List<SourceSchema> Data, Databaseoperations dataOperation)
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

                dataOperation.InsertOneDataToDestinationTable(sourceList, ref idc);
                //dataOperation.InsertToDestinationTable(sourceList);
                Console.WriteLine("Batch Added");
            }

        }
        //int SumOperation(int firstNumber, int secondNumber)
        //{
        //    int sum = firstNumber + secondNumber;
        //    Thread.Sleep(50);
        //    return sum;
        //}




        //public void HandleSum(Databaseoperations dataOperation)
        //{
        //    Console.WriteLine("Enter the range from start to end");

        //    int start = Convert.ToInt32(Console.ReadLine());
        //    int end = Convert.ToInt32(Console.ReadLine());

        //    List<SourceSchema> Data = dataOperation.GetData(start, end);
        //    List<DestinationSchema> destinationData = new();
        //    int count = 1;
        //    //Thread thread1 = new Thread();


        //    foreach (SourceSchema record in Data)
        //    {
        //        destinationData.Add(new DestinationSchema(count++, record.Id, SumOperation(record.FirstNumber, record.SecondNumber)));
        //    }
        //    int size = destinationData.Count;
        //    int i = 0;
        //    while (i < size)
        //    {
        //        count = 1;
        //        List<DestinationSchema> destinationList = new();
        //        for (i = 0; i < size && count <= 100; i++)
        //        {
        //            destinationList.Add(destinationData[i]);

        //        }
        //        dataOperation.InsertToDestinationTable(destinationData);
        //        Console.WriteLine("Batch Added");
        //    }
        //    Console.WriteLine("Data Added!");

        //}


    }
}
