using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance; // Singleton instance
    public bool isReloadFromAnotherScene = true; // Flag to determine scene reload type

    private void Awake()
    {
        // Ensure this object persists across scene loads
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}