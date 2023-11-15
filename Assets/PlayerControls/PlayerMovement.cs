using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 5f;
    public float CrouchSpeed = 2f;
    public float CrouchAnimationSpeed = 2f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;

    private float StandingHeight;
    

    private bool IsCrouching;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        StandingHeight = controller.height;


        IsCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKeyDown("left ctrl") && isGrounded) {
            if (IsCrouching == false) {
                controller.height = 1 * Time.deltaTime;
                groundCheck.transform.position += new Vector3(0, 0.5f, 0);
                IsCrouching = true;
            }
            else {
                controller.height = StandingHeight;
                groundCheck.transform.position -= new Vector3(0, 0.5f, 0);
                IsCrouching = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
