using EmployeeManagement.Data.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Operations
{
    public class Operations
    {
        public async Task TriggerOpertations()
        {
            string tableName = "Employee";
            CloudTable cloudTable = await Common.Common.CreateTableAsync(tableName);
            try
            {
                await BasicTableOperations(cloudTable);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task BasicTableOperations(CloudTable cloudTable)
        { 
            // create entity
            EmployeeEntity employeeEntity = new EmployeeEntity("Consultant")
            {
                FullName = "Shahab Matapour",
                Email = "shahab@matapour.pro",
                RowKey = "shahab-matapour-rk",
            };

            TableOperation addOperation = TableOperation.InsertOrMerge(employeeEntity);
            TableResult addResult = await cloudTable.ExecuteAsync(addOperation);
            EmployeeEntity addResponse = addResult.Result as EmployeeEntity;

            if(addResult.RequestCharge.HasValue) {
                Console.WriteLine($"addResponse ==> {addResponse.PartitionKey} - {addResponse.RowKey} - {addResponse.FullName}");
            }

        }
    }
}
