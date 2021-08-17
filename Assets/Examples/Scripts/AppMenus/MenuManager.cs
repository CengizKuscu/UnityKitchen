using System;
using UKitchen.MenuSystem;
using UnityEngine;
using Zenject;

namespace AppMenus
{
    public class MenuManager : AbsMenuManager<MenuName>
    {
        [Inject] private MyFirstMenu.Factory _firstMenuFactory;
        [Inject] private SecondMenu.Factory _secondMenuFactory;
        [Inject] private ThirdMenu.Factory _thirdMenuFactory;
        [Inject] private OtherMenu.Factory _otherMenuFactory;
        [Inject] private PopupMenu.Factory _popupMenuFactory;

        [Inject]
        public void Construct()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();
            OpenMenu(MenuName.MyFirstMenu, new MyFirstMenuArgs());
        }

        public override bool OpenMenu(MenuName menuName, IMenuArgs args)
        {
            if (base.OpenMenu(menuName, args))
            {
                switch (menuName)
                {
                    case MenuName.MyFirstMenu:
                        Open(_firstMenuFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.SecondMenu:
                        Open(_secondMenuFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.ThirdMenu:
                        Open(_thirdMenuFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.OtherMenu:
                        Open(_otherMenuFactory.Create(menuContainer, args), args);
                        break;
                    case MenuName.PopupMenu:
                        Open(_popupMenuFactory.Create(popupContainer, args), args);
                        break;
                }

                return true;
            }

            return false;
        }
    }
}