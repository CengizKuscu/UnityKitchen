using System.Collections.Generic;
using UKitchen.MenuSystem;
using UnityEngine;
using Zenject;

namespace AppMenus
{
    [CreateAssetMenu(fileName = "MenuInstaller", menuName = "UnityKitchen/Installers/MenuInstaller")]
    public class MenuInstaller : ScriptableObjectInstaller<MenuInstaller>
    {
        public List<AbsMenu<MenuName>> menus;
        
        public override void InstallBindings()
        {
            menus.ForEach(s =>
            {
                switch (s.menuName)
                {
                    case MenuName.MyFirstMenu:
                        Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<MyFirstMenu.Factory>();
                        Container.BindFactory<Transform, IMenuArgs, MyFirstMenu, MyFirstMenu.Factory>()
                            .FromFactory<MyFirstMenu.Factory>();
                        break;
                    case MenuName.SecondMenu:
                        Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<SecondMenu.Factory>();
                        Container.BindFactory<Transform, IMenuArgs, SecondMenu, SecondMenu.Factory>()
                            .FromFactory<SecondMenu.Factory>();
                        break;
                    case MenuName.ThirdMenu:
                        Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<ThirdMenu.Factory>();
                        Container.BindFactory<Transform, IMenuArgs, ThirdMenu, ThirdMenu.Factory>()
                            .FromFactory<ThirdMenu.Factory>();
                        break;
                    case MenuName.OtherMenu:
                        Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<OtherMenu.Factory>();
                        Container.BindFactory<Transform, IMenuArgs, OtherMenu, OtherMenu.Factory>()
                            .FromFactory<OtherMenu.Factory>();
                        break;
                    case MenuName.PopupMenu:
                        Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<PopupMenu.Factory>();
                        Container.BindFactory<Transform, IMenuArgs, PopupMenu, PopupMenu.Factory>()
                            .FromFactory<PopupMenu.Factory>();
                        break;
                }
            });
        }
    }
}