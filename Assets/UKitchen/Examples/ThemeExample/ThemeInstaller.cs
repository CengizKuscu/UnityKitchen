using UnityEngine;
using Zenject;

namespace UKitchen.Themes
{
    [CreateAssetMenu(fileName = "ThemeInstaller", menuName = "UnityKitchen/Installers/ThemeInstaller")]
    public class ThemeInstaller : ScriptableObjectInstaller<ThemeInstaller>
    {
        public AppThemes themes;

        public PaletteName selectedPalette;

        public override void InstallBindings()
        {
            Container.BindInstance(selectedPalette);
            Container.BindInstance(themes);
        }
    }
}