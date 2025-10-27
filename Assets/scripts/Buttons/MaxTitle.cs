using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxTitle : MonoBehaviour
{
    public Slider tileCountSlider; // Reference to the slider in the UI.
    public Text tileCountLabel;   // Text to display the slider's value.

    void Start()
    {
        // Load the saved max tile count or set a default value.
        int savedTileCount = PlayerPrefs.GetInt("MaxTileCount", 500);
        Pathmaker.maxGlobalTiles = savedTileCount;

        // Set the slider's value and range.
        tileCountSlider.minValue = 100;
        tileCountSlider.maxValue = 500;
        tileCountSlider.value = savedTileCount;

        // Update the label.
        UpdateTileCountLabel(savedTileCount);

        // Add listener for slider changes.
        tileCountSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnSliderValueChanged(float value)
    {
        // Update the max tile count based on the slider's value.
        int tileCount = Mathf.RoundToInt(value);
        Pathmaker.maxGlobalTiles = tileCount;

        // Save the value to persist across scenes.
        PlayerPrefs.SetInt("MaxTileCount", tileCount);

        // Update the label to reflect the change.
        UpdateTileCountLabel(tileCount);
    }

    private void UpdateTileCountLabel(int value)
    {
        if (tileCountLabel != null)
        {
            tileCountLabel.text = $" {value}";
        }
    }
}

