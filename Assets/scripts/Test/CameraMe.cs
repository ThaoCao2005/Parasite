using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMe : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    private void LateUpdate()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }

        Vector3 cameraPosition = _mainCamera.transform.position;
        cameraPosition.y = transform.position.y; // Keep only horizontal rotation
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f); // Flip to face the camera
    }
}

