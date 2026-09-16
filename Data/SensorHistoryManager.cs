using System;
using System.Collections.Generic;
using GreenGrowIoTMonitor.Models;

namespace GreenGrowIoTMonitor.Data
{
    public class SensorHistoryManager
    {
        // Encapsulated Stack to enforce LIFO principles
        private readonly Stack<SensorReading> _temperatureStack = new Stack<SensorReading>();

        public int Count => _temperatureStack.Count;

        public void PushReading(SensorReading reading)
        {
            if (reading == null) throw new ArgumentNullException(nameof(reading));
            _temperatureStack.Push(reading);
            System.Diagnostics.Debug.WriteLine($"[DEV LOG - STACK]: Pushed reading '{reading.Value} {reading.Unit}'. New count: {Count}");
        }

        public SensorReading PeekLatest()
        {
            if (_temperatureStack.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("[DEV LOG - STACK]: Peek attempted on empty stack.");
                throw new InvalidOperationException("No readings are currently stored in history.");
            }
            return _temperatureStack.Peek();
        }

        public SensorReading PopLatest()
        {
            if (_temperatureStack.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("[DEV LOG - STACK]: Pop attempted on empty stack.");
                throw new InvalidOperationException("No readings available to remove from history.");
            }

            SensorReading popped = _temperatureStack.Pop();
            System.Diagnostics.Debug.WriteLine($"[DEV LOG - STACK]: Popped reading '{popped.Value} {popped.Unit}'. Remaining count: {Count}");
            return popped;
        }
    }
}