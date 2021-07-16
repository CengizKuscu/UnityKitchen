using System;
using System.Collections.Generic;
using UnityEngine;

namespace UKitchen.Localizations.Model
{
    [Serializable]
    public class Localization
    {
        public List<Word> wordList;
    }

    [Serializable]
    public partial class Word
    {
        public string English = string.Empty;
        public string key = string.Empty;
    }
}