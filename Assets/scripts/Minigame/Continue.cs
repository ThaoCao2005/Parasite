using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    public GameObject menuUI; // Reference to the intro menu UI

    void Start()
    {
        // Show the menu UI only if reloading from another scene
        if (SceneStateManager.Instance != null && SceneStateManager.Instance.isReloadFromAnotherScene)
        {
            menuUI.SetActive(true);
            SceneStateManager.Instance.isReloadFromAnotherScene = false; // Reset the flag
        }
        else
        {
            menuUI.SetActive(false);
        }
    }

    public void HideMenu()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Menu UI reference is missing!");
        }
    }
}