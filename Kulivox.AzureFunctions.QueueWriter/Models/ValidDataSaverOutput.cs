using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class ValidDataSaverOutput
{
   
    
    [SqlOutput(Constants.SensorDataTableName, connectionStringSetting: Constants.AzureSqlConnectionString)]
    public required SensorDataDbRecord DbRecord { get; init; }
    
}