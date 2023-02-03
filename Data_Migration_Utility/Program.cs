﻿using Data_Migration_Utility;
using DatabaseOperaionAPI;
using DataModel;


Databaseoperations DB = new Databaseoperations();
Controller controller= new Controller();



while(true)
{
    Console.WriteLine("1. Initialize Record\n2. Sum Operation\n3.Clear Data From Destination Table.\n4.Clear Data From Source Table\n5. Exit");


    Console.Write("Enter your choice: ");
    int choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            controller.AddToSource(DB);
            break;
        case 2:
            controller.DataMigration(DB);
            break;
        case 3:
            controller.ClearDataFromDestinationTable(DB);
            break;
        case 4:
            controller.ClearDataFromSourceTable(DB);
            break;
        case 5:
            return;
    }
}