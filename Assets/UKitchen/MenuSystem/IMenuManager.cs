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

        void Open(AbsMenu<TEnum> menu, IMenuArgs menuArgs);

        void OpenMenu(TEnum menuName, IMenuArgs args);

        bool IsAlreadyOpen(TEnum menuName, IMenuArgs args, bool update);
        bool IsAlreadyOpen(TEnum menuName);

        void CloseMenu(TEnum menuName);

        void CloseOthers();
    }
}