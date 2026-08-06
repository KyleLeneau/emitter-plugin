using System;
using System.Collections.Generic;

namespace Bortle.NINA.Emitter.Events {
    internal sealed class HandlerRegistry : IDisposable {
        private readonly List<IDisposable> _handlers = [];

        public void Add(IDisposable handler) => _handlers.Add(handler);

        public void Dispose() {
            foreach (var h in _handlers) h.Dispose();
        }
    }
}