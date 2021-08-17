using UKitchen.Localizations;
using UnityEditor;

namespace Localizations
{
    public class LocalizationEditor : AbsLocalizationEditor<Word, Localization, LocalizationSettings,
        LocalizationInstaller, LocalizationEditor>
    {
        
        [MenuItem("UnityKitchen/Localization Editor")]
        static void Init()
        {
            ShowWindow();
        }
    }
}