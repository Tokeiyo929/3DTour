using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFromFile : MonoBehaviour
{
    public Transform parentNode;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        //prefab是AssetBundle的名字
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "prefab");
        AssetBundle bundle = AssetBundle.LoadFromFile(path);
        if (bundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }
        //Cube是AssetBundle中的资源名字
        AssetBundleRequest request = bundle.LoadAssetAsync<GameObject>("DogPolyart");
        yield return request;

        if (request.asset != null)
        {
            GameObject obj = request.asset as GameObject;
            Instantiate(obj, parentNode);
        }

        bundle.Unload(false);
    }
}
