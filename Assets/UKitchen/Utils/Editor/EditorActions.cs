using System.IO;
using UKitchen.Logger;
using UnityEditor;
using UnityEngine;
using Zenject.Internal;

namespace UKitchen.Utils
{
    public static class EditorActions
    {
        #region PlayerPrefs

        [MenuItem("UnityKitchen/Editor Actions/Delete All PlayerPrefs")]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void DeletePlayerPrefsKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        #endregion

        public static string AddCSharpClassTemplate(string defaultFileName, string templateStr, string packageName)
        {
            return AddCSharpClassTemplate(defaultFileName, templateStr, packageName,
                ZenUnityEditorUtil.GetCurrentDirectoryAssetPathFromSelection());
        }

        public static string AddCSharpClassTemplate(string defaultFileName, string templateStr, string packageName,
            string folderPath)
        {
            var absolutePath = Path.Combine(Path.GetFullPath(folderPath), packageName + "/" + defaultFileName + ".cs");

            var directory = Path.Combine(Path.GetFullPath(folderPath), packageName);
            AppLog.Warning("DIRECTORY", directory);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (absolutePath == "")
            {
                // Dialog was cancelled
                return null;
            }


            var className = Path.GetFileNameWithoutExtension(absolutePath);
            
            Debug.LogError("PATH : "+ absolutePath);
            if (!File.Exists(absolutePath))
                File.WriteAllText(absolutePath, templateStr.Replace("CLASS_NAME", className));

            AssetDatabase.Refresh();

            var assetPath = ZenUnityEditorUtil.ConvertFullAbsolutePathToAssetPath(absolutePath);

            EditorUtility.FocusProjectWindow();
            //Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

            return assetPath;
        }
    }
}