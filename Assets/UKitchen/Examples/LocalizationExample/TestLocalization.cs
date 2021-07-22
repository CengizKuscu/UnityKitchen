using System;
using UKitchen.Localizations.UI;
using UnityEngine;
using Zenject;

namespace UKitchen.Localizations
{
    public class TestLocalization : MonoBehaviour
    {
        [Inject] private LocalizationSettings _settings;
        public LocalizationStateHelper _helper;

        private void OnEnable()
        {
            _helper.StateValue = 0;
        }
    }
}