using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigiPlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        rb.AddForce(InputKey * moveSpeed);
    }
}
