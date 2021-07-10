using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MenuSystemSample
{
    public class MenuA : Menu<MenuName, MenuAArg, MenuA>
    {
        [Inject] private SampleMenuManager _menuManager;
        [SerializeField] private Text _txt;
        
        public override void OnShowBefore<TMenuArgs>(TMenuArgs e)
        {
            base.OnShowBefore(e);
            _txt.text = menuName.ToString();
        }

        public override void OnShowAfter<TMenuArgs>(TMenuArgs e)
        {
            base.OnShowAfter(e);
        }

        
        public override void OnHideBefore()
        {
            base.OnHideBefore();
        }

        public override void OnHideAfter()
        {
            base.OnHideAfter();
        }

        public void OnClick_MenuBBtn()
        {
            _menuManager.OpenMenu(MenuName.B, new MenuBArg());
        }
        
        public void OnClick_MenuCBtn()
        {
            _menuManager.OpenMenu(MenuName.C, new MenuCArg());
        }
        
        public void OnClick_MenuDBtn()
        {
            _menuManager.OpenMenu(MenuName.D, new MenuDArg());
        }
    }

    public class MenuAArg : MenuArgs
    {
        public MenuAArg()
        {
            mode = MenuMode.Single;
        }
    }
}