using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}

