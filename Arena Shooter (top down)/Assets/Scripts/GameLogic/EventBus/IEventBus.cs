using System;

namespace Assets.Scripts.GameLogic.GameEventBus
{
    public interface IEventBus
    {
        void Publish<T>(T eventData) where T : EventData;
        void Subscribe<T>(Action<T> handler) where T : EventData;
        void Unsubscribe<T>(Action<T> handler) where T : EventData;
    }
}