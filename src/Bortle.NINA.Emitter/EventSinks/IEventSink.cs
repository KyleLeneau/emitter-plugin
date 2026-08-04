using Bortle.NINA.Emitter.Events;
using CloudNative.CloudEvents;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bortle.NINA.Emitter.EventSinks {

    public interface IEventSink {
        string Name { get; }

        ConnectionState State { get; }

        event EventHandler<ConnectionState> StateChanged;

        Task ConnectAsync(CancellationToken ct);

        Task DisconnectAsync(CancellationToken ct);

        Task SendAsync(CloudEvent evt, CancellationToken ct);
    }
}
