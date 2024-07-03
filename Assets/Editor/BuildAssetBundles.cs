using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundles : MonoBehaviour
{
    //将BuildAllAssetBundles方法添加到Unity菜单中，使其可以通过菜单选项调用。
    [MenuItem("Assets/Build AssetBundles")]

    static void BuildAllAssetBundles()
    {
        // 定义存储AssetBundle的目录
        string assetBundleDirectory = "Assets/AssetBundles";

        // 如果目录不存在，则创建它
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        // 构建AssetBundles
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);

        // 打印成功消息
        Debug.Log("AssetBundles are built successfully!");
    }
}
