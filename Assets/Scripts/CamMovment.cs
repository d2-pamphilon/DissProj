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


        if (Input.GetMouseButtonDown(1))//right mouse sets rotation to true
        {
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }
        if (!Input.GetMouseButton(1))
        { isRotating = false; }


        float scroll = Input.GetAxis("Mouse ScrollWheel");//gets delta of scroll wheel

        if (scroll > 0f)//scroll up
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);

        }
        else if (scroll < 0f)//scroll down
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 move = pos.y * zoomSpeed * transform.forward*-1;
            transform.Translate(move, Space.World);
        }
       

        if (isRotating)//rotate towards mouse when right mouse down
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

       
    }
}
