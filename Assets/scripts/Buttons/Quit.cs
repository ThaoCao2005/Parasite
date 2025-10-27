using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public GameObject confirmMenu; // Assign your confirm menu in the inspector.

    public void ShowConfirmMenu()
    {
        confirmMenu.SetActive(true); // Show the confirmation menu.
    }

    public void ConfirmQuitGame(bool confirm)
    {
        if (confirm)
        {
            Application.Quit(); // Quit the game.
            // Note: This won't work in the editor. Test it in a built version of the game.
        }
        else
        {
            confirmMenu.SetActive(false); // Hide the confirmation menu.
        }
    }
}