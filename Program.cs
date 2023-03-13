
using EmployeeManagement.Data.Common;
using EmployeeManagement.Data.Operations;

// Create tge Employee table in Cosmos DB 
// Common.CreateTableAsync("Employee").GetAwaiter().GetResult();

// Create entity in table 
Operations dataOperation = new Operations();
dataOperation.TriggerOpertations().Wait();


Console.ReadKey();

