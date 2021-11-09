using Examples;
using Localizations;
using Services;
using UKitchen.MenuSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AppMenus
{
    public class MyFirstMenu : Menu<MenuName, MyFirstMenuArgs, MyFirstMenu>
    {
        [Inject] private readonly MenuManager _menuManager;
        [Inject] private readonly SignalBus _signal;
        [Inject] private RandomDogService _service;
        [Inject] private LocalizationSettings _localization;
        [SerializeField] private Text _txt;
        [SerializeField] private Text _serviceResultTxt;
        [SerializeField] private Text _localizationTxt;

        public void OnClick_RandomDogBtn()
        {
            _service.GetRandomDogImage();
        }
        
        protected override void OnShowBefore()
        {
            _signal.TryUnsubscribe<DogImageResponseSignal>(DogImageResult);
            _signal.Subscribe<DogImageResponseSignal>(DogImageResult);
            _txt.text = menuName.ToString();
            _localizationTxt.text = _localization.GetText("hello");
        }

        protected override void OnHideBefore()
        {
            _signal.TryUnsubscribe<DogImageResponseSignal>(DogImageResult);
        }

        private void DogImageResult(DogImageResponseSignal e)
        {
            _serviceResultTxt.text = e.Response.message;
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