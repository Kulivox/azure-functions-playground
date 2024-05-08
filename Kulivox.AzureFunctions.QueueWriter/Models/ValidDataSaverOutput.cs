using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class ValidDataSaverOutput
{
   
    
    [SqlOutput("dbo.ToDo", connectionStringSetting: Constants.AzureSqlConnectionString)]
    public required SensorDataDbRecord DbRecord { get; init; }
    
}