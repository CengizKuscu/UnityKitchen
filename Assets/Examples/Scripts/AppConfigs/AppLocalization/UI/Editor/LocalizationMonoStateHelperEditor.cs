using UKitchen.Localizations;
using UnityEditor;

namespace Localizations
{
    [CustomEditor(typeof(LocalizationMonoStateHelper))]
    public class LocalizationMonoStateHelperEditor : AbsLocalizationMonoStateHelperEditor<Word, LocalizationSettings,
        LocalizationInstaller, LocalizationMonoStateHelper>
    {
    }
}