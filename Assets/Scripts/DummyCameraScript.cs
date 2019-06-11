using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCameraScript : MonoBehaviour
{
    public float Slide = 10;
    public float Up_down = 10;

    private float MouseX;
    private float MouseY;

    private void Start()
    {
        
    }

    private void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");


    }

    private void FixedUpdate()
    {
        Quaternion Rotation = new Quaternion(MouseX * Slide * Time.deltaTime, MouseY * Up_down * Time.deltaTime, 0, 0);
        transform.localRotation = Rotation;

    }
}
