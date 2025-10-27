using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    public GameObject confirmMenu; // Reference to the confirmation menu GameObject.

    // Method to show the confirmation menu
    public void ShowConfirmMenu()
    {
        confirmMenu.SetActive(true); // Activate the confirmation menu UI.
    }

    // Method to handle the Yes/No response
    public void ConfirmReturnToStart(bool confirm)
    {
        if (confirm)
        {
            // Load the start scene (replace "StartScene" with your scene name)
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            // Hide the confirmation menu if No is selected
            confirmMenu.SetActive(false);
        }
    }
}
