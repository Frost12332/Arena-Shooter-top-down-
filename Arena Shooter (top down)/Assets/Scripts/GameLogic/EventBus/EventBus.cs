using System;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic.GameEventBus
{
    public class EventBus : IEventBus
    {
        private static class TypedEventBus<T> where T : EventData
        {
            private static event Action<T> Handlers;

            public static void Publish(T eventData) => Handlers?.Invoke(eventData);
            public static void Subscribe(Action<T> handler) => Handlers += handler;
            public static void Unsubscribe(Action<T> handler) => Handlers -= handler;
        }

        public void Subscribe<T>(Action<T> handler) where T : EventData
            => TypedEventBus<T>.Subscribe(handler);

        public void Unsubscribe<T>(Action<T> handler) where T : EventData
            => TypedEventBus<T>.Unsubscribe(handler);

        public void Publish<T>(T eventData) where T : EventData
            => TypedEventBus<T>.Publish(eventData);
    }
}