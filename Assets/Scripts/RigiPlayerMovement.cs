using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigiPlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    public float moveSpeed;
    public float jumpForce;

    [Header("GroundDetection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //Checking Key Presses and Assigning Axis
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, (int)ground);

        //Checking Jump Key and Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    void FixedUpdate()
    {
        //Moving Player Based on Assigned Axis and Defined Speed
        rb.AddForce(InputKey * moveSpeed);
    }
}
