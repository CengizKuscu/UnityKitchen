namespace UKitchen.Services
{
    public abstract class AbsServiceSignal<T> : IServiceSignal<T>
    {
        public T Response { get; set; }

        protected AbsServiceSignal(T response)
        {
            Response = response;
        }
    }
}