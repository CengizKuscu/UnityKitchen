using UnityEngine;
using Zenject;

namespace UKitchen.Examples.LocalizationExample
{
    [CreateAssetMenu(fileName = "LocalizationInstaller", menuName = "UnityKitchen/Installers/LocalizationInstaller")]
    public class LocalizationInstaller : ScriptableObjectInstaller<LocalizationInstaller>
    {
        public LocalizationSettings settings;

        public override void InstallBindings()
        {
            Container.BindInstance(settings);
        }
    }
}