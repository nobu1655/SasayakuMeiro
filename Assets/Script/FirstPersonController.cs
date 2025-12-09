using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movespeed = 15f;
    public float runspeed = 25f;
    public float rspeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    float rotationX = 0f;
    CharacterController controller;
    private Energy energy;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        energy = GetComponent<Energy>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = movespeed;
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (shiftPressed && energy != null && energy.CanSprint())
        {
            speed = runspeed;
        }

        controller.Move(move * speed * Time.deltaTime);
 
     
    }
}
