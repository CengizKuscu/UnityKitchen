using System.Collections.Generic;
using MenuSystemSample;
using UKitchen.MenuSystem;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MenuPrefabInstaller", menuName = "Installers/MenuPrefabInstaller")]
public class MenuPrefabInstaller : ScriptableObjectInstaller<MenuPrefabInstaller>
{
    public List<AbsMenu<MenuName>> menus;
    
    public override void InstallBindings()
    {
        menus.ForEach(s =>
        {
            switch (s.menuName)
            {
                case MenuName.A:
                    Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<MenuA.Factory>();
                    Container.BindFactory<Transform, IMenuArgs, MenuA, MenuA.Factory>().FromFactory<MenuA.Factory>();
                    break;
                case MenuName.B:
                    Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<MenuB.Factory>();
                    Container.BindFactory<Transform, IMenuArgs, MenuB, MenuB.Factory>().FromFactory<MenuB.Factory>();
                    break;
                case MenuName.C:
                    Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<MenuC.Factory>();
                    Container.BindFactory<Transform, IMenuArgs, MenuC, MenuC.Factory>().FromFactory<MenuC.Factory>();
                    break;
                case MenuName.D:
                    Container.Bind<GameObject>().FromInstance(s.gameObject).WhenInjectedInto<MenuD.Factory>();
                    Container.BindFactory<Transform, IMenuArgs, MenuD, MenuD.Factory>().FromFactory<MenuD.Factory>();
                    break;
            }
        });
    }
}