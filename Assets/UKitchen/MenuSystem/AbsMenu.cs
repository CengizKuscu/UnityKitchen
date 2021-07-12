using System;
using UnityEngine;

namespace UKitchen.MenuSystem
{
    public abstract class AbsMenu<TEnum> : MonoBehaviour, IMenu<TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("Menu Name"), SerializeField] private TEnum _menuName;

        [SerializeField] private bool _isPopup;

        [Tooltip("Destroy the Menu when menu is closed(reduces memory usage"), SerializeField]
        private bool _destroyWhenClosed;
        
        protected bool _isInit = false;

        public bool isPopup => _isPopup;

        public TEnum menuName => _menuName;

        public bool destroyWhenClosed => _destroyWhenClosed;

        protected abstract void Prepare();
        protected abstract void Prepare<TArgs1>(TArgs1 args) where TArgs1 : IMenuArgs;
        protected internal abstract void Open();
        protected abstract void OnShowBefore<TMenuArgs>(TMenuArgs e) where TMenuArgs : IMenuArgs;
        protected abstract void OnShowAfter<TMenuArgs>(TMenuArgs e) where TMenuArgs : IMenuArgs;
        protected internal abstract void Close();
        protected abstract void OnHideBefore();
        protected abstract void OnHideAfter();
    }

    public abstract class AbsMenu<TEnum, TArgs> : AbsMenu<TEnum>, IMenu<TEnum, TArgs>
        where TEnum : struct, Enum where TArgs : IMenuArgs
    {
        protected IMenuArgs _args;
        
        public TArgs Args => (TArgs) _args;
    }
}