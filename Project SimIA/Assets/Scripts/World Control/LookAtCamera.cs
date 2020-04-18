using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera cameraMain;

    void Start()
    {
        cameraMain = FindObjectOfType<Camera>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cameraMain.transform.position);
    }
}
