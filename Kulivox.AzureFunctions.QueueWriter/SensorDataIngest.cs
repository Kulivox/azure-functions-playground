using System.Net;
using Kulivox.AzureFunctions.QueueWriter.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Kulivox.AzureFunctions.QueueWriter;

public class SensorDataIngest
{
    private readonly ILogger _logger;

    public SensorDataIngest(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SensorDataIngest>();
    }

    [Function("SensorDataIngest")]
    public IngestOutput Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sensors/data")] HttpRequestData req,
        [FromBody] SensorData sensorData,
        FunctionContext executionContext
        )
    {
        _logger.LogInformation("Enqueued sensor data from sensor: {id}", sensorData.DeviceId);

        var response = req.CreateResponse(HttpStatusCode.OK);
        return new IngestOutput()
        {
            HttpResponse = response,
            OutputEvent = sensorData
        };
        
    }
}