using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] List<GameObject> animals;
    private GameObject activeAnimal;
    private int index = 0;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Quaternion cameraRotation;
    [SerializeField] private Vector3 zoomAmount;
    private Vector3 newZoom;

    void Start()
    {
        //activeAnimal = animals[0];
        //cameraTransform.parent = activeAnimal.transform;
        cameraRotation = cameraTransform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && animals.Count != 0)
        //{
        //    NextIndex();
        //}

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
        cameraTransform.localRotation = Quaternion.identity;
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * .5f);
    }

    public void CameraUnparent()
    {
        cameraTransform.parent = null;
    }

    public void RemoveFromList(GameObject objectToRemove)
    {
        animals.Remove(objectToRemove);
        if (objectToRemove.Equals(activeAnimal))
        {
            NextIndex();
        }
    }

    private void NextIndex()
    {
        if(animals.Count != 0)
        {
            if (index < animals.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            activeAnimal = animals[index];
            cameraTransform.parent = activeAnimal.transform;
        }
        else
        {
            CameraUnparent();
        }
    }
}
