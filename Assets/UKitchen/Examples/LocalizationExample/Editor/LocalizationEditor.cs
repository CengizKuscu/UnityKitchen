using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UKitchen.Localizations.Model;
using UKitchen.Logger;
using UnityEditor;
using UnityEngine;

namespace UKitchen.Localizations
{
    public class LocalizationEditor : EditorWindow
    {
        private LocalizationInstaller _installer;

        private static LocalizationEditor _window;

        private Word _addLanguage;

        /*private string addKey;
        private string addTurkish;
        private string addEnglish;*/

        private string searchKey;
        private string searchTurkish;
        private string searchEnglish;

        private Vector2 scrollPos;
        private Vector2 scrollPosVertical;

        private static bool _showAllList = false;
        private static bool _showSearchList = false;

        private FieldInfo[] _wordFieldInfos;
        private FieldInfo[] _addLanguageFieldInfos;
        private FieldInfo[] _searchLanguageFieldInfos;

        private Type _wordType;
        private Type _addLanguageType;
        private Type _searchLanguageType;
        
        private int _scrollW;

        [MenuItem("UnityKitchen/Localization Editor")]
        static void Init()
        {
            _window = GetWindow<LocalizationEditor>("Localization Editor", true, typeof(SceneView));
            _window.Show();
        }

        private void Refresh()
        {
            _wordType = typeof(Word);
            _addLanguageType = typeof(Word);
            _searchLanguageType = _addLanguageType;
            _wordFieldInfos = _wordType.GetFields().Reverse().ToArray();
            _addLanguageFieldInfos = _addLanguageType.GetFields().Reverse().ToArray();
            _searchLanguageFieldInfos = _addLanguageFieldInfos;
            _addLanguage = new Word();
            _scrollW = (_addLanguageFieldInfos.Length * 150) + 25;
        }

        private void OnGUI()
        {
            if (_addLanguage == null)
                _addLanguage = new Word();
            GUILayout.Space(20);
            if (_window == null)
            {
                _window = GetWindow<LocalizationEditor>("Localization Editor", true, typeof(SceneView));
                _window.Show();
            }

            if (_installer == null)
            {
                string filePath = Application.dataPath + "/Resources/UnityKitchen/LocalizationInstaller.asset";
                
                if (File.Exists(filePath))
                {
                    _installer =
                        AssetDatabase.LoadAssetAtPath<LocalizationInstaller>(
                            "Assets/Resources/UnityKitchen/LocalizationInstaller.asset");
                }
                else
                {
                    EditorGUILayout.LabelField("Not found LocalizationInstaller.asset under Resources/UnityKitchen/");
                    return;
                }
            }

            EditorGUILayout.BeginVertical();

            if (GUILayout.Button("Refresh LanguageData"))
            {
                Refresh();
            }
            
            if(_wordFieldInfos == null || _addLanguageFieldInfos == null || _searchLanguageFieldInfos == null)
                return;

            if (_installer != null )
            {
                #region ADD And SEARCH

                GUILayout.Space(20);
                EditorGUILayout.BeginVertical(GUILayout.Width(400));
                
                foreach (var fieldInfo in _addLanguageFieldInfos)
                {
                    string val = fieldInfo.GetValue(_addLanguage).ToString();
                    val = EditorGUILayout.TextField(fieldInfo.Name, val, GUILayout.Width(400));
                    fieldInfo.SetValue(_addLanguage, val);
                }
                GUILayout.Space(10);
                
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Add", GUILayout.Width(80)))
                {
                    if (_installer.settings.wordList.FirstOrDefault(s =>
                        s.key.ToLower() == _addLanguage.key.ToLower()) != null)
                    {
                        EditorUtility.DisplayDialog("Warning", $"\"{_addLanguage.key}\" is already used", "Ok");
                        return;
                    }
                    _installer.settings.wordList.Add(_addLanguage);

                    _addLanguage = new Word();
                    Refresh();
                    //addKey = addEnglish = addTurkish = string.Empty;
                }
                
                if (GUILayout.Button("Search", GUILayout.Width(80)))
                {
                    _showSearchList = true;

                    //addKey = addEnglish = addTurkish = string.Empty;
                }
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndVertical();

                #endregion

                #region SAVE
                
                GUILayout.Space(10);
                
                EditorGUILayout.BeginHorizontal(GUILayout.Width(350));
                if (GUILayout.Button("Asset To Json", GUILayout.Width(150)))
                {
                    string json = JsonUtility.ToJson(new Localization{wordList = _installer.settings.wordList}, true);
                    File.WriteAllText(Application.dataPath + "/Resources/UnityKitchen/localization.json", json);
                }

                if (GUILayout.Button("Json To Asset", GUILayout.Width(150)))
                {
                    if (File.Exists(Application.dataPath + "/Resources/UnityKitchen/localization.json"))
                    {
                        string fileText =
                            File.ReadAllText(Application.dataPath + "/Resources/UnityKitchen/localization.json");
                        _installer.settings.wordList = JsonUtility.FromJson<Localization>(fileText).wordList;
                    }
                    else
                    {
                        string json = JsonUtility.ToJson(new Localization{wordList = _installer.settings.wordList}, true);;
                        File.WriteAllText(Application.dataPath + "/Resources/UnityKitchen/localization.json", json);
                    }
                }

                EditorGUILayout.EndHorizontal();
                
                #endregion

                scrollPosVertical = EditorGUILayout.BeginScrollView(scrollPosVertical, GUILayout.ExpandHeight(true),
                    GUILayout.Width(_scrollW + 50));
                
                
                GUILayout.Space(10);
                _showAllList = EditorGUILayout.ToggleLeft("Show ALl List", _showAllList);

                if (_showAllList)
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.Width(_scrollW));
                    GUILayout.Space(30);
                    foreach (var fieldInfo in _wordFieldInfos)
                    {
                        EditorGUILayout.LabelField(fieldInfo.Name, GUILayout.Width(150));
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(_scrollW+50),GUILayout.Height(250));

                    foreach (var word in _installer.settings.wordList)
                    {
                        EditorGUILayout.BeginHorizontal(GUILayout.Width(_scrollW));
                        if (GUILayout.Button("-", GUILayout.Width(25)))
                        {
                            _installer.settings.wordList.Remove(word);
                            return;
                        }
                        
                        foreach (var fieldInfo in _wordFieldInfos)
                        {
                            string val = fieldInfo.GetValue(word).ToString();
                            val = EditorGUILayout.TextField(val, GUILayout.Width(150));
                            fieldInfo.SetValue(word, val);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndScrollView();
                }
                GUILayout.Space(10);

                _showSearchList = EditorGUILayout.ToggleLeft("Show Search List", _showSearchList);
                if (_showSearchList)
                {
                    
                    EditorGUILayout.BeginHorizontal(GUILayout.Width(_scrollW));
                    GUILayout.Space(30);
                    foreach (var fieldInfo in _wordFieldInfos)
                    {
                        EditorGUILayout.LabelField(fieldInfo.Name, GUILayout.Width(150));
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(_scrollW+50), GUILayout.Height(250));

                    List<Word> searchResult = new List<Word>();
                    List<Word> wordList = _installer.settings.wordList;
                    
                    foreach (var fieldInfo in _wordFieldInfos)
                    {
                        string tmp = fieldInfo.GetValue(_addLanguage).ToString().ToLower();
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            wordList.ForEach(s =>
                            {
                                var item = fieldInfo.GetValue(s).ToString().ToLower();
                                if (item.Contains(tmp))
                                    searchResult.Add(s);
                            });
                            searchResult = searchResult.Distinct().ToList();
                        }
                    }
                    
                    foreach (var word in searchResult)
                    {
                        EditorGUILayout.BeginHorizontal(GUILayout.Width(_scrollW));
                        if (GUILayout.Button("-", GUILayout.Width(25)))
                        {
                            _installer.settings.wordList.Remove(word);
                            return;
                        }
                        
                        foreach (var fieldInfo in _wordFieldInfos)
                        {
                            string val = fieldInfo.GetValue(word).ToString();
                            val = EditorGUILayout.TextField(val, GUILayout.Width(150));
                            fieldInfo.SetValue(word, val);
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndScrollView();
                }
            }

            EditorGUILayout.EndScrollView();

            //EditorGUILayout.EndVertical();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(_installer);
        }

        private void AddNewWord()
        {
            
        }
    }
}