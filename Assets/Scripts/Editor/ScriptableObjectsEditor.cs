using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectsEditor : EditorWindow
{
    public string ScriptableObjectName { get; set; }

    [MenuItem("ScriptableObjects/Create")]
    static void Init()
    {
        ScriptableObjectsEditor window = (ScriptableObjectsEditor)GetWindow(typeof(ScriptableObjectsEditor));
        window.Show();
    }

    void OnGUI()
    {
        ScriptableObjectName = EditorGUILayout.TextField("Name", ScriptableObjectName);

        if (GUILayout.Button("Create"))
        {
            var asset = CreateInstance(ScriptableObjectName);

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + ScriptableObjectName + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }

}
