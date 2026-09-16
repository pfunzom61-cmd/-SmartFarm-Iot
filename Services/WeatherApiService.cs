using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GreenGrowIoTMonitor.Models;

namespace GreenGrowIoTMonitor.Services
{
    public class WeatherApiService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        // Coordinates for Pretoria, South Africa
        private const string Latitude = "-25.7479";
        private const string Longitude = "28.2293";
        private const string ApiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={Latitude}&longitude={Longitude}&current=temperature_2m,relative_humidity_2m,wind_speed_10m,soil_temperature_0cm";

        public async Task<(SensorReading Temp, SensorReading Humidity, SensorReading Wind, SensorReading Soil)> FetchLatestDataAsync()
        {
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();

                // Fixed: Accessed via .Content property
                string jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement current = doc.RootElement.GetProperty("current");

                double tempVal = current.GetProperty("temperature_2m").GetDouble();
                double humidityVal = current.GetProperty("relative_humidity_2m").GetDouble();
                double windVal = current.GetProperty("wind_speed_10m").GetDouble();
                double soilVal = current.GetProperty("soil_temperature_0cm").GetDouble();

                var temp = new SensorReading { SensorName = "Temperature", Value = tempVal, Unit = "°C", Status = "OK" };
                var humidity = new SensorReading { SensorName = "Relative Humidity", Value = humidityVal, Unit = "%", Status = "OK" };
                var wind = new SensorReading { SensorName = "Wind Speed", Value = windVal, Unit = "km/h", Status = "OK" };
                var soil = new SensorReading { SensorName = "Soil Temperature", Value = soilVal, Unit = "°C", Status = "OK" };

                return (temp, humidity, wind, soil);
            }
            catch (HttpRequestException httpEx)
            {
                // Internal diagnostic logging for developers
                System.Diagnostics.Debug.WriteLine($"[DEV LOG - API ERROR]: HTTP request failed: {httpEx.Message}");
                throw new Exception("Unable to reach the weather server. Please check your internet connection.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DEV LOG - PARSE ERROR]: Parsing failed: {ex.Message}");
                throw new Exception("Received unexpected response format from the weather service.");
            }
        }
    }
}