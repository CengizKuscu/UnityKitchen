using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.States
{
    [Serializable]
    public abstract class AbsEnumStateHelper<TEnum> where TEnum : struct, Enum
    {
        protected AbsEnumStateHelper()
        {
            StateValue = defaultStateValue;
        }

        protected AbsEnumStateHelper(TEnum defaultState)
        {
            defaultStateValue = defaultState;
            StateValue = defaultState;
        }

        [SerializeField] private TEnum _defaultStateValue;

        public virtual TEnum defaultStateValue
        {
            get => _defaultStateValue;
            private set => _defaultStateValue = value;
        }
        
        public virtual TEnum StateValue { get; set; }
    }

    [Serializable]
    public abstract class AbsEnumStateHelper<TEnum, TStateItem> : AbsEnumStateHelper<TEnum>
        where TStateItem : IEnumStateItem<TEnum> where TEnum : struct, Enum
    {
        [SerializeField] protected List<TStateItem> _stateItems;

        private TStateItem _selectedState;

        public TStateItem selectedState
        {
            get => _stateItems.FirstOrDefault(s => s.StateValue.Equals(StateValue));
        }
    }
}