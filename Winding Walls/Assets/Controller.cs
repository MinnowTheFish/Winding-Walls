using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController _CharacterController;
    public Camera Cam;
    public Light FlashLight, BodyLight;
    public bool FlashLightOn {
        get { return _FlashLightOn; }
        set {
            _FlashLightOn = value;
            FlashLight.gameObject.SetActive(value);
            BodyLight.gameObject.SetActive(!value);
        }
    }
    private bool _FlashLightOn = false;
    public float speed = 6, gravityScale = 1;
    public Vector2 CamLimit, MouseSensitivity;

    private float VerticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Toggle Flashlight")) 
            FlashLightOn = !FlashLightOn;

        // Movement
        Vector3 move = transform.forward * Mathf.Clamp(Input.GetAxis("Joystick Forwards") + Input.GetAxis("Forwards"), -1, 1) // Backwards/Forwards movement
                     + transform.right * Mathf.Clamp(Input.GetAxis("Joystick Sideways") + Input.GetAxis("Sideways"), -1, 1); // Left/Right movement
        _CharacterController.Move(speed * Time.deltaTime * move); // Move the CharacterController by the product of move, change in time, and the character speed

        // Camera Rotation
        transform.Rotate(0, Mathf.Clamp(Input.GetAxis("Mouse X") + Input.GetAxis("Joystick Look X"), -1, 1) * MouseSensitivity.x, 0); // Rotate the camera horizontally
        Cam.transform.Rotate(-Mathf.Clamp(Input.GetAxis("Mouse Y") + Input.GetAxis("Joystick Look Y"), -1, 1) * MouseSensitivity.y, 0, 0); // Rotate the camera vertically

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

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hit!");
        if (other.tag == "Respawn")
            Death();
    }

    public void Death() {
        _CharacterController.transform.position = new Vector3(0, 0, 11.5f);
        VerticalVelocity = 0;
        _CharacterController.transform.rotation = Quaternion.Euler(Vector3.zero);
        Cam.transform.rotation = Quaternion.Euler(Vector3.zero);
        FlashLightOn = false;
    }
}
