using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RigiPlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    public float moveSpeed;
    public float jumpForce;
    public float rotationSpeed;
    public Transform player;
    public bool isPlayerVisible;

    [Header("CameraPosition Rotation Values")]
    private Vector3 horizontalMove;
    private Vector3 verticalMove;
    public Transform cam;

    [Header("GroundDetection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadius;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool doubleJump = false;

    [Header("Collectables")]
    public int collectables;

    [Header("Sounds")]
    public AudioSource source;
    public AudioClip collectable, jump;

    [Header("Animation")]
    [SerializeField] private Animator animator;

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
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && isPlayerVisible)
        {
            rb.AddForce(0, jumpForce, 0);
            animator.SetBool("isJumping", true);
            doubleJump = true;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && doubleJump && !isGrounded)
        {
            Debug.Log("boost");
            rb.AddForce(player.transform.forward.x * 500, 0f, player.transform.forward.z * 500);
            doubleJump = false;
        }

        else
        {
            animator.SetBool("isJumping", false);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("hidden");
            isPlayerVisible = false;
            animator.SetBool("isHidden", true);
        }
        else
        {
            isPlayerVisible = true;
            animator.SetBool("isHidden", false);
        }
    }

    void FixedUpdate()
    {
        //Moving Player Based on Assigned Axis and Defined Speed
        if (isPlayerVisible == true)
        {
            rb.AddForce(InputKey * moveSpeed);
        }
        else rb.AddForce(InputKey * (moveSpeed / 2));

        if (InputKey.magnitude > 0.1f)
        {
            float Angle = Mathf.Atan2(InputKey.x, InputKey.z) * Mathf.Rad2Deg;
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, Smooth, 0);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            collectables++;

            Destroy(collision.gameObject);
            source.clip = collectable;
            source.Play();
            Debug.Log(collectables);
        }
    }
}
