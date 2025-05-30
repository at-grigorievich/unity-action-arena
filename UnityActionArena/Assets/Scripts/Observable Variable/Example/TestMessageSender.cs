using UnityEngine;

namespace ATG.Observable
{
    public class TestMessageSender: MonoBehaviour
    {
        private IMessageBroker _broker;

        public TestMessageSender(IMessageBroker broker)
        {
            _broker = broker;
        }

        [ContextMenu("Send test message")]
        public void SendTestMessage()
        {
            _broker.Send<TestMessage>(new TestMessage());
        }
    }
}
