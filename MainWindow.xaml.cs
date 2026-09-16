using System;
using System.Windows;
using System.Windows.Media;
using GreenGrowIoTMonitor.Data;
using GreenGrowIoTMonitor.Models;
using GreenGrowIoTMonitor.Services;

namespace GreenGrowIoTMonitor
{
    public partial class MainWindow : Window
    {
        private readonly WeatherApiService _apiService = new WeatherApiService();
        private readonly SensorHistoryManager _historyManager = new SensorHistoryManager();

        public MainWindow()
        {
            InitializeComponent();
            UpdateStackUI();
        }

        private async void BtnFetch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BtnFetch.IsEnabled = false;
                TxtSystemStatus.Text = "System Status: FETCHING DATA...";
                TxtSystemStatus.Foreground = Brushes.Orange;

                var (temp, humidity, wind, soil) = await _apiService.FetchLatestDataAsync();

                // Display current sensor values
                TxtTemperature.Text = $"{temp.Value} {temp.Unit}";
                TxtHumidity.Text = $"{humidity.Value} {humidity.Unit}";
                TxtWind.Text = $"{wind.Value} {wind.Unit}";
                TxtSoil.Text = $"{soil.Value} {soil.Unit}";

                // Push temperature reading onto Stack
                _historyManager.PushReading(temp);

                TxtSystemStatus.Text = "System Status: ONLINE";
                TxtSystemStatus.Foreground = Brushes.Green;

                UpdateStackUI();
            }
            catch (Exception ex)
            {
                TxtSystemStatus.Text = "System Status: ERROR";
                TxtSystemStatus.Foreground = Brushes.Red;

                // Display simple, clear user message
                MessageBox.Show(ex.Message, "Fetch Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                BtnFetch.IsEnabled = true;
            }
        }

        private void BtnPeek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SensorReading latest = _historyManager.PeekLatest();
                MessageBox.Show($"Latest Stored Temperature:\n{latest.Value} {latest.Unit} (Recorded at {latest.Timestamp:HH:mm:ss})",
                                "Peek Operation Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operation Blocked", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnPop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SensorReading removed = _historyManager.PopLatest();
                MessageBox.Show($"Removed Reading:\n{removed.Value} {removed.Unit} (Recorded at {removed.Timestamp:HH:mm:ss})",
                                "Pop Operation Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateStackUI();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operation Blocked", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateStackUI()
        {
            TxtHistoryCount.Text = $"Stored Readings: {_historyManager.Count}";

            if (_historyManager.Count > 0)
            {
                SensorReading top = _historyManager.PeekLatest();
                TxtLatestReading.Text = $"Latest Stored Reading: {top.Value} {top.Unit} ({top.Timestamp:HH:mm:ss})";
            }
            else
            {
                TxtLatestReading.Text = "Latest Stored Reading: No readings stored";
            }
        }
    }
}