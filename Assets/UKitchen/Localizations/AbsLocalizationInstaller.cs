using UKitchen.Localizations.Model;
using UnityEngine;
using Zenject;

namespace UKitchen.Localizations
{
    
    public abstract class
        AbsLocalizationInstaller<TWord, TSettings> : ScriptableObjectInstaller<
            AbsLocalizationInstaller<TWord, TSettings>> where TWord : AbsWord
        where TSettings : AbsLocalizationSettings<TWord>
    {
        public TSettings settings;

        public override void InstallBindings()
        {
            Container.BindInstance(settings);
        }
    }
}