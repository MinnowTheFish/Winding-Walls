using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController _CharacterController;
    public Camera Cam;
    public float speed = 6, gravityScale = 1;
    public Vector2 CamLimit, MouseSensitivity;

    private float VerticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 move = transform.forward * Input.GetAxis("Vertical") // Backwards/Forwards movement
                     + transform.right * Input.GetAxis("Horizontal"); // Left/Right movement
        _CharacterController.Move(speed * Time.deltaTime * move); // Move the CharacterController by the product of move, change in time, and the character speed

        // Camera Rotation
        transform.Rotate(0, Input.GetAxis("Mouse X") * MouseSensitivity.x, 0); // Rotate the camera horizontally
        Cam.transform.Rotate(-Input.GetAxis("Mouse Y") * MouseSensitivity.y, 0, 0); // Rotate the camera vertically

        // Limiting the vertical camera rotation
        Vector3 currentRotation = Cam.transform.localEulerAngles;
        if (currentRotation.x > 180) // limit the period of the rotation, -180 < θ < 180, rather than 0 < θ < 360
            currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, CamLimit.x, CamLimit.y); // clamp the rotation
        Cam.transform.localRotation = Quaternion.Euler(currentRotation); // change the camera rotation

        // Apply gravity
        if (_CharacterController.isGrounded) VerticalVelocity = 0; // if charcter is touching the ground, set vertical velocity
        else VerticalVelocity -= Time.deltaTime * 9.807f; // else subtract gravity from the character
        _CharacterController.Move(Vector3.up * VerticalVelocity * Time.deltaTime); // apply the velocity
    }
}
