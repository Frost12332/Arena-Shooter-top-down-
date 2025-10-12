namespace Assets.Scripts.GameLogic.GameResource
{
    public interface IResourceStorage
    {
        ResourceType GetResourceType();
        void Restore();
        void Add(float amount);
        bool TrySpend(float amount);
        bool HasEnough(float amount);
    }
}