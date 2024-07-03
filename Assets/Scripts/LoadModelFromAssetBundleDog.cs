using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadModelFromAssetBundleDog : MonoBehaviour
{
    public GameObject targetObject1; // 第一个需要替换的对象
    public GameObject targetObject2; // 第二个需要替换材质的对象
    public Button loadButton; // 用于触发加载操作的按钮

    private AssetBundle loadedBundle; // 保存已加载的 AssetBundle 引用

    void Start()
    {
        // 监听按钮的点击事件，当点击时执行LoadAssetBundleAndReplace方法
        loadButton.onClick.AddListener(LoadAssetBundleAndReplace);
    }

    void LoadAssetBundleAndReplace()
    {
        StartCoroutine(LoadAssetBundle());
    }

    IEnumerator LoadAssetBundle()
    {
        // 如果已经有 AssetBundle 加载了，则先卸载它
        if (loadedBundle != null)
        {
            loadedBundle.Unload(true);
            loadedBundle = null;
        }

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "prefab");
        loadedBundle = AssetBundle.LoadFromFile(path);
        if (loadedBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        // 加载第一个目标对象的模型
        AssetBundleRequest request1 = loadedBundle.LoadAssetAsync<GameObject>("DogPolyart");
        yield return request1;

        if (request1.asset != null)
        {
            GameObject newModelPrefab = request1.asset as GameObject;

            // 删除第一个目标对象的所有子节点
            foreach (Transform child in targetObject1.transform)
            {
                Destroy(child.gameObject);
            }

            // 在第一个目标对象下生成新对象
            GameObject newObj1 = Instantiate(newModelPrefab, targetObject1.transform.position, targetObject1.transform.rotation, targetObject1.transform);

            // 保持原有的本地缩放比例
            newObj1.transform.localScale = targetObject1.transform.localScale;
        }

        // 加载第二个目标对象的材质
        AssetBundleRequest request2 = loadedBundle.LoadAssetAsync<Material>("blue");
        yield return request2;

        if (request2.asset != null)
        {
            Material newMaterial = request2.asset as Material;

            // 获取第二个目标对象的所有子节点
            foreach (Transform child in targetObject2.transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = newMaterial;
                }
            }
        }
    }

    void OnDestroy()
    {
        // 在对象销毁时确保卸载 AssetBundle
        if (loadedBundle != null)
        {
            loadedBundle.Unload(true);
            loadedBundle = null;
        }
    }
}