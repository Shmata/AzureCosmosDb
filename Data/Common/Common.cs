using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Common
{
    public class Common
    {
        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            // in order to check our connection string is valid or not 
            string connectionString = "YOUR_CONNECTIONSTRING_GOES_HERE";
            CloudStorageAccount cloudStorageAccount;
            if(CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount)) 
            {
                Console.WriteLine("Connectin string is valid");
                CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
                CloudTable cloudTable = cloudTableClient.GetTableReference(tableName);
                if( await cloudTable.CreateIfNotExistsAsync() )
                {
                    Console.WriteLine($"Table {tableName} is created");
                    
                }
                else
                {
                    Console.WriteLine($"Table {tableName} is already exist");
                }
                return cloudTable;
            }
            else
            {
                Console.WriteLine("Connection string is not valid");
                return null;
            }
        }
    }
}
