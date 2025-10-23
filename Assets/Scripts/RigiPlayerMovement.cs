using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigiPlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    public float moveSpeed;
    public float jumpForce;
    public float rotationSpeed;

    [Header("CameraPosition Rotation Values")]
    private Vector3 horizontalMove;
    private Vector3 verticalMove;
    public Transform cam;

    [Header("GroundDetection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //Checking Key Presses and Assigning Axis
        //InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        horizontalMove = Input.GetAxis("Horizontal") * cam.transform.right;
        verticalMove = Input.GetAxis("Vertical") * new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z);

        InputKey = horizontalMove + verticalMove;

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

        if (InputKey.magnitude > 0.1f)
        {
            float Angle = Mathf.Atan2(InputKey.x, InputKey.z) * Mathf.Rad2Deg;
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, Smooth, 0);
        }
    }
}
