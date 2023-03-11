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
                // Add to cosmos db
                //await AddTableOperations(cloudTable);

                // update record in cosmos db
                //await UpdateTableOperations(cloudTable);

                // Retreive all data from a table 
                //RetrieveAllDataOperations(cloudTable);

                // Retrieve single record
                // insert rowkey and partition key as second and third parameters
                //RetrieveSingleRecord(cloudTable, "Manual RK", "Manual PK");

                // delete a record 
                EmployeeEntity entity = RetrieveSingleRecord(cloudTable, "Manual RK", "Manual PK");
                DeleteRecord(cloudTable, entity);

            }
            catch (Exception)
            {

                throw;
            }
        }
        // add a record 
        public static async Task AddTableOperations(CloudTable cloudTable)
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

            if (addResult.RequestCharge.HasValue)
            {
                Console.WriteLine($"addResponse ==> {addResponse.PartitionKey} - {addResponse.RowKey} - {addResponse.FullName}");
            }

        }

        // update a record
        public static async Task UpdateTableOperations(CloudTable cloudTable)
        {
            // create entity
            EmployeeEntity updatedDmployeeEntity = new EmployeeEntity("Consultant")
            {
                FullName = "Hassan Matapour",
                Email = "newshm@matapour.pro",
                RowKey = "shahab-matapour-rk-new",
            };
            TableOperation addOperation = TableOperation.InsertOrMerge(updatedDmployeeEntity);
            TableResult addResult = await cloudTable.ExecuteAsync(addOperation);
            EmployeeEntity addResponse = addResult.Result as EmployeeEntity;

            if (addResult.RequestCharge.HasValue)
            {
                Console.WriteLine($"addResponse ==> {addResponse.PartitionKey} - {addResponse.RowKey} - {addResponse.FullName}");
            }

            updatedDmployeeEntity.FullName = "Hassan Matapourtavakolian";

            TableOperation updateOperation = TableOperation.InsertOrMerge(updatedDmployeeEntity);
            TableResult updateResult = await cloudTable.ExecuteAsync(updateOperation);
            EmployeeEntity updateResponse = updateResult.Result as EmployeeEntity;

            if (updateResult.RequestCharge.HasValue)
            {
                Console.WriteLine($"Updated Response ==> {updateResponse.PartitionKey} - {updateResponse.RowKey} - {updateResponse.FullName}");
            }

        }

        // retrieve all data 
        public void RetrieveAllDataOperations(CloudTable cloudTable)
        {
            var allDataQuery = cloudTable.ExecuteQuery(new TableQuery<EmployeeEntity>());
            foreach (var employee in allDataQuery)
            {
                Console.WriteLine($"Row Key : {employee.RowKey} - Email : {employee.Email} - Partition Key: {employee.PartitionKey}");
            }

        }

        // retrieve a record
        public EmployeeEntity RetrieveSingleRecord(CloudTable cloudTable, string rowKey, string partitionKey)
        {
            TableOperation retrieveSingleOperation = TableOperation.Retrieve<EmployeeEntity>(partitionKey, rowKey);
            TableResult result = cloudTable.Execute(retrieveSingleOperation);
            EmployeeEntity employee = result.Result as EmployeeEntity;

            if (employee != null)
            {
                Console.WriteLine($"Employee partition key is :{employee.PartitionKey} and full name is : {employee.FullName}");
                return employee;

            }
            else
            {
                return null;
            }
        }

        // delete a record 
        public void DeleteRecord(CloudTable cloudTable, EmployeeEntity employeeEntity)
        {
            TableOperation deleteOperation = TableOperation.Delete(employeeEntity);
            TableResult result = cloudTable.Execute(deleteOperation);

            if (result.RequestCharge.HasValue)
            {
                Console.WriteLine($"Request charge of delete operation {result.RequestCharge}");
            }
        }
    }
}
