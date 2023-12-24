using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust speed as needed

    void Update()
    {
        // Read arrow key inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Apply movement to the GameObject
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
