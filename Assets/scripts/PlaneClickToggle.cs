using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlaneClickToggle : MonoBehaviour
{
    public Material material1; // Assign the first material in the Inspector
    public Material material2; // Assign the second material in the Inspector
    public AudioClip clickSound; // Assign the sound effect in the Inspector

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;
    private bool isMaterial1 = true;

    void Start()
    {
        // Ensure the object has a collider (needed for OnMouseDown)
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("No collider attached to the object. Please add a collider component.");
            return;
        }

        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("No MeshRenderer found on the object. Please add one.");
            return;
        }

        // Create and configure the AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Assign initial material and sound
        meshRenderer.material = material1;
        if (clickSound != null) audioSource.clip = clickSound;
    }

    void OnMouseDown()
    {
        // Toggle the material between material1 and material2
        isMaterial1 = !isMaterial1;
        if (meshRenderer != null)
        {
            meshRenderer.material = isMaterial1 ? material1 : material2;
        }

        // Play the click sound effect
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play();
        }
    }
}
