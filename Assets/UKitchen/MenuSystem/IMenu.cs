using System;

namespace UKitchen.MenuSystem
{
    public interface IMenu<TEnum> where TEnum : struct, Enum
    {
        TEnum menuName { get; }

        void Prepare();
        
        bool destroyWhenClosed { get; }

        void Open();

        void OnShowBefore<TMenuArgs>(TMenuArgs e) where TMenuArgs : IMenuArgs;
        
        void OnShowAfter<TMenuArgs>(TMenuArgs e) where TMenuArgs : IMenuArgs;

        void Close();

        void OnHideBefore();

        void OnHideAfter();
    }

    public interface IMenu<TEnum, TArgs> : IMenu<TEnum> where TEnum : struct, Enum where TArgs : IMenuArgs
    {
        TArgs args { get; }

        void Prepare<TArgs>(TArgs args) where TArgs : IMenuArgs;
    }
    
}