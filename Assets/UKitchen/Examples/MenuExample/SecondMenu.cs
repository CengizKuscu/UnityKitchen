using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UKitchen.Examples.MenuExample
{
    public class SecondMenu : Menu<MenuName, SecondMenuArgs, SecondMenu>
    {
        [Inject] private ExampleMenuManager _menuManager;
        [SerializeField] private Text _txt;

        protected override void OnShowBefore<TMenuArgs>(TMenuArgs e)
        {
            _txt.text = menuName.ToString();
        }

        public void OnClick_CloseBtn()
        {
            _menuManager.CloseMenu(menuName);
        }
    }

    public class SecondMenuArgs : MenuArgs
    {
        public SecondMenuArgs()
        {
        }
    }
}