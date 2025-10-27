using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxRestarts = 3; // Maximum restarts allowed
    public int restartsLeft; // Remaining restarts
    public int playerScore; // Player's current score

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }

    private void Start()
    {
        // Initialize only if values are not set
        if (restartsLeft == 0)
        {
            restartsLeft = maxRestarts;
        }
    }

    public void ResetGame()
    {
        // Only reset score when max restarts are used
        if (restartsLeft <= 0)
        {
            playerScore = 0;
            restartsLeft = maxRestarts;
        }
    }
}
