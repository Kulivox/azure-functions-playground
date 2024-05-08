using System;
using Azure.Messaging.ServiceBus;
using Kulivox.AzureFunctions.QueueWriter.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Kulivox.AzureFunctions.QueueWriter;

public class DataValidator
{
    private readonly ILogger<DataValidator> _logger;

    public DataValidator(ILogger<DataValidator> logger)
    {
        _logger = logger;
    }
    
    private static readonly IReadOnlyCollection<string> ValidDeviceIds = new HashSet<string>()
    {
        "living-room",
        "basement",
        "roof"
    };

    [Function(nameof(DataValidator))]
    public DataValidatorOutput? Run([ServiceBusTrigger(Constants.RawDataQueueName, Connection = Constants.ServiceBusReader)] ServiceBusReceivedMessage message)
    {
        _logger.LogInformation("Processing message ID: {id}", message.MessageId);
       
        // Validate the message
        var sensorData = message.Body.ToObjectFromJson<SensorData>();
        
        if (!ValidDeviceIds.Contains(sensorData.DeviceId))
        {
            _logger.LogWarning("Invalid device ID: {id}", sensorData.DeviceId);
            // Return null to indicate that the message should not be enqueued
            return null;
        }

        if (sensorData.Temperature < -274)
        {
            _logger.LogWarning("Invalid temperature: {temp} from sensor {sensor}", sensorData.Temperature, sensorData.DeviceId);
            return null;
        }
        
        if (sensorData.Timestamp > DateTimeOffset.UtcNow.AddHours(1))
        {
            _logger.LogWarning("Invalid timestamp: {timestamp} from sensor {sensor}", sensorData.Timestamp, sensorData.DeviceId);
            return null;
        }
        
        // Enqueue the validated data
        _logger.LogInformation("Enqueued validated sensor data from sensor: {id}", sensorData.DeviceId);
        return new DataValidatorOutput()
        {
            AnomalyDetectionEvent = sensorData,
            DataSaverEvent = sensorData
        };
    }
}