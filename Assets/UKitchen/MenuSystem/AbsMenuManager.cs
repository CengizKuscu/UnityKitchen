using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.MenuSystem
{
    public abstract class AbsMenuManager<TEnum> : MonoBehaviour, IMenuManager<TEnum> where TEnum : struct, Enum
    {
        [SerializeField] private Transform _menuContainer;
        [SerializeField] private Transform _popupContainer;
        [SerializeField] private GameObject _loadingAnim;

        public Transform menuContainer => _menuContainer;
        public Transform popupContainer => _popupContainer;
        public GameObject loadingAnim => _loadingAnim;

        protected List<IMenu<TEnum>> menuLinkedList = new List<IMenu<TEnum>>();

        public virtual void Init()
        {
            loadingAnim?.SetActive(false);

            foreach (Transform o in menuContainer?.transform)
            {
                o.gameObject.SetActive(false);
            }

            foreach (Transform o in popupContainer?.transform)
            {
                o.gameObject.SetActive(false);
            }
        }

        public void Open(IMenu<TEnum> menu, IMenuArgs menuArgs)
        {
            loadingAnim?.SetActive(true);

            if (menuArgs.mode == MenuMode.Single)
                CloseOthers();

            if (menuLinkedList.FirstOrDefault(s=>s.menuName.Equals(menu.menuName)) != null)
            {
                loadingAnim?.SetActive(false);
                return;
            }

            menuLinkedList.Add(menu);
            
            menu.Open();
            loadingAnim?.SetActive(false);
        }

        public virtual bool OpenMenu(TEnum menuName, IMenuArgs args)
        {
            IMenu<TEnum> menu = menuLinkedList.FirstOrDefault(s => Equals(s.menuName, menuName));

            if (menu != null)
            {
                menu.Open();

                return false;
            }

            return true;
        }

        public void CloseMenu(TEnum menuName)
        {
            IMenu<TEnum> menu = menuLinkedList.FirstOrDefault(s => s.menuName.Equals(menuName));

            if (menu != null)
            {
                if (menu.destroyWhenClosed)
                    menuLinkedList.Remove(menu);
                menu.Close();
            }
        }

        public void CloseOthers()
        {
            if (menuLinkedList.Count == 0)
                return;

            foreach (IMenu<TEnum> menu in menuLinkedList.ToList())
            {
                if (menu.destroyWhenClosed)
                    menuLinkedList.Remove(menu);
                menu.Close();
            }
        }
    }
}