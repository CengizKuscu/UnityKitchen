using System;
using UnityEngine;

namespace UKitchen.MenuSystem
{
    public interface IMenuManager<TEnum> where TEnum : struct,Enum
    {
        Transform menuContainer { get; }
        Transform popupContainer { get; }

        GameObject loadingAnim { get; }

        void Init();

        void Open(IMenu<TEnum> menu, IMenuArgs menuArgs);

        bool OpenMenu(TEnum menuName, IMenuArgs args);

        void CloseMenu(TEnum menuName);

        void CloseOthers();
    }
}