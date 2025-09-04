using System;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public interface IReleasable
    {
        public event Action OnReleased;
    }
}