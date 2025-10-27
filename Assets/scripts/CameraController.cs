using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // Reference to the camera to control.
    public GameObject uiImage1; // First UI image to hide/show.
    public GameObject uiImage2;
    public GameObject uiImage3;
    public GameObject uiImage4; // Second UI image to hide/show.
    public float zoomSpeed = 10f; // Speed of zooming.
    public float minZoom = 20f; // Minimum field of view for zoom.
    public float maxZoom = 60f; // Maximum field of view for zoom.
    public float defaultZoom = 50f; // Default field of view for the camera.
    public float moveStep = 1f; // Fixed step size for camera movement.

    public AudioClip scrollSound; // Sound for mouse scroll
    public AudioSource audioSource; // Audio source to play the sound

    public TMP_Text inspectButtonText; // Reference to TMP_Text component in the button.
    public string inspectText = "INSPECT"; // Default text for button.
    public string goBackText = "GO BACK"; // Text for button while inspecting.

    private bool isUIHidden = false; // Tracks whether the UI is hidden.
    private bool controlsEnabled = false; // Tracks if movement and zoom are enabled.

    public Animator characterAnimator; // Reference to the character's Animator component.

    void Update()
    {
        // Enable movement and zoom only if controls are active.
        if (controlsEnabled)
        {
            HandleCameraZoom();
            HandleCameraMovement();
        }
    }

    // Function called by the button to toggle UI and camera controls.
    public void ToggleUIAndControls()
    {
        if (isUIHidden)
        {
            // Stage 2: Re-enable UI and disable controls.
            ShowUI();
            controlsEnabled = false;

            // Stop the animation when UI reappears
            if (characterAnimator != null)
            {
                characterAnimator.SetBool("IsInspecting", false);
            }

            // Update button text
            if (inspectButtonText != null)
            {
                inspectButtonText.text = inspectText;
            }
        }
        else
        {
            // Stage 1: Hide UI and enable controls.
            HideUI();
            controlsEnabled = true;

            // Trigger the animation when UI is hidden
            if (characterAnimator != null)
            {
                characterAnimator.SetBool("IsInspecting", true);
            }

            // Update button text
            if (inspectButtonText != null)
            {
                inspectButtonText.text = goBackText;
            }
        }
    }

    // Handles zooming with the mouse scroll wheel.
    private void HandleCameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            mainCamera.fieldOfView -= scrollInput * zoomSpeed;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, minZoom, maxZoom);

            // Play scroll sound
            if (audioSource != null && scrollSound != null)
            {
                audioSource.PlayOneShot(scrollSound);
            }
        }
    }

    // Handles camera movement using W, A, S, D keys with fixed steps.
    private void HandleCameraMovement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += -Vector3.forward * moveStep;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -Vector3.back * moveStep;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += -Vector3.left * moveStep;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += -Vector3.right * moveStep;
        }

        // Apply movement instantly without velocity or Time.deltaTime.
        mainCamera.transform.position += movement;
    }

    // Hides the UI elements.
    private void HideUI()
    {
        if (uiImage1 != null) uiImage1.SetActive(false);
        if (uiImage2 != null) uiImage2.SetActive(false);
        if (uiImage3 != null) uiImage3.SetActive(true);
        if (uiImage4 != null) uiImage4.SetActive(true);
        isUIHidden = true;
    }

    // Shows the UI elements.
    private void ShowUI()
    {
        if (uiImage1 != null) uiImage1.SetActive(true);
        if (uiImage2 != null) uiImage2.SetActive(true);
        if (uiImage3 != null) uiImage3.SetActive(false);
        if (uiImage4 != null) uiImage4.SetActive(false);
        isUIHidden = false;

        // Reset camera zoom to default when UI is re-enabled.
        mainCamera.fieldOfView = defaultZoom;
    }
}
