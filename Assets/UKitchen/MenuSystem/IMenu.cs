using System;

namespace UKitchen.MenuSystem
{
    public interface IMenu<TEnum> where TEnum : struct, Enum
    {
        TEnum menuName { get; }

        bool destroyWhenClosed { get; }
    }

    public interface IMenu<TEnum, TArgs> : IMenu<TEnum> where TEnum : struct, Enum where TArgs : IMenuArgs
    {
        TArgs Args { get; }
    }
    
}