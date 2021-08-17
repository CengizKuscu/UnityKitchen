using System;
using UnityEngine;
using Zenject;

namespace UKitchen.Themes
{
    [Serializable]
    public abstract class AbsThemeInstaller<TThemeName, TTheme> : ScriptableObjectInstaller<AbsThemeInstaller<TThemeName, TTheme>> where TThemeName : Enum
    {
        //public T themes;

        public TThemeName selectedTheme;

        public TTheme themeSettings;
        

        public override void InstallBindings()
        {
            Container.BindInstance(selectedTheme);
            Container.BindInstance(themeSettings);
            //Container.BindInstance(themes);
        }
    }
}