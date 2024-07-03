using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherC2L : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene(2);
    }
}
