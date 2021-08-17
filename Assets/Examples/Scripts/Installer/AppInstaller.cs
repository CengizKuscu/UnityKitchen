using Localizations;
using Services;
using Zenject;

namespace Examples.Scripts.Installer
{
    public class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LocalizationManager>().AsSingle();
            
            Container.Bind<RandomDogService>().AsSingle();

            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<DogImageResponseSignal>();
        }
    }
}