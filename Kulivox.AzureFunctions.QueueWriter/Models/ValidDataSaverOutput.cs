using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;

namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class ValidDataSaverOutput
{
    [ServiceBusOutput(Constants.AnomalyDetectionQueueName, Connection = Constants.ServiceBusWriter)]
    public required SensorData OutputEvent { get; init; }
    
    [SqlOutput("dbo.ToDo", connectionStringSetting: Constants.AzureSqlConnectionString)]
    public required SensorDataDbRecord DbRecord { get; init; }
}