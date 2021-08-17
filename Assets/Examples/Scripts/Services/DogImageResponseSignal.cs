using Services.Model;
using UKitchen.Services;

namespace Services
{
    public class DogImageResponseSignal : AbsServiceSignal<DogImageData>
    {
        public DogImageResponseSignal(DogImageData response) : base(response)
        {
        }
    }
}