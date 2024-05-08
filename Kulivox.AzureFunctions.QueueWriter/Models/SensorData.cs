namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class SensorData
{
    public required DateTimeOffset Timestamp { get; init; }
    public required string DeviceId { get; init; }
    public required double Temperature { get; init; }
}