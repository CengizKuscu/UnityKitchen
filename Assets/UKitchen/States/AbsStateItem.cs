using System;
using UnityEngine;

namespace UKitchen.States
{
    [Serializable]
    public abstract class AbsStateItem : IStateItem
    {
        [SerializeField] private int _stateValue;
        public int StateValue => _stateValue;
    }
    
    [Serializable]
    public abstract class AbsStateItem<TState1> : AbsStateItem
    {
        public bool useState1;
        public TState1 state1;
    }

    [Serializable]
    public abstract class AbsStateItem<TState1, TState2> : AbsStateItem<TState1>
    {
        public bool useState2;
        public TState2 state2;
    }
    
    [Serializable]
    public abstract class AbsStateItem<TState1, TState2, TState3> : AbsStateItem<TState1, TState2>
    {
        public bool useState3;
        public TState3 state3;
    }
    
    [Serializable]
    public abstract class AbsStateItem<TState1, TState2, TState3, TState4> : AbsStateItem<TState1, TState2, TState3>
    {
        public bool useState4;
        public TState4 state4;
    }
    
    [Serializable]
    public abstract class AbsStateItem<TState1, TState2, TState3, TState4, TState5> : AbsStateItem<TState1, TState2, TState3, TState4>
    {
        public bool useState5;
        public TState5 state5;
    }
}