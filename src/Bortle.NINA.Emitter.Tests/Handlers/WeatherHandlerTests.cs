using Bortle.NINA.Emitter.Handlers;
using Bortle.NINA.Emitter.Models;
using Bortle.NINA.Emitter.Tests.Fakes;
using NINA.Equipment.Equipment.MyWeatherData;

namespace Bortle.NINA.Emitter.Tests.Handlers
{

    public class WeatherHandlerTests
    {
        [Fact]
        public void Constructor_RegistersItselfAsConsumer()
        {
            var mediator = new FakeWeatherDataMediator();
            var handler = new WeatherHandler(new FakeEventEmitter(), mediator);

            Assert.Contains(handler, mediator.RegisteredConsumers);
        }

        [Fact]
        public void Dispose_RemovesItselfAsConsumer()
        {
            var mediator = new FakeWeatherDataMediator();
            var handler = new WeatherHandler(new FakeEventEmitter(), mediator);

            handler.Dispose();

            Assert.DoesNotContain(handler, mediator.RegisteredConsumers);
        }

        [Fact]
        public void UpdateDeviceInfo_EnqueuesWeatherDeviceInfoEvent()
        {
            var emitter = new FakeEventEmitter();
            var handler = new WeatherHandler(emitter, new FakeWeatherDataMediator());

            var info = new WeatherDataInfo
            {
                Connected = true,
                Temperature = 12.3,
                Humidity = 55.0,
                DewPoint = 4.2,
                Pressure = 1013.0,
                WindSpeed = 3.1,
                WindDirection = 180.0,
                WindGust = 5.5,
                SkyTemperature = -10.0,
                SkyQuality = 21.5,
                RainRate = 0.0,
                CloudCover = 25.0,
                StarFWHM = 2.1
            };

            handler.UpdateDeviceInfo(info);

            var call = Assert.Single(emitter.Calls);
            Assert.Equal("weather", call.Domain);
            Assert.Equal("device-info", call.EventType);

            var data = Assert.IsType<WeatherDeviceInfoData>(call.Data);
            Assert.True(data.Connected);
            Assert.Equal(12.3, data.Temperature);
            Assert.Equal(55.0, data.Humidity);
            Assert.Equal(4.2, data.DewPoint);
            Assert.Equal(1013.0, data.Pressure);
            Assert.Equal(3.1, data.WindSpeed);
            Assert.Equal(180.0, data.WindDirection);
            Assert.Equal(5.5, data.WindGust);
            Assert.Equal(-10.0, data.SkyTemperature);
            Assert.Equal(21.5, data.SkyQuality);
            Assert.Equal(0.0, data.RainRate);
            Assert.Equal(25.0, data.CloudCover);
            Assert.Equal(2.1, data.StarFwhm);
        }

        [Fact]
        public void UpdateDeviceInfo_MapsUnsupportedSensorNaNValuesToNull()
        {
            var emitter = new FakeEventEmitter();
            var handler = new WeatherHandler(emitter, new FakeWeatherDataMediator());

            var info = new WeatherDataInfo
            {
                Connected = false,
                Temperature = double.NaN,
                Humidity = double.NaN,
                DewPoint = double.NaN,
                Pressure = double.NaN,
                WindSpeed = double.NaN,
                WindDirection = double.NaN,
                WindGust = double.NaN,
                SkyTemperature = double.NaN,
                SkyQuality = double.NaN,
                RainRate = double.NaN,
                CloudCover = double.NaN,
                StarFWHM = double.NaN
            };

            handler.UpdateDeviceInfo(info);

            var call = Assert.Single(emitter.Calls);
            var data = Assert.IsType<WeatherDeviceInfoData>(call.Data);

            Assert.False(data.Connected);
            Assert.Null(data.Temperature);
            Assert.Null(data.Humidity);
            Assert.Null(data.DewPoint);
            Assert.Null(data.Pressure);
            Assert.Null(data.WindSpeed);
            Assert.Null(data.WindDirection);
            Assert.Null(data.WindGust);
            Assert.Null(data.SkyTemperature);
            Assert.Null(data.SkyQuality);
            Assert.Null(data.RainRate);
            Assert.Null(data.CloudCover);
            Assert.Null(data.StarFwhm);
        }
    }
}
