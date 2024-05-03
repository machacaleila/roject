using NUnit.Framework;
using Weather;
using System.Text.Json;

namespace Weather.Tests
{
    [TestFixture]
    public class WeatherInfoTests
    {
        [Test]
        public void Deserialize_AllData_ShouldReturnCorrectCityName()
        {
            string json = "{\"city\":{\"name\":\"California\",\"country\":\"US\"},\"weather\":[{\"main\":\"Cloudy\",\"description\":\"overcast clouds\"}]}";
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<WeatherInfo.All>(json, options);
            
            Assert.AreEqual("California", result.City.Name);
        }
    }
}
