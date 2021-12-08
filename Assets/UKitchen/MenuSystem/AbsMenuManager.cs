using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UKitchen.Logger;
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

        private List<string> _loadingKeys;

        
        protected List<AbsMenu<TEnum>> menuLinkedList = new List<AbsMenu<TEnum>>();

        public virtual void Init()
        {
            _loadingAnim.SetActive(false);

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
            AddLoading(menu.menuName.ToString());

            if (menuArgs.mode == MenuMode.Single && !menu.isPopup)
                CloseOthers();

            if (menuLinkedList.FirstOrDefault(s=>s.menuName.Equals(menu.menuName)) != null)
            {
                RemoveLoading(menu.menuName.ToString());
                return;
            }

            menuLinkedList.Add(menu);
            
            menu.Open();
            RemoveLoading(menu.menuName.ToString());
        }

        public void AddLoading(string key)
        {
            _loadingKeys ??= new List<string>();
            
            _loadingKeys.Add(key);
            
            AppLog.Info(this, "ADD LOADING", _loadingKeys.Count, key);
            
            _loadingAnim.SetActive(_loadingKeys.Any());
        }

        public void RemoveLoading(string key)
        {
            _loadingKeys ??= new List<string>();

            var str = _loadingKeys.FirstOrDefault(s => s.Equals(key));

            if (!string.IsNullOrEmpty(str))
            {
                _loadingKeys.Remove(str);
            }
            
            AppLog.Info(this, "REMOVE LOADING", _loadingKeys.Count, key);
            
            _loadingAnim.SetActive(_loadingKeys.Any());
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
