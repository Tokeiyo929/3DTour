using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;  // 引用TextMeshPro命名空间

public class FakeLoadingBar : MonoBehaviour
{
    public Scrollbar scrollbar;  // 拖拽你的Scrollbar到此处
    public TMP_Text statusText;  // 拖拽你的TextMeshPro - Text组件到此处
    public float loadTime = 5f;  // 伪加载总时间
    private float elapsedTime = 0f;  // 已经过去的时间

    private string[] loadingTexts = new string[] {
        "Downloading scene",
        "Comparing versions",
        "Checking packages",
        "Verifying AssetBundles",
        "Downloading resources"
    };

    private bool loadingComplete = false;

    void Update()
    {
        if (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadTime);

            // 更新Scrollbar的值
            scrollbar.size = progress;

            // 计算当前的状态索引
            int statusIndex = Mathf.FloorToInt(progress * loadingTexts.Length);

            // 确保索引在合法范围内
            if (statusIndex < loadingTexts.Length)
            {
                statusText.text = loadingTexts[statusIndex];
            }
        }
        else if (!loadingComplete)
        {
            loadingComplete = true;
            statusText.text = "Loading Complete";
            StartCoroutine(WaitAndLoadNextScene(1f));
        }
    }

    IEnumerator WaitAndLoadNextScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(3);  // 确保场景名称正确
    }
}
