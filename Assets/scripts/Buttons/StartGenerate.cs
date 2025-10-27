using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGenerate : MonoBehaviour
{
    [SerializeField]
    private string sceneToReload; // The name of the scene to reload.

    public void ReloadScene()
    {
        if (!string.IsNullOrEmpty(sceneToReload))
        {
            SceneManager.LoadScene(sceneToReload);
        }
        else
        {
            Debug.LogWarning("Scene name is not set! Please assign a scene name in the inspector.");
        }
    }
}