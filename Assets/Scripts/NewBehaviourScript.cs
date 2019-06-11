using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 10f;
    public float jumpPower = 50f;

    private Rigidbody rb;
    private float horizontalMovement;
    private float VerticalMovement;
    private float Jump = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");
        Jump = Input.GetAxis("Jump");
    }

    private void FixedUpdate()
    {
        // rb.AddForce(new Vector3(horizontalMovement * speed * Time.deltaTime, 0, 0));
        transform.Translate(new Vector3(horizontalMovement * speed * Time.deltaTime, 0, VerticalMovement * speed * Time.deltaTime));
        rb.AddForce(new Vector3(0, Jump * jumpPower * Time.deltaTime  , 0));
    }

}
