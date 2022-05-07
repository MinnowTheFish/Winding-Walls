using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController _CharacterController;
    public Camera Cam;
    public float speed = 6;
    public Vector2 CamLimit, MouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        _CharacterController.Move(speed * Time.deltaTime * move);

        transform.Rotate(0, Input.GetAxis("Mouse X") * MouseSensitivity.x, 0);
        Cam.transform.Rotate(-Input.GetAxis("Mouse Y") * MouseSensitivity.y, 0, 0);

        Vector3 currentRotation = Cam.transform.localEulerAngles;
        if (currentRotation.x > 180)
            currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, CamLimit.x, CamLimit.y);
        Cam.transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
