namespace UKitchen.Services
{
    public enum ServiceError
    {
        None = 0,
        ConnectionError = 1,
        EmptyUrl = 2,
        NotResponse = 3,
        ConvertJSONError = 4,
        Error = 5,
        ProtocolError = 6,
        DataProcessingError = 7
    }
    
    public abstract class AbsServiceResponse
    {
        public ServiceError error = ServiceError.None;
    }
}