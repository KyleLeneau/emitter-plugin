using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using NINA.Core.Utility;
using NINA.WPF.Base.Interfaces.Mediator;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class ImageSaveHandler : IDisposable {
        private readonly IEventEmitter emitter;
        private readonly IImageSaveMediator service;

        public ImageSaveHandler(IEventEmitter eventEmitter, IImageSaveMediator imageSaveMediator) {
            emitter = eventEmitter;
            service = imageSaveMediator;
            service.BeforeFinalizeImageSaved += ServiceOnBeforeFinalizeImageSaved;
            service.BeforeImageSaved += ServiceOnBeforeImageSaved;
            service.ImageSaved += ServiceOnImageSaved;
        }

        private Task ServiceOnBeforeFinalizeImageSaved(object arg1, BeforeFinalizeImageSavedEventArgs arg2) {
            // TODO: Implement event
            Logger.Debug("on image before finalize");
            return Task.CompletedTask;
        }

        private Task ServiceOnBeforeImageSaved(object arg1, BeforeImageSavedEventArgs arg2) {
            // TODO: Implement event
            Logger.Debug("on image before saved");
            return Task.CompletedTask;
        }

        private void ServiceOnImageSaved(object sender, ImageSavedEventArgs e) {
            Logger.Debug("on image saved");
            var data = new ImageSavedData {
                FilePath = e.PathToImage.AbsolutePath,
                FileType = e.FileType.ToString(),
                ImageType = e.MetaData.Image.ImageType,
                IsBayered = e.IsBayered,
                ExposureDuration = e.Duration,
                PixelHeight = e.Image.PixelHeight,
                PixelWidth = e.Image.PixelWidth,
                Filter = e.Filter,
                ImageStatistics = new ImageStatistics {
                    BitDepth = e.Statistics.BitDepth,
                    Stdev = e.Statistics.StDev,
                    Mean = e.Statistics.Mean,
                    Median = e.Statistics.Median,
                    MedianAbsDev = e.Statistics.MedianAbsoluteDeviation,
                    Max = e.Statistics.Max,
                    MaxOccurrences = e.Statistics.MaxOccurrences,
                    Min = e.Statistics.Min,
                    MinOccurrences = e.Statistics.MinOccurrences,
                },
                StarStatistics = new StarStatistics {
                    Hfr = e.StarDetectionAnalysis.HFR,
                    HfrStdDev = e.StarDetectionAnalysis.HFRStDev,
                    DetectedStars = e.StarDetectionAnalysis.DetectedStars,
                }
            };
            emitter.Enqueue("image", "saved", data);
        }

        public void Dispose() {
            service.ImageSaved -= ServiceOnImageSaved;
            service.BeforeImageSaved -= ServiceOnBeforeImageSaved;
            service.BeforeFinalizeImageSaved -= ServiceOnBeforeFinalizeImageSaved;
        }
    }
}