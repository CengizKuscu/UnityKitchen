namespace UKitchen.Services
{
    public interface IServiceSignal
    {
    }

    public interface IServiceSignal<T> : IServiceSignal
    {
        T Response { get; set; }
    }
}