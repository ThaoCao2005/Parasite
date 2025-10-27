using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousePercentageController : MonoBehaviour
{
    public Slider houseSlider; // Reference to the UI slider
    public Text houseSliderText; // Reference to the text displaying the slider's value
    public static int housePercentage = 0; // Percentage set by the slider

    void Start()
    {
        // Load slider value
        if (PlayerPrefs.HasKey("HouseSliderValue"))
        {
            houseSlider.value = PlayerPrefs.GetFloat("HouseSliderValue");
            OnHouseSliderValueChanged(houseSlider.value);
        }
    }

    public void OnHouseSliderValueChanged(float value)
    {
        housePercentage = Mathf.Clamp(Mathf.RoundToInt(value), 0, 20); // Clamp to 0-20% range
        if (houseSliderText != null)
        {
            houseSliderText.text = housePercentage + "%"; // Update the UI text
        }

        // Save slider value
        PlayerPrefs.SetFloat("HouseSliderValue", value);
        PlayerPrefs.Save();
    }
    public void GenerateHouses(List<Transform> housePrefabs, int houseSliderValue)
    {
        GameObject[] floorTiles = GameObject.FindGameObjectsWithTag("Floor");
        int totalHousesToPlace = Mathf.FloorToInt((housePercentage / 100f) * floorTiles.Length);

        if (totalHousesToPlace == 0)
        {
            Debug.Log("House percentage too low. No houses will be placed.");
            return;
        }

        List<GameObject> availableTiles = new List<GameObject>(floorTiles);

        for (int i = 0; i < totalHousesToPlace; i++)
        {
            if (availableTiles.Count == 0)
            {
                Debug.Log("No more available tiles to place houses.");
                break;
            }

            int randomIndex = Random.Range(0, availableTiles.Count);
            GameObject selectedTile = availableTiles[randomIndex];
            availableTiles.RemoveAt(randomIndex);

            if (housePrefabs.Count > 0)
            {
                int randomHouseIndex = Random.Range(0, housePrefabs.Count);
                Transform selectedHousePrefab = housePrefabs[randomHouseIndex];
                Instantiate(selectedHousePrefab, selectedTile.transform.position, Quaternion.identity);
                Debug.Log($"Spawned House: {selectedHousePrefab.name}");
            }
            else
            {
                Debug.LogWarning("House prefab list is empty!");
            }
        }

        Debug.Log($"Placed {totalHousesToPlace} houses out of {floorTiles.Length} available floor tiles.");
    }
}
