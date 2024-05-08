using System;
using Azure;
using Azure.Messaging.ServiceBus;
using Kulivox.AzureFunctions.QueueWriter.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Kulivox.AzureFunctions.QueueWriter;

public class ValidDataSaver
{
    private readonly ILogger<ValidDataSaver> _logger;
    
    
    public ValidDataSaver(ILogger<ValidDataSaver> logger)
    {
        _logger = logger;
    }
    
    [Function(nameof(ValidDataSaver))]
    public ValidDataSaverOutput Run([ServiceBusTrigger(Constants.ValidatedDataQueueName, Connection = Constants.ServiceBusReader)] ServiceBusReceivedMessage message)
    {
        var sensorData = message.Body.ToObjectFromJson<SensorData>();
        _logger.LogInformation("Saving validated sensor data from sensor: {id}", sensorData.DeviceId);
        return new ValidDataSaverOutput()
        {
            DbRecord = new SensorDataDbRecord()
            {
                Id = Guid.NewGuid(),
                SensorId = sensorData.DeviceId,
                Temperature = sensorData.Temperature,
                Timestamp = sensorData.Timestamp
            },
        };
    }
}