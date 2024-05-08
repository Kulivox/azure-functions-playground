namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class Constants
{
    // AEB Connection strings
    public const string ServiceBusReader = "ServiceBusReader";
    public const string ServiceBusWriter = "ServiceBusWriter";

    // DB Connection strings
    public const string AzureSqlConnectionString = "AzureSqlConnectionString";
    
    // Queue names
    public const string RawDataQueueName = "raw-sensor-data";
    public const string ValidatedDataQueueName = "validated-sensor-data";
    public const string AnomalyDetectionQueueName = "anomaly-detection";
}