using UnityEngine;
using UnityEngine.UI;

public class SliderSound : MonoBehaviour
{
    public Slider uiSlider; // Reference to the UI Slider
    public AudioClip slideSound; // Sound effect for the slider
    public float stepSize = 25f; // Configurable step size

    private AudioSource audioSource;
    private float previousValue;

    private void Awake()
    {
        // Add AudioSource component and set its properties
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void Start()
    {
        // Set the initial value
        previousValue = uiSlider.value;

        // Add listener to handle value changes
        uiSlider.onValueChanged.AddListener(PlaySlideSound);
    }

    private void PlaySlideSound(float value)
    {
        if (Mathf.Abs(value - previousValue) >= stepSize)
        {
            audioSource.PlayOneShot(slideSound);
            previousValue = value;
        }
    }
}
