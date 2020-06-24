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

    public float movementSpeed;
    private Vector3 newPosition;
    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    void Start()
    {
        newZoom = cameraTransform.localPosition;
        newRotation = transform.rotation;
        newPosition = new Vector3(+80, 5, +80);
    }

    void Update()
    {
        InputHandler();

        if (Input.GetMouseButtonDown(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * zoomSpeed);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
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

        if (Input.GetKey(KeyCode.Mouse1))
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
