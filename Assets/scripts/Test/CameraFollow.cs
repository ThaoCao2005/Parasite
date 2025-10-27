using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0); // Flip if sprite faces away
    }
}