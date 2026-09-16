using System;

namespace GreenGrowIoTMonitor.Models
{
    public class SensorReading
    {
        public string SensorName { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{SensorName}: {Value} {Unit} [{Timestamp:HH:mm:ss}]";
        }
    }
}