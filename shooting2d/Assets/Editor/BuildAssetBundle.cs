using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle 
{
    [MenuItem("Build/BuildAssetBundle")]
    public static void BuildAssetBundles()
    {
        string Dir = "Assets/AssetBundles";
        if (Directory.Exists(Dir) == false) //폴더가없으면
        {
            Directory.CreateDirectory(Dir); //폴더만들기
        }
        BuildPipeline.BuildAssetBundles(Dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
