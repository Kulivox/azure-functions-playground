using Microsoft.Azure.Functions.Worker;

namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class DataValidatorOutput
{
    [ServiceBusOutput(Constants.AnomalyDetectionQueueName, Connection = Constants.ServiceBusWriter)]
    public required SensorData AnomalyDetectionEvent { get; init; }
    
    [ServiceBusOutput(Constants.ValidatedDataQueueName, Connection = Constants.ServiceBusWriter)]
    public required SensorData DataSaverEvent { get; init; }
}