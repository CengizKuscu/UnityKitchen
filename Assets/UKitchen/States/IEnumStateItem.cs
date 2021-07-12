using System;

namespace UKitchen.States
{
    public interface IEnumStateItem
    {
    }

    public interface IEnumStateItem<TEnum> : IEnumStateItem where TEnum : struct, Enum
    {
        TEnum StateValue { get; }
    }
}