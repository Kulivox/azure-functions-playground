using System;
using Azure;
using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using Kulivox.AzureFunctions.QueueWriter.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Kulivox.AzureFunctions.QueueWriter;

public class AnomalyDetector
{
    private const string SenderAddress = "DoNotReply@4de3b0f0-7101-4a61-a9e2-650f7660de55.azurecomm.net";
    private const string RecipientAddress = "mich.klinka@gmail.com";
    
    private readonly ILogger<AnomalyDetector> _logger;
    private readonly Lazy<EmailClient> _emailClient = new(GetEmailClient);

    private static EmailClient GetEmailClient()
    {
        var connectionString = Environment.GetEnvironmentVariable("AlertMailerConnectionString");
        return new EmailClient(connectionString);
    }


    public AnomalyDetector(ILogger<AnomalyDetector> logger)
    {
        _logger = logger;
    }
    
    private Task SendHighTemperatureAlert(SensorData sensorData)
    {
        var message = new EmailMessage(
            SenderAddress,
            RecipientAddress,
            new EmailContent("High temperature alert")
            {
                PlainText = $"High temperature detected: {sensorData.Temperature} from sensor {sensorData.DeviceId}"
            });

        _logger.LogInformation("Sending high temperature alert email");
        return _emailClient.Value.SendAsync(WaitUntil.Completed, message);
    }

    [Function(nameof(AnomalyDetector))]
    public async Task Run(
        [ServiceBusTrigger(Constants.AnomalyDetectionQueueName, Connection = Constants.ServiceBusReader)]
        ServiceBusReceivedMessage message)
    {
        var sensorData = message.Body.ToObjectFromJson<SensorData>();
        if (sensorData.Temperature > 100)
        {
            _logger.LogInformation("High temperature detected: {temp} from sensor {sensor}", sensorData.Temperature, sensorData.DeviceId);
            await SendHighTemperatureAlert(sensorData);
        }
    }
}