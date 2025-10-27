using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRestartManager : MonoBehaviour
{
    public Button restartButton;
    public Text restartsLeftText;
    public Text scoreText;
    public GameObject winUI;
    public GameObject loseUI;

    private void Start()
    {
        // Initialize UI with current values
        UpdateUI();

        // Add button listener
        restartButton.onClick.AddListener(HandleRestartClick);
    }

    public void HandleRestartClick()
    {
        if (GameManager.Instance.restartsLeft > 0)
        {
            // Reduce restarts left and reload scene
            GameManager.Instance.restartsLeft--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Check win/lose conditions when out of restarts
            if (GameManager.Instance.playerScore >= 10)
            {
                winUI.SetActive(true); // Show win UI
            }
            else
            {
                loseUI.SetActive(true); // Show lose UI
            }

            restartButton.interactable = false; // Disable restart button

            // Reset game data AFTER the win/lose screen appears
            Invoke(nameof(ResetGame), 2f); // Reset after 2 seconds for UI visibility
        }

        // Update UI after restart
        UpdateUI();
    }

    private void ResetGame()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateUI()
    {
        restartsLeftText.text = "Restarts Left: " + GameManager.Instance.restartsLeft;
        scoreText.text = "Score: " + GameManager.Instance.playerScore;
    }
}
