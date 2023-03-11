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
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=shahabdbuser;AccountKey=vXU61gzLDz6C1C5Nwig9HO5lAitKIOI85jXbgtFvMZzicuwE9y772smA7ooD9EUJ9W836rlsW3xaACDbT9oTfg==;TableEndpoint=https://shahabdbuser.table.cosmos.azure.com:443/;";
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
