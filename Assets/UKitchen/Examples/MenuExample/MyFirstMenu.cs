using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Examples.MenuExample
{
    public class MyFirstMenu : Menu<MenuName, MyFirstMenuArgs, MyFirstMenu>
    {
        [Inject] private ExampleMenuManager _menuManager;
        [SerializeField] private Text _txt;

        protected override void OnShowBefore<TMenuArgs>(TMenuArgs e)
        {
            _txt.text = menuName.ToString();
        }

        public void OnClick_SecondMenuBtn()
        {
            _menuManager.OpenMenu(MenuName.SecondMenu, new SecondMenuArgs {mode = MenuMode.Additive});
        }

        public void OnClick_ThirdMenuBtn()
        {
            _menuManager.OpenMenu(MenuName.ThirdMenu, new ThirdMenuArgs());
        }

        public void OnClick_OtherMenuBtn()
        {
            _menuManager.OpenMenu(MenuName.OtherMenu, new OtherMenuArgs(MenuMode.Additive));
        }

        public void OnClick_PopupMenuBtn()
        {
            _menuManager.OpenMenu(MenuName.PopupMenu, new PopupMenuArgs());
        }
    }

    public class MyFirstMenuArgs : MenuArgs
    {
        public MyFirstMenuArgs()
        {
            mode = MenuMode.Single;
        }
    }
}