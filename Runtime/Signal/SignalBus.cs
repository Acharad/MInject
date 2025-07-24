using System;
using System.Collections.Generic;
using MInject.Runtime.Service;

namespace MInject.Runtime.Signal
{
    public class SignalBus : ServiceBase
    {
        private readonly Dictionary<Type, Delegate> _signalTable = new();
        
        public void Subscribe<T>(Action<T> callback) where T : ISignal
        {
            var type = typeof(T);
            if (_signalTable.TryGetValue(type, out var del))
            {
                _signalTable[type] = Delegate.Combine(del, callback);
            }
            else
            {
                _signalTable[type] = callback;
            }
        }

        public void Unsubscribe<T>(Action<T> callback) where T : ISignal
        {
            var type = typeof(T);
            if (_signalTable.TryGetValue(type, out var del))
            {
                del = Delegate.Remove(del, callback);
                if (del == null) _signalTable.Remove(type);
                else _signalTable[type] = del;
            }
        }

        public void Fire<T>(T signal) where T : ISignal
        {
            if (_signalTable.TryGetValue(typeof(T), out var del))
            {
                if (del is Action<T> callback)
                {
                    callback.Invoke(signal);
                }
            }
        }
    }
}
