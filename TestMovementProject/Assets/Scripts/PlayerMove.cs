using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public FixedJoystick moveJoystick;
    public FixedJoystick rotateJoystick;
    public float speed = 5f;
    public float rotationSpeed = 70f;

    public Camera playerCamera;
    private CharacterController controller;
    public Vector3 cameraOffset = new Vector3(0, 6.5f, -2);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Rotation logic using the rotateJoystick
        float horizontalRotation = rotateJoystick.Horizontal * rotationSpeed * Time.deltaTime;

        // Apply rotation only around the Y-axis (horizontal rotation)
        transform.Rotate(0, horizontalRotation, 0);

        // Movement logic using the moveJoystick, includes Y-axis movement
        Vector3 moveDirection = transform.right * moveJoystick.Horizontal + transform.forward * moveJoystick.Vertical;
        
        // Optionally, allow vertical movement with another joystick axis or button
        // You can map this to jump or any other action (e.g., space bar for PC or a UI button for mobile)
        if (Input.GetKey(KeyCode.Space)) // For testing, spacebar to move up
        {
            moveDirection += transform.up;
        }
        if (Input.GetKey(KeyCode.LeftShift)) // Shift to move down
        {
            moveDirection -= transform.up;
        }

        // Apply movement
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Update camera to follow the character's position
        UpdateCameraPosition();
    }

    public void UpdateCameraPosition() {
        playerCamera.transform.position = transform.position + transform.TransformDirection(cameraOffset);
        // playerCamera.transform.position = transform.position + transform.TransformDirection(cameraOffset);
        playerCamera.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }
}

