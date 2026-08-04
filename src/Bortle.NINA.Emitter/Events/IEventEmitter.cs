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
    }
}