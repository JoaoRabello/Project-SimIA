using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCamera : MonoBehaviour
{
    float startRotation;
    float targetRotation = 360f;

    public float speed = 0;

    void Start()
    {
        startRotation = transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Slerp();
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }
}
