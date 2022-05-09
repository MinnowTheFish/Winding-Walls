using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool FlashLightOn {
        get { return _FlashLightOn; }
        set {
            _FlashLightOn = value;
            FlashLight.gameObject.SetActive(value);
            BodyLight.gameObject.SetActive(!value);
        }
    }
    public bool MenuOpen {
        get { return _MenuOpen; }
        set {
            _MenuOpen = value;
            Menu.gameObject.SetActive(value);
            if (value) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public bool SettingsOpen {
        get { return _SettingsOpen; }
        set {
            _SettingsOpen = value;
            Settings.gameObject.SetActive(value);
            if (value) MenuOpen = false;
            if (value) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private bool _FlashLightOn, _MenuOpen, _SettingsOpen;
    public Canvas Menu, Settings;
    public Camera Cam;
    public CharacterController _CharacterController;
    public float speed = 6, gravityScale = 1;
    private float VerticalVelocity;
    public Light FlashLight, BodyLight;
    public Vector2 CamLimit, MouseSensitivity;


    // Start is called before the first frame update
    void Start()
    {
        MenuOpen = false;
        FlashLightOn = false;
        SettingsOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu")) {
            if (SettingsOpen) {
                SettingsOpen = false;
                MenuOpen = true;
            }
            else MenuOpen = !MenuOpen;
        }

        if (MenuOpen || SettingsOpen) return;

        if (Input.GetButtonDown("Toggle Flashlight")) 
            FlashLightOn = !FlashLightOn;

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

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hit!");
        if (other.tag == "Respawn")
            Death();
    }

    public void Death() {
        _CharacterController.transform.position = new Vector3(0, 0, 11.5f);
        VerticalVelocity = 0;
        _CharacterController.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        Cam.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        FlashLightOn = false;
        MenuOpen = false;
    }

    // public void OpenSettings() {
    //     SettingsOpen = true;
    // }
}
