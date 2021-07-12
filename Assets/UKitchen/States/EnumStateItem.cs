using System;
using UnityEngine;

namespace UKitchen.States
{
    [Serializable]
    public abstract class EnumStateItem<TEnum> : IEnumStateItem<TEnum> where TEnum : struct, Enum
    {
        [SerializeField] private TEnum _stateValue;

        public TEnum StateValue => _stateValue;
    }

    [Serializable]
    public abstract class EnumStateItem<TEnum, TState1> : EnumStateItem<TEnum> where TEnum : struct, Enum
    {
        public TState1 state1;
    }
    
    [Serializable]
    public abstract class EnumStateItem<TEnum, TState1, TState2> : EnumStateItem<TEnum> where TEnum : struct, Enum
    {
        public TState1 state1;
        public TState2 state2;
    }
    
    [Serializable]
    public abstract class EnumStateItem<TEnum, TState1, TState2, TState3> : EnumStateItem<TEnum, TState1, TState2> where TEnum : struct, Enum
    {
        public TState3 state3;
    }
    
    [Serializable]
    public class EnumStateItem<TEnum, TState1, TState2, TState3, TState4> : EnumStateItem<TEnum, TState1, TState2, TState3> where TEnum : struct, Enum
    {
        public TState4 state4;
    }
    
    [Serializable]
    public class EnumStateItem<TEnum, TState1, TState2, TState3, TState4, TState5> : EnumStateItem<TEnum, TState1, TState2, TState3, TState4> where TEnum : struct, Enum
    {
        public TState5 state5;
    }
}