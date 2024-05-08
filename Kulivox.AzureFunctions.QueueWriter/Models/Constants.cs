namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class Constants
{
    public const string ServiceBusReader = "ServiceBusReader";
    public const string ServiceBusWriter = "ServiceBusWriter";

    public const string RawDataQueueName = "raw-sensor-data";
    public const string ValidatedDataQueueName = "validated-sensor-data";
}