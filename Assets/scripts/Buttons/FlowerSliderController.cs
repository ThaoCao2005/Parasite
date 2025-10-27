using UnityEngine;
using UnityEngine.UI;

public class FlowerSliderController : MonoBehaviour
{
    public Slider flowerSlider; // UI Slider
    public Text flowerPercentageText; // Text to display percentage
    public static float flowerPercentage = 0f; // Public static for Pathmaker use

    void Start()
    {
        // Load saved slider value (default to 50 if not found)
        flowerPercentage = PlayerPrefs.GetFloat("FlowerSliderValue", 50f);

        if (flowerSlider != null)
        {
            flowerSlider.value = flowerPercentage; // Set slider value to saved percentage
            flowerSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        UpdateSliderUI(); // Update UI on start
    }

    void OnSliderValueChanged(float value)
    {
        flowerPercentage = value; // Update the global percentage
        PlayerPrefs.SetFloat("FlowerSliderValue", value); // Save the slider value
        UpdateSliderUI(); // Update the percentage text
    }

    void UpdateSliderUI()
    {
        if (flowerPercentageText != null)
        {
            flowerPercentageText.text = $" {flowerPercentage}%"; // Update UI label
        }

        Debug.Log($"Flower tile percentage updated to {flowerPercentage}%");
    }
}