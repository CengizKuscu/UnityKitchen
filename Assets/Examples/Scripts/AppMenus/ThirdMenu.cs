using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AppMenus
{
    public class ThirdMenu : Menu<MenuName, ThirdMenuArgs, ThirdMenu>
    {
        [Inject] private MenuManager _menuManager;
        [SerializeField] private Text _txt;

        protected override void OnShowBefore()
        {
            _txt.text = menuName.ToString();
        }

        public void OnClick_CloseBtn()
        {
            _menuManager.CloseMenu(menuName);
        }
    }

    public class ThirdMenuArgs : MenuArgs
    {
        public ThirdMenuArgs()
        {
            mode = MenuMode.Additive;
        }
    }
}