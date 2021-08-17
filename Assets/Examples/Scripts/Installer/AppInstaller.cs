using Services;
using Zenject;

namespace Examples.Scripts.Installer
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RandomDogService>().AsSingle();

            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<DogImageResponseSignal>();
        }
    }
}