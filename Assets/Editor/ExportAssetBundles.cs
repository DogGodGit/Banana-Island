// C# Example
// Builds an asset bundle from the selected objects in the project view.
// Once compiled go to "Menu" -> "Assets" and select one of the choices
// to build the Asset Bundle

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExportAssetBundles
{
    [MenuItem("Assets/Build AssetBundle From Selection - Track dependencies")]
    private static void ExportResource()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path,
                                           BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
            Selection.objects = selection;
        }
    }

    [MenuItem("Assets/Build AssetBundle From Selection - No dependency tracking")]
    private static void ExportResourceNoTrack()
    {
        // Bring up save panel
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");
        if (path.Length != 0)
        {
            // Build the resource file from the active selection.
#if UNITY_4_5
			BuildPipeline.BuildAssetBundle(Selection.activeObject, Selection.objects, path);
#else
            // Create the array of bundle build details.
            AssetBundleBuild[] buildMap = new AssetBundleBuild[Selection.objects.Length];

            buildMap[0].assetBundleName = "enemybundle";

            List<string> assets = new List<string>();
            foreach (var item in Selection.objects)
            {
                assets.Add(item.ToString());
            }

            string[] enemyAssets = new string[2];
            enemyAssets[0] = "Assets/Textures/char_enemy_alienShip.jpg";
            enemyAssets[1] = "Assets/Textures/char_enemy_alienShip-damaged.jpg";

            buildMap[0].assetNames = enemyAssets;

            buildMap[1].assetBundleName = "herobundle";

            string[] heroAssets = new string[1];
            heroAssets[0] = "char_hero_beanMan";
            buildMap[1].assetNames = heroAssets;
            //"Assets/ABs"
            BuildPipeline.BuildAssetBundles(path, buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

#endif
        }
    }
}