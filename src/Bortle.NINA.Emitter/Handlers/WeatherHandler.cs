using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using Bortle.NINA.Emitter.Utils;
using NINA.Equipment.Equipment.MyWeatherData;
using NINA.Equipment.Interfaces.Mediator;

namespace Bortle.NINA.Emitter.Handlers {

    public class WeatherHandler : IWeatherDataConsumer {
        private readonly IEventEmitter emitter;
        private readonly IWeatherDataMediator weatherDataMediator;

        public WeatherHandler(IEventEmitter emitter, IWeatherDataMediator weatherDataMediator) {
            this.emitter = emitter;
            this.weatherDataMediator = weatherDataMediator;
            this.weatherDataMediator.RegisterConsumer(this);
        }

        public void UpdateDeviceInfo(WeatherDataInfo deviceInfo) {
            var data = new WeatherData {
                Connected = deviceInfo.Connected,
                Temperature = deviceInfo.Temperature.Optional(),
                Humidity = deviceInfo.Humidity.Optional(),
                DewPoint = deviceInfo.DewPoint.Optional(),
                Pressure = deviceInfo.Pressure.Optional(),
                WindSpeed = deviceInfo.WindSpeed.Optional(),
                WindDirection = deviceInfo.WindDirection.Optional(),
                WindGust = deviceInfo.WindGust.Optional(),
                SkyTemperature = deviceInfo.SkyTemperature.Optional(),
                SkyQuality = deviceInfo.SkyQuality.Optional(),
                RainRate = deviceInfo.RainRate.Optional(),
                CloudCover = deviceInfo.CloudCover.Optional(),
                StarFwhm = deviceInfo.StarFWHM.Optional()
            };

            this.emitter.Enqueue("weather", "device-info", data);
        }

        public void Dispose() {
            this.weatherDataMediator.RemoveConsumer(this);
        }
    }
}