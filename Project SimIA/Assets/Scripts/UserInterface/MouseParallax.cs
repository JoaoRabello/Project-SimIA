using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 startPos;

    public float movementAmount;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        transform.position = new Vector3(startPos.x + (pos.x * movementAmount), startPos.y + (pos.y * movementAmount), 0);
    }
}
