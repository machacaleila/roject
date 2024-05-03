using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Weather
{
    class WeatherInfo
    {
        public class City
        {
            public double Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
        }

        public class Coord
        {
            public double Lat { get; set; }
            public double Lon { get; set; }
        }

        public class Dt
        {
            public double DtValue { get; set; } // Renamed to avoid naming conflict with class name
        }

        public class Weather
        {
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class Main
        {
            public double Temp { get; set; }
            public double Pressure { get; set; }
            public double Humidity { get; set; }
            public double TempMin { get; set; }
            public double TempMax { get; set; }
        }

        public class Wind
        {
            public double Speed { get; set; }
        }

        public class All
        {
            public City City { get; set; }
            public Coord Coord { get; set; }
            public List<Weather> Weather { get; set; }
            public Main Main { get; set; }
            public Wind Wind { get; set; }
            public Dt Dt { get; set; }
        }

        public class Application
        {
            public static async Task Main(string[] args)
            {
                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.openweathermap.org/data/2.5/forecast?q=ephraim&appid=503640c14dc34b096c0c55618ff38825");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<All>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Console.WriteLine($"Weather Forecast for {weatherData.City.Name}, {weatherData.City.Country}:");
                foreach (var forecast in weatherData.Weather)
                {
                    DateTime date = DateTimeOffset.FromUnixTimeSeconds((long)weatherData.Dt.DtValue).DateTime;
                    Console.WriteLine($"{date.ToShortDateString()} - Temp: {weatherData.Main.Temp}K, Weather: {forecast.Main} ({forecast.Description})");
                }
            }
        }
    }
}