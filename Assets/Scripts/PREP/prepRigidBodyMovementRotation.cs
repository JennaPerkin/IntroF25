using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prepRigidBodyMovementRotation : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody rb;
    public float jumpForce;
    public LayerMask FloorMask;
    private Transform groundCheck;
    float groundRadius;
    private LayerMask ground;

    private Vector3 horizontalMove;
    private Vector3 verticalMove;

    public bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * transform.right;
        verticalMove = Input.GetAxis("Vertical") * new Vector3(transform.forward.x, 0f, transform.forward.z);

        Vector3 direction = horizontalMove + verticalMove;
        transform.position += direction * movementSpeed * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, (int)ground);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }
}
