namespace UKitchen.Pooling
{
    public interface IPrefabItem
    {
        void ReInitialize();

        void SetActive(bool val);

        void Dispose();
    }

    public interface IPrefabItem<TArgs> : IPrefabItem
    {
        TArgs Args { get; set; }
    }
}