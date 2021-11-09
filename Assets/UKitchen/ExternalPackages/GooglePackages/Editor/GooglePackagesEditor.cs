using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace UKitchen.ExternalPackages.GooglePackages.Editor
{
    public class GooglePackagesEditor : EditorWindow
    {
        private static GooglePackagesEditor _window;

        private static bool FirebaseFirestore;
        private static bool FirebaseInstallations;
        private static bool FirebaseStorage;
        private static bool FirebaseFunctions;
        private static bool FirebaseDatabase;
        private static bool FirebaseMessaging;
        private static bool FirebaseRemoteConfig;
        private static bool FirebaseDynamicLinks;
        private static bool FirebaseCrashlytics;
        private static bool FirebaseAuth;
        private static bool FirebaseAnalytics;
        private static bool DependencyManager;
        private static bool FirebaseApp;

        private static TextAsset _googlePackagesJson;
        private Packages _packages;
        private string _packageFolder = "ExternalPackages";
        private string _packageResult;


        /*
         * FirebaseFirestore            com.google.firebase.firestore           https://dl.google.com/games/registry/unity/com.google.firebase.firestore/com.google.firebase.firestore-8.0.0.tgz
         * FirebaseInstallations        com.google.firebase.installations       https://dl.google.com/games/registry/unity/com.google.firebase.installations/com.google.firebase.installations-8.0.0.tgz
         * FirebaseStorage              com.google.firebase.storage             https://dl.google.com/games/registry/unity/com.google.firebase.storage/com.google.firebase.storage-8.0.0.tgz
         * FirebaseFunctions            com.google.firebase.functions           https://dl.google.com/games/registry/unity/com.google.firebase.functions/com.google.firebase.functions-8.0.0.tgz
         * FirebaseDatabase             com.google.firebase.database            https://dl.google.com/games/registry/unity/com.google.firebase.database/com.google.firebase.database-8.0.0.tgz
         * FirebaseMessaging            com.google.firebase.messaging           https://dl.google.com/games/registry/unity/com.google.firebase.messaging/com.google.firebase.messaging-8.0.0.tgz
         * FirebaseRemoteConfig         com.google.firebase.remote-config       https://dl.google.com/games/registry/unity/com.google.firebase.remote-config/com.google.firebase.remote-config-8.0.0.tgz
         * FirebaseDynamicLinks         com.google.firebase.dynamic-links       https://dl.google.com/games/registry/unity/com.google.firebase.dynamic-links/com.google.firebase.dynamic-links-8.0.0.tgz
         * FirebaseCrashlytics          com.google.firebase.crashlytics         https://dl.google.com/games/registry/unity/com.google.firebase.crashlytics/com.google.firebase.crashlytics-8.0.0.tgz
         * FirebaseAuth                 com.google.firebase.auth                https://dl.google.com/games/registry/unity/com.google.firebase.auth/com.google.firebase.auth-8.0.0.tgz
         * FirebaseAnalytics            com.google.firebase.analytics           https://dl.google.com/games/registry/unity/com.google.firebase.analytics/com.google.firebase.analytics-8.0.0.tgz
         * External Dependency Manager  com.google.external-dependency-manager  https://dl.google.com/games/registry/unity/com.google.external-dependency-manager/com.google.external-dependency-manager-1.2.165.tgz
         * Firebase App                 com.google.firebase.app                 https://dl.google.com/games/registry/unity/com.google.firebase.app/com.google.firebase.app-8.0.0.tgz
         */

        [MenuItem("UnityKitchen/Packages/Google Packages")]
        private static void Init()
        {
            _window = GetWindow<GooglePackagesEditor>("Google Packages", true, typeof(SceneView));
            _window.Show();

            _googlePackagesJson =
                AssetDatabase.LoadAssetAtPath<TextAsset>(
                    "Assets/UKitchen/ExternalPackages/GooglePackages/package.json");
        }

        private void OnGUI()
        {
            
            EditorGUILayout.BeginVertical(GUILayout.Width(300));

            UseFireBase();

            EditorGUILayout.EndVertical();
        }

        private void UseFireBase()
        {
            if (_packages == null)
            {
                _googlePackagesJson =
                    AssetDatabase.LoadAssetAtPath<TextAsset>(
                        "Assets/UKitchen/ExternalPackages/GooglePackages/package.json");
                if (_googlePackagesJson != null)
                {
                    _packages = JsonUtility.FromJson<Packages>(_googlePackagesJson.text);
                }
            }

            if (_packages != null)
            {
                //AppLog.Warning(this, _googlePackagesJson.text);

                EditorGUILayout.Space(20);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Reference #1"))
                    Application.OpenURL("https://developers.google.com/unity/instructions");
                if (GUILayout.Button("Refenrence #2"))
                    Application.OpenURL("https://developers.google.com/unity/archive");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(20);

                _packageFolder = EditorGUILayout.TextField("Download Folder", _packageFolder);
                EditorGUILayout.Space(20);

                EditorGUILayout.BeginHorizontal(GUILayout.Width(400));

                EditorGUILayout.BeginVertical(GUILayout.Width(200));
                _packages.packages.ForEach(s =>
                {
                    if (s.id != "ExternalDependencyManager" && s.id != "FirebaseApp")
                    {
                        s.selected = EditorGUILayout.ToggleLeft(s.id, s.selected);
                    }
                });
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(GUILayout.Width(400));
                GUILayout.Space(20);
                
                EditorGUILayout.LabelField("Add to \"Editor Coroutine\" Package from Package Manager");
                GUILayout.Space(20);
                
                if (GUILayout.Button("DOWNLOAD Google Packages"))
                {
                    DownloadPackages();
                }
                
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();

                _packageResult = EditorGUILayout.TextArea(_packageResult, GUILayout.Width(800), GUILayout.Height(200));
            }
        }

        private void DownloadPackages()
        {
            List<string> names = new List<string>();
            _packages.packages.ForEach(s =>
            {
                if (s.selected)
                {
                    names.Add(s.id);
                    names.AddRange(s.requireIds);
                }
            });

            names = names.Distinct().ToList();

            List<Package> downloadList = new List<Package>();

            names.ForEach(s =>
            {
                var p = _packages.packages.Find(a => a.id == s);
                if (p != null)
                    downloadList.Add(p);
            });

            EditorCoroutineUtility.StartCoroutine(DownLoadProcess(downloadList), this);
        }

        private IEnumerator DownLoadProcess(List<Package> e)
        {
            string folderPath = Application.dataPath.Replace("Assets", _packageFolder);

            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }
            else
            {
                _packageResult = "\"com.unity.editorcoroutines\": \"1.0.0\",";
                foreach (var package in e)
                {
                    //await Donwload(package, folderPath);
                    using (UnityWebRequest uwr = new UnityWebRequest(package.url))
                    {
                        uwr.method = UnityWebRequest.kHttpVerbGET;

                        var tmp = Path.Combine(folderPath, package.name + "-" + package.version + ".tgz");

                        _packageResult += "\n\"" + package.name + "\": \"file:../" + _packageFolder + "/" +
                                          package.name + "-" + package.version + ".tgz\",";

                        DownloadHandlerFile dh = new DownloadHandlerFile(tmp);

                        dh.removeFileOnAbort = true;

                        uwr.downloadHandler = dh;

                        yield return uwr.SendWebRequest();
                    }
                }

                _packageResult = _packageResult.Substring(0, _packageResult.Length - 1);
                //UnityWebRequest uwr = UnityWebRequest.Get(e[0].url);

            }
        }
    }
}