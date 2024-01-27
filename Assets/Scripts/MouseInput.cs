using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public Camera cam;
    public Transform player;
    public float mouseSensivity;
    float xRotation;

    bool AllowMouse;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (AllowMouse)
        {
            MouseInputs();
        }
    }

    void MouseInputs()
    {
        float mouseXPos = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseYPos = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseYPos;
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        player.Rotate(Vector3.up * mouseXPos);
    }

    public void AllowChanger()
    {
        AllowMouse = true;
    }

}
