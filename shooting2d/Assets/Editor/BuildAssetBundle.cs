using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle 
{
    [MenuItem("Build/BuildAssetBundle")]
    public static void BuildAssetBundles()
    {
        string Dir = "Assets/AssetBundles";
        if (Directory.Exists(Dir) == false) //������������
        {
            Directory.CreateDirectory(Dir); //���������
        }
        BuildPipeline.BuildAssetBundles(Dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
