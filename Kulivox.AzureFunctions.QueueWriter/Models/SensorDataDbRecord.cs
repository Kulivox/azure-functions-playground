namespace Kulivox.AzureFunctions.QueueWriter.Models;

public class SensorDataDbRecord
{
    public required Guid Id { get; set; }

    public required string SensorId { get; set; }
    
    public required DateTimeOffset Timestamp { get; set; }
    
    public required double Temperature { get; set; }
}