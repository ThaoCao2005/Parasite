using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateB : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        Pathmaker.globalTileCount = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Reload the current scene.
    }
}

