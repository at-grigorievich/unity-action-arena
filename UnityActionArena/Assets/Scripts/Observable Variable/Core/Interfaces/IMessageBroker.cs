using System;

namespace ATG.Observable
{
    public interface IMessageBroker
    {
        void Send<T>(T message) where T : IMessage;
        ObserveDisposable Subscribe<T>(Action<IMessage> receiver) where T : IMessage;

        void Dispose();
    }
}
