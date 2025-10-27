using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMinigameScene()
    {
        if (SceneStateManager.Instance != null)
        {
            SceneStateManager.Instance.isReloadFromAnotherScene = true; // Set the flag
        }

        SceneManager.LoadScene("Minigame"); // Replace with your scene name
    }
}
