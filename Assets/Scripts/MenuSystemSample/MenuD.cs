using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MenuSystemSample
{
    public class MenuD : Menu<MenuName, MenuDArg, MenuD>
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
        
        public void OnClick_CloseBtn()
        {
            _menuManager.CloseMenu(menuName);
        }
    }
    
    public class MenuDArg : MenuArgs
    {
        public MenuDArg()
        {
            mode = MenuMode.Additive;
        }
    }
}