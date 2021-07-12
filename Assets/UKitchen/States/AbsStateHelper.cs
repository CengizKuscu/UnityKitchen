using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UKitchen.States
{
    [Serializable]
    public abstract class AbsStateHelper<TStateItem> where TStateItem : IStateItem
    {
        [SerializeField] protected int _defaultStateValue;
        [SerializeField] protected List<TStateItem> _stateItems;

        private int _stateValue;
        private TStateItem _selectedState;

        protected AbsStateHelper()
        {
            StateValue = _defaultStateValue;
        }

        public virtual TStateItem SelectedState => _stateItems.FirstOrDefault(s=>s.StateValue == _stateValue);

        public virtual int StateValue
        {
            get => _stateValue;
            set => _stateValue = value;
        }
    }
}