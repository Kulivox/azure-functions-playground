using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class IngestOutput
{
    [ServiceBusOutput(Constants.RawDataQueueName, Connection = Constants.ServiceBusWriter)]
    public required SensorData OutputEvent { get; init; }
    public required HttpResponseData HttpResponse { get; init; }
}