using Bortle.NINA.Emitter.Events;
using Bortle.NINA.Emitter.Models;
using Bortle.NINA.Emitter.Utils;
using NINA.Core.Utility;
using NINA.Plugin.Interfaces;
using System;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Handlers {
    public class TargetSchedulerHandler : IDisposable, ISubscriber {
        private readonly Guid SenderId = Guid.Parse("B4541BA9-7B07-4D71-B8E1-6C73D4933EA0");
        private const string TopicWaitStart = "TargetScheduler-WaitStart";
        private const string TopicNewTargetStart = "TargetScheduler-NewTargetStart";
        private const string TopicTargetStart = "TargetScheduler-TargetStart";
        private const string TopicContatinerStopped = "TargetScheduler-ContainerStopped";
        private const string TopicTargetComplete = "TargetScheduler-TargetComplete";

        private readonly IEventEmitter emitter;
        private readonly IMessageBroker broker;

        public TargetSchedulerHandler(IEventEmitter eventEmitter, IMessageBroker messageBroker) {
            emitter = eventEmitter;
            broker = messageBroker;

            // WARN: this message broker might go away in the future update
            broker.Subscribe(TopicWaitStart, this);
            broker.Subscribe(TopicNewTargetStart, this);
            broker.Subscribe(TopicTargetStart, this);
            broker.Subscribe(TopicContatinerStopped, this);
            broker.Subscribe(TopicTargetComplete, this);
        }

        public Task OnMessageReceived(IMessage message) {
            // TODO: Implement event

            //
            // https://tcpalmer.github.io/nina-scheduler/adv-topics/pub-sub.html
            //

            if (!message.SenderId.Equals(SenderId)) {
                // Ignore other senders here, only TargetScheduler
                return Task.CompletedTask;
            }

            switch (message.Topic) {
                case TopicWaitStart:
                    if (message.Version != 2) { break; }

                    // Version: 2
                    // Content: (DateTime) wait end time
                    // Expiration: wait end time
                    // Headers:
                    //     SecondsUntilNextTarget: (int) number of seconds to wait until the next target starts
                    //     ProjectName: (string) TS project name for the next target
                    //     TargetName: (string) TS name for the next target
                    //     Coordinates: (NINA.Astrometry.Coordinates) coordinates of the next target
                    //     Rotation: (double) next target rotation angle (as entered into the TS UI)
                    try {
                        var data = new TargetSchedulerWaitStartData {
                            WaitEndTime = (DateTime)message.Content,
                            SecsTillNextTarget = (int)message.CustomHeaders["SecondsUntilNextTarget"],
                            ProjectName = (string)message.CustomHeaders["ProjectName"],
                            TargetName = (string)message.CustomHeaders["TargetName"],
                            Coordinates =
                                AstrometryCoordinates.ToCoordinates(
                                    (global::NINA.Astrometry.Coordinates)message.CustomHeaders["Coordinates"]),
                            Rotation = (double)message.CustomHeaders["Rotation"],
                        };
                        emitter.Enqueue("target-scheduler", "wait-start", data);
                    } catch (Exception e) {
                        Logger.Error("Error decoding TS wait start message", e);
                    }
                    break;
                case TopicNewTargetStart:
                    if (message.Version != 2) { break; }

                    // Version: 2
                    // Content: (string) target name
                    // Expiration: target end time (see below)
                    // Headers:
                    //     ProjectName: (string) TS project name for the target
                    //     Coordinates: (NINA.Astrometry.Coordinates) coordinates of the target
                    //     Rotation: (double) target rotation angle (as entered into the TS UI)
                    //     ExposureFilterName: (string) name of the filter for the selected exposure
                    //     ExposureLength: (double) length of the selected exposure in seconds
                    //     ExposureGain: (string) gain setting of the selected exposure or ‘(camera)’ if defaults to camera setting
                    //     ExposureOffset: (string) offset setting of the selected exposure or ‘(camera)’ if defaults to camera setting
                    //     ExposureBinning: (string) binning mode of the selected exposure, e.g. ‘1x1’
                    try {
                        var data = new TargetSchedulerNewTargetStartData {
                            TargetName = (string)message.CustomHeaders["TargetName"],
                            TargetEndTime = message.Expiration,
                            ProjectName = (string)message.CustomHeaders["ProjectName"],
                            Coordinates =
                                AstrometryCoordinates.ToCoordinates(
                                    (global::NINA.Astrometry.Coordinates)message.CustomHeaders["Coordinates"]),
                            Filter = (string)message.CustomHeaders["Filter"],
                            Exposure = (double)message.CustomHeaders["Exposure"],
                            Gain = (string)message.CustomHeaders["Gamma"],
                            Offset = (string)message.CustomHeaders["Offset"],
                            Binning = (string)message.CustomHeaders["Binning"],
                        };
                        emitter.Enqueue("target-scheduler", "new-target-start", data);
                    } catch (Exception e) {
                        Logger.Error("Error decoding TS new target start message", e);
                    }
                    break;
                case TopicTargetStart:
                    if (message.Version != 2) { break; }

                    // Version: 2
                    // Content: (string) target name
                    // Expiration: target end time (see below)
                    // Headers:
                    //     ProjectName: (string) TS project name for the target
                    //     Coordinates: (NINA.Astrometry.Coordinates) coordinates of the target
                    //     Rotation: (double) target rotation angle (as entered into the TS UI)
                    //     ExposureFilterName: (string) name of the filter for the selected exposure
                    //     ExposureLength: (double) length of the selected exposure in seconds
                    //     ExposureGain: (string) gain setting of the selected exposure or ‘(camera)’ if defaults to camera setting
                    //     ExposureOffset: (string) offset setting of the selected exposure or ‘(camera)’ if defaults to camera setting
                    //     ExposureBinning: (string) binning mode of the selected exposure, e.g. ‘1x1’
                    try {
                        var data = new TargetSchedulerTargetStartData {
                            TargetName = (string)message.CustomHeaders["TargetName"],
                            TargetEndTime = message.Expiration,
                            ProjectName = (string)message.CustomHeaders["ProjectName"],
                            Coordinates =
                                AstrometryCoordinates.ToCoordinates(
                                    (global::NINA.Astrometry.Coordinates)message.CustomHeaders["Coordinates"]),
                            Filter = (string)message.CustomHeaders["Filter"],
                            Exposure = (double)message.CustomHeaders["Exposure"],
                            Gain = (string)message.CustomHeaders["Gamma"],
                            Offset = (string)message.CustomHeaders["Offset"],
                            Binning = (string)message.CustomHeaders["Binning"],
                        };
                        emitter.Enqueue("target-scheduler", "new-target-start", data);
                    } catch (Exception e) {
                        Logger.Error("Error decoding TS new target start message", e);
                    }
                    break;
                case TopicContatinerStopped:
                    if (message.Version != 1) { break; }

                    // Version: 1
                    // Content: (string) “Container Stopped”
                    // Expiration: n/a
                    // Headers:
                    //     StoppedAt: (DateTime) time that the container stopped
                    try {
                        var data = new TargetSchedulerContainerStoppedData {
                            StoppedAt = (DateTime)message.CustomHeaders["StoppedAt"],
                        };
                        emitter.Enqueue("target-scheduler", "container-stopped", data);
                    } catch (Exception e) {
                        Logger.Error("Error decoding TS new target start message", e);
                    }
                    break;
                case TopicTargetComplete:
                    if (message.Version != 1) { break; }

                    // Version: 1
                    // Content: (string) target name
                    // Expiration: none
                    // Headers:
                    //     ProjectName: (string) TS project name for the target
                    //     Coordinates: (NINA.Astrometry.Coordinates) coordinates of the target
                    //     Rotation: (double) target rotation angle (as entered into the TS UI)
                    try {
                        var data = new TargetSchedulerTargetCompleteData {
                            TargetName = (string)message.Content,
                            ProjectName = (string)message.CustomHeaders["ProjectName"],
                            Coordinates =
                                AstrometryCoordinates.ToCoordinates(
                                    (global::NINA.Astrometry.Coordinates)message.CustomHeaders["Coordinates"]),
                            Rotation = (double)message.CustomHeaders["Rotation"],
                        };
                        emitter.Enqueue("target-scheduler", "target-complete", data);
                    } catch (Exception e) {
                        Logger.Error("Error decoding TS new target start message", e);
                    }
                    break;
            }

            return Task.CompletedTask;
        }

        public void Dispose() {
            broker.Unsubscribe(TopicWaitStart, this);
            broker.Unsubscribe(TopicNewTargetStart, this);
            broker.Unsubscribe(TopicTargetStart, this);
            broker.Unsubscribe(TopicContatinerStopped, this);
            broker.Unsubscribe(TopicTargetComplete, this);
        }
    }
}