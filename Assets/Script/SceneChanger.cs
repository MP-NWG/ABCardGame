using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("説明文")]
    string sceneName;

    [SerializeField]
    bool changeOnStart;

    IEnumerator Start()
    {
        yield return null;
        if(changeOnStart)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    
    void Update()
    {
        
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}