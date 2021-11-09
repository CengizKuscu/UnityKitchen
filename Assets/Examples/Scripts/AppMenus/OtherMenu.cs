using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AppMenus
{
    public class OtherMenu : Menu<MenuName, OtherMenuArgs, OtherMenu>
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

    public class OtherMenuArgs : MenuArgs
    {
        public OtherMenuArgs(MenuMode mode)
        {
            this.mode = mode;
        }
    }
}