using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject;

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

        protected List<AbsMenu<TEnum>> menuLinkedList = new List<AbsMenu<TEnum>>();

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

        public abstract void OpenMenu(TEnum menuName, IMenuArgs args);
        

        public void Open(AbsMenu<TEnum> menu, IMenuArgs menuArgs)
        {
            loadingAnim?.SetActive(true);

            if (menuArgs.mode == MenuMode.Single && !menu.isPopup)
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
        

        public bool IsAlreadyOpen(TEnum menuName, IMenuArgs args, bool update)
        {
            AbsMenu<TEnum> menu = menuLinkedList.FirstOrDefault(s => Equals(s.menuName, menuName));
            
            if(update)
                menu?.UpdateMenu(args);

            return menu != null;
        }
        
        public bool IsAlreadyOpen(TEnum menuName)
        {
            AbsMenu<TEnum> menu = menuLinkedList.FirstOrDefault(s => Equals(s.menuName, menuName));

            return menu != null;
        }

        public void CloseMenu(TEnum menuName)
        {
            AbsMenu<TEnum> menu = menuLinkedList.FirstOrDefault(s => s.menuName.Equals(menuName));

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

            foreach (AbsMenu<TEnum> menu in menuLinkedList.ToList())
            {
                if (menu.destroyWhenClosed)
                    menuLinkedList.Remove(menu);
                menu.Close();
            }
        }
        
        public void CloseOthers(params TEnum[] menuNames)
        {
            if (menuLinkedList.Count == 0)
                return;
            
            foreach (AbsMenu<TEnum> menu in menuLinkedList.ToList())
            {
                if(menuNames.Contains(menu.menuName)) continue;
                if (menu.destroyWhenClosed)
                    menuLinkedList.Remove(menu);
                menu.Close();
            }
        }
    }
}