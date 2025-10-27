using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNightToggle : MonoBehaviour
{
    public Toggle dayToggle; // Reference to the Day toggle
    public Toggle nightToggle; // Reference to the Night toggle
    public Camera mainCamera; // Reference to the Main Camera
    public AudioClip clickSound; // Sound effect for toggles
    public AudioClip dayAmbientSound; // Morning sound
    public AudioClip nightAmbientSound; // Night sound

    private AudioSource audioSource;
    private AudioSource ambientAudioSource;
    private Color dayColor = new Color(0.8745f, 0.8275f, 0.6863f); // Hex C9B885
    private Color nightColor = new Color(0.153f, 0.169f, 0.196f); // Hex 272B32

    private static bool isDay = true; // Default to day mode
    private static bool hasPlayedAmbient = false; // Prevent stacking

    private void Awake()
    {
       
        DontDestroyOnLoad(gameObject);

        // Initialize audio sources
        audioSource = gameObject.AddComponent<AudioSource>();
        ambientAudioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        ambientAudioSource.loop = true; // Ambient sound should loop
        ambientAudioSource.volume = 0.5f; // Adjust volume to blend with background music

        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
    }

    private void Start()
    {
        // Set initial toggle states
        dayToggle.onValueChanged.AddListener(OnDayToggleChanged);
        nightToggle.onValueChanged.AddListener(OnNightToggleChanged);

        if (isDay)
        {
            dayToggle.isOn = true;
            nightToggle.isOn = false;
            mainCamera.backgroundColor = dayColor;
            PlayAmbientSound(dayAmbientSound);
        }
        else
        {
            dayToggle.isOn = false;
            nightToggle.isOn = true;
            mainCamera.backgroundColor = nightColor;
            PlayAmbientSound(nightAmbientSound);
        }
    }

    private void OnDayToggleChanged(bool isOn)
    {
        if (isOn)
        {
            mainCamera.backgroundColor = dayColor;
            nightToggle.isOn = false; // Ensure the other toggle is off
            isDay = true; // Save state
            PlayClickSound();
            PlayAmbientSound(dayAmbientSound);
        }
    }

    private void OnNightToggleChanged(bool isOn)
    {
        if (isOn)
        {
            mainCamera.backgroundColor = nightColor;
            dayToggle.isOn = false; // Ensure the other toggle is off
            isDay = false; // Save state
            PlayClickSound();
            PlayAmbientSound(nightAmbientSound);
        }
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void PlayAmbientSound(AudioClip ambientClip)
    {
        if (ambientAudioSource != null && ambientClip != null)
        {
            if (!hasPlayedAmbient || ambientAudioSource.clip != ambientClip)
            {
                ambientAudioSource.clip = ambientClip;
                ambientAudioSource.Play();
                hasPlayedAmbient = true;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop ambient sound on scene load and reset
        if (ambientAudioSource != null)
        {
            ambientAudioSource.Stop();
            hasPlayedAmbient = false;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from event
    }
}
