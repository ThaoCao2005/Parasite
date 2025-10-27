using UnityEngine;
using UnityEngine.UI;

public class NarrownessController : MonoBehaviour
{
    public Slider narrownessSlider;
    public Text narrownessLabel;

    public static float narrownessValue = 0;

    private static bool isInitialized = false; // To ensure the value is not reset after reloading.

    void Start()
    {
        if (!isInitialized)
        {
            narrownessValue = narrownessSlider.value; // Set to the slider's initial value only once.
            isInitialized = true;
        }
        else
        {
            narrownessSlider.value = narrownessValue; // Restore the previous value.
        }

        narrownessSlider.onValueChanged.AddListener(UpdateNarrownessValue);
        UpdateNarrownessValue(narrownessSlider.value);
    }

    public void UpdateNarrownessValue(float value)
    {
        narrownessValue = value;

        if (narrownessLabel != null)
        {
            narrownessLabel.text = $" {Mathf.RoundToInt(value * 100)}%";
        }
    }
}