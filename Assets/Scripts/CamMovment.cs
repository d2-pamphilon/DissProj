using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{


    public float speed;
    public float turnSpeed;
    public float zoomSpeed;

    private Vector3 mouseOrigin;
    private bool isRotating;
    private bool isZooming;

    void Update()
    {
        //movment keys
        if (Input.GetKey("a"))
        { transform.Translate(Vector3.left * speed); }
        if (Input.GetKey("w"))
        { transform.Translate(Vector3.up * speed); }
        if (Input.GetKey("d"))
        { transform.Translate(Vector3.right * speed); }
        if (Input.GetKey("s"))
        { transform.Translate(Vector3.down * speed); }

        if (Input.GetMouseButtonDown(1))//right mouse
        {
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }
        if (!Input.GetMouseButton(1)) isRotating = false;

        if (Input.GetMouseButtonDown(0))//left mouse
        { isZooming = true; }
        if (!Input.GetMouseButton(0))
        { isZooming = false; }

        //rotate the screen with mouse when holding right mouse
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

        //zoom when left mouse clicked, zooms out with mouse at top of screen, down at bottom
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }
    }
}
