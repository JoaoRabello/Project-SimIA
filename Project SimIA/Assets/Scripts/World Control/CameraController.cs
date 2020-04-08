using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    
    [SerializeField] List<GameObject> monkeys;
    private GameObject activeMonkey;
    private int index = 0;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 zoomAmount;
    private Vector3 newZoom;

    void Start()
    {
        activeMonkey = monkeys[0];
        cameraTransform.parent = activeMonkey.transform;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && monkeys.Count != 0)
        {
            NextIndex();
        }

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

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom / 2.5f, Time.deltaTime * 0.5f);
    }

    public void CameraUnparent()
    {
        cameraTransform.parent = null;
    }

    public void RemoveFromList(GameObject objectToRemove)
    {
        monkeys.Remove(objectToRemove);
        if (objectToRemove.Equals(activeMonkey))
        {
            NextIndex();
        }
    }

    private void NextIndex()
    {
        if(monkeys.Count != 0)
        {
            if (index < monkeys.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            activeMonkey = monkeys[index];
            cameraTransform.parent = activeMonkey.transform;
        }
        else
        {
            CameraUnparent();
        }
    }
}
