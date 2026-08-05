using Bortle.NINA.Emitter.EventSinks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.Events {

    public interface IEventEmitter : IAsyncDisposable {
        IReadOnlyList<IEventSink> Sinks { get; }

        Task StartAsync(CancellationToken ct = default);

        void Enqueue<T>(string domain, string eventType, T data);

        /// <summary>
        /// Connects <paramref name="sink"/> and installs it under its <see cref="IEventSink.Name"/>,
        /// replacing (and disconnecting/disposing) any existing sink registered under that name.
        /// Used to apply sink configuration changes at runtime, e.g. from the plugin options UI.
        /// </summary>
        Task SetSinkAsync(IEventSink sink, CancellationToken ct = default);

        /// <summary>
        /// Disconnects and removes the sink registered under <paramref name="name"/>, if any.
        /// </summary>
        Task RemoveSinkAsync(string name, CancellationToken ct = default);
    }
}