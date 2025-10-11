using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    public float moveSpeed;
    public float rotationSpeed;
    public float jumpForce;

    [Header("GroundDetection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded;


    // Start is called before the first frame update
    void Update()
    {
        InputKey = new Vector3(Input.GetAxis("Horizontal") * -1, 0, Input.GetAxis("Vertical") * -1);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, (int)ground);

        Debug.Log(isGrounded);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition((Vector3)transform.position + InputKey * 10 * Time.deltaTime);

        if (InputKey.magnitude > 0.1f)
        {
            float Angle = Mathf.Atan2(InputKey.x, InputKey.z) * Mathf.Rad2Deg;
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, Smooth, 0);
        }
    }
}
