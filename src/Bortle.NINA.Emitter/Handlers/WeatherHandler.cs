using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using Bortle.NINA.Emitter.Utils;
using NINA.Equipment.Equipment.MyWeatherData;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {

    public class WeatherHandler : IWeatherDataConsumer {
        private readonly IEventEmitter emitter;
        private readonly IWeatherDataMediator mediator;
        private WeatherDataInfo lastInfo;

        public WeatherHandler(IEventEmitter eventEmitter, IWeatherDataMediator weatherDataMediator) {
            emitter = eventEmitter;
            mediator = weatherDataMediator;
            mediator.RegisterConsumer(this);
            mediator.Connected += MediatorOnConnected;
            mediator.Disconnected += MediatorOnDisconnected;
        }

        public void UpdateDeviceInfo(WeatherDataInfo deviceInfo) {
            // Skip duplicates from internal nina polling
            if (deviceInfo.Equals(lastInfo)) return;

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

            emitter.Enqueue("weather", "device-info", data);
            lastInfo = deviceInfo;
        }

        private Task MediatorOnConnected(object arg1, EventArgs arg2) {
            var deviceInfo = mediator.GetInfo();
            var data = new DeviceConnectionData {
                Connected = true,
                DeviceType = "Weather",
                Name = deviceInfo.Name,
                Description = deviceInfo.Description,
                DriverInfo = deviceInfo.DriverInfo,
                DriverVersion = deviceInfo.DriverVersion,
                DeviceId = deviceInfo.DeviceId
            };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        private Task MediatorOnDisconnected(object arg1, EventArgs arg2) {
            var data = new DeviceConnectionData { Connected = false, DeviceType = "Weather" };
            emitter.Enqueue("device", "connection", data);
            return Task.CompletedTask;
        }

        public void Dispose() {
            mediator.Disconnected -= MediatorOnDisconnected;
            mediator.Connected -= MediatorOnConnected;
            mediator.RemoveConsumer(this);
        }
    }
}