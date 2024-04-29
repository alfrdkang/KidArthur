using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;

    Vector2 moveInput;
    Rigidbody rb;

    //Jump Variables
    public float jumpForce;
    public float jumpCooldown;

    public float jumpCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        Vector3 playerVelo = new Vector3(moveInput.x * playerSpeed, rb.velocity.y, moveInput.y * playerSpeed);
        rb.velocity = transform.TransformDirection(playerVelo);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (jumpCount>0)
        {
            jumpCount--;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void ResetJump()
    {
        jumpCount = 1;
    }
}
