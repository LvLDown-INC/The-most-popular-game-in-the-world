using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 4f;
    public float WalkSpeed = 4f;
    public float CrouchSpeed = 2f;
    public float SprintSpeed = 6f;
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
        
        CharacterMove(speed);

        if (Input.GetButtonDown("Jump") && isGrounded && !IsCrouching) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKeyDown("left ctrl") && isGrounded) {
            if (IsCrouching == false) {
                controller.height = 1 * Time.deltaTime;
                groundCheck.transform.position += new Vector3(0, 0.5f, 0);
                IsCrouching = true;
                speed = CrouchSpeed;
            }
            else {
                controller.height = StandingHeight;
                groundCheck.transform.position -= new Vector3(0, 0.5f, 0);
                IsCrouching = false;
                speed = WalkSpeed;
            }
        }
        if (Input.GetKey("left shift") && isGrounded) {
            if (!IsCrouching) {
                speed = SprintSpeed;
            }
        } else {
            if (!IsCrouching) {
                speed = WalkSpeed;
            }
            else {
                speed = CrouchSpeed;
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    void CharacterMove(float speed) {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (z > 0) {
            if (x == 0) {
                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * speed * Time.deltaTime);
                Debug.Log(z * 0.8f * speed);
            }
            else {
                Vector3 move = transform.right * x * 0.7f + transform.forward * z * 0.7f;
                controller.Move(move * speed * Time.deltaTime);
            }
        }
        else if (z < 0) {
            if (x == 0) {
                Vector3 move = transform.right * x + transform.forward * z * 0.8f;
                controller.Move(move * speed * Time.deltaTime);
            }
            else {
                Vector3 move = transform.right * x * 0.57f + transform.forward * z * 0.57f;
                controller.Move(move * speed * Time.deltaTime);
            }
            Debug.Log(z * speed);
            Debug.Log("X:");
            Debug.Log(x * speed * 0.7f);
        }
        else {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            Debug.Log(x * speed);
        }
    }
}
