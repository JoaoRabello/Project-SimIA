using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Quaternion cameraRotation;
    [SerializeField] private Vector3 zoomAmount;
    [SerializeField] private float rotationAmount;
    private Vector3 newZoom;
    private Quaternion newRotation;

    private bool isRotating = false;
    private bool isZooming = false;

    public float rotationSpeed;
    public float zoomSpeed;

    void Start()
    {
        newZoom = cameraTransform.localPosition;
        newRotation = transform.rotation;
    }

    void Update()
    {
        InputHandler();

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * zoomSpeed);

    }

    private void InputHandler()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            newZoom -= zoomAmount;
        }
        else
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                newZoom += zoomAmount;
            }
        }

        if (Input.GetKey(KeyCode.Mouse2))
            isRotating = true;
        else
            isRotating = false;

        if (isRotating)
        {
            if (Input.GetAxisRaw("Mouse X") > 0)
            {
                newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
            }
            else
            {
                if (Input.GetAxisRaw("Mouse X") < 0)
                {
                    newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
                }
            }
        }
    }
}
