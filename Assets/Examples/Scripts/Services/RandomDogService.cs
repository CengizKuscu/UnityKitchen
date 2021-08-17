using Services.Model;
using UKitchen.Services;
using Zenject;

namespace Services
{
    public class RandomDogService : AbsServiceManager
    {
        [Inject] private SignalBus _signal;
        
        public async void GetRandomDogImage()
        {
            var url = "https://dog.ceo/api/breeds/image/random";

            var sentTask = ResponseWithJson<DogImageData>(url, "GetRandomDogImage");

            await sentTask;
            
            _signal.Fire(new DogImageResponseSignal(sentTask.Result));
        }
    }
}