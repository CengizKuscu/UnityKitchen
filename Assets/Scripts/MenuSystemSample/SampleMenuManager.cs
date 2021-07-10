using System;
using UKitchen.MenuSystem;
using Zenject;

namespace MenuSystemSample
{
    public class SampleMenuManager : AbsMenuManager<MenuName>
    {
        [Inject] private MenuA.Factory _menuAFactory;
        [Inject] private MenuB.Factory _menuBFactory;
        [Inject] private MenuC.Factory _menuCFactory;
        [Inject] private MenuD.Factory _menuDFactory;

        [Inject]
        public void Construct()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
            OpenMenu(MenuName.A, new MenuAArg());
        }

        public override bool OpenMenu(MenuName menuName, IMenuArgs args)
        {
            if (base.OpenMenu(menuName, args))
            {
                switch (menuName)
                {
                    case MenuName.A:
                        Open(_menuAFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.B:
                        Open(_menuBFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.C:
                        Open(_menuCFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.D:
                        Open(_menuDFactory.Create(menuContainer, args), args);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(menuName), menuName, null);
                }
            }
            
            return false;
        }
    }
}