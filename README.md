# 🌿 GreenGrow IoT Monitor

A modern C# WPF desktop application built for **GreenGrow Farms** to monitor real-time greenhouse environmental metrics in Pretoria, South Africa. The application integrates with the Open-Meteo REST API and demonstrates core Data Structure concepts by utilizing a **LIFO (Last-In, First-Out) Stack** to track sensor reading history.

---

## 🚀 Features

* **Real-Time API Integration:** Fetches live weather and soil metrics directly from the Open-Meteo REST API using Pretoria geographical coordinates (`-25.7479, 28.2293`).
* **LIFO Stack Memory Engine:** Manages historical temperature readings using a custom C# `Stack<SensorReading>` data structure.
* **Stack Operations (`Push`, `Peek`, `Pop`, `Count`):**
  * `Push`: Automatically adds every new temperature reading to the top of the stack upon API retrieval.
  * `Peek`: View the top/latest temperature reading without removing it from history.
  * `Pop`: Remove the top/latest temperature reading, causing the history to roll back to the previously recorded item.
* **Safety & Exception Handling:** Prevents invalid operations when attempting to `Peek` or `Pop` from an empty stack.
* **Modern Cyber-Green Dashboard UI:** Designed with a glassmorphic aesthetic, glowing status indicators, and visual stack metrics.

---

## 🛠️ Tech Stack & Architecture

* **Language:** C# (.NET 10 / .NET Core)
* **Framework:** Windows Presentation Foundation (WPF)
* **Data Structure:** `System.Collections.Generic.Stack<T>`
* **API Service:** `System.Net.Http.HttpClient` & `System.Text.Json`
* **Architecture:** Layered Object-Oriented Design (`Models`, `Services`, `Data`, `UI`)

---

## 📊 Environmental Metrics Monitored

| Metric | API Parameter | Unit | Target Component |
| :--- | :--- | :---: | :--- |
| **Air Temperature** | `temperature_2m` | °C | Stack Engine & Live Card |
| **Relative Humidity** | `relative_humidity_2m` | % | Live Card |
| **Wind Speed** | `wind_speed_10m` | km/h | Live Card |
| **Soil Temperature** | `soil_temperature_0cm` | °C | Live Card |

---

## 📦 How to Run the Application

### Prerequisites
* Windows 10/11 OS
* [Visual Studio 2022 / 2026](https://visualstudio.microsoft.com/) with the **.NET desktop development** workload installed.
* .NET 8.0 SDK or .NET 10.0 SDK installed.

### Execution Steps

1. **Clone the Repository:**
   ```bash
   git clone [https://github.com/pfunzom61-cmd/-SmartFarm-Iot.git](https://github.com/pfunzom61-cmd/-SmartFarm-Iot.git)
