/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 30/04/2024
 * Date Modified: 19/05/2024
 * Description: Player Movement Script
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Multiplies the speed of the player
    /// </summary>
    [SerializeField] float playerSpeed = 5f;

    /// <summary>
    /// User's movement input
    /// </summary>
    Vector2 moveInput;

    /// <summary>
    /// Player's Rigidbody Component
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Player's Animator Component
    /// </summary>
    Animator playerAnimator;

    // Jump Variables

    /// <summary>
    /// Multiplies the force applied to player model when they jump
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// Stores player's max jump count before retouching ground (1 or 2)
    /// </summary>
    public float maxJumpCount = 1;

    /// <summary>
    /// Checks if player is able to updraft
    /// </summary>
    public bool canUpdraft = true;

    /// <summary>
    /// Number of jumps player has available
    /// </summary>
    public float jumpCount;

    /// <summary>
    /// Checks if player is currently dashing
    /// </summary>
    private bool dashing = false;


    /// <summary>
    /// Stores player model orientation
    /// </summary>
    public Transform orientation;
    /// <summary>
    /// Stores PlayerCamera Transform Settings
    /// </summary>
    public Transform playerCam;

    /// <summary>
    /// Amount of Force on Player Dash
    /// </summary>
    public float dashForce;
    /// <summary>
    /// Amount of Upward Force on Player Dash
    /// </summary>
    public float dashUpForce;
    /// <summary>
    /// Dash Time Duration
    /// </summary>
    public float dashDuration;

    /// <summary>
    /// Dash Cooldown
    /// </summary>
    public float dashCD;
    /// <summary>
    /// Timer for Dash Cooldown
    /// </summary>
    private float dashCDTimer;

    // Start is called before the first frame update
    private void Start()
    {
        jumpCount = maxJumpCount;
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!dashing)
        {
            Run();
        }

        Updraft();
        Dash();
    }

    /// <summary>
    /// Fetches player input and move player model
    /// </summary>
    void Run()
    {
        Vector3 playerVelo = new Vector3(moveInput.x * playerSpeed, rb.velocity.y, moveInput.y * playerSpeed);
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            playerAnimator.Play("MoveFWD_Normal_InPlace_SwordAndShield");
        }

        rb.velocity = transform.TransformDirection(playerVelo);
    }

    /// <summary>
    /// Runs everytime player inputs movement keys, fetches input
    /// </summary>
    /// <param name="value"></param>
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    /// <summary>
    /// Runs everytime player inputs jump keybinds, jumps player model
    /// </summary>
    void OnJump()
    {
        if (jumpCount>0)
        {
            jumpCount--;
            
            playerAnimator.Play("JumpFull_Normal_RM_SwordAndShield",0,0.0f);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Checks if player is touching the ground
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ResetJump();
            canUpdraft = true;
        }
    }

    /// <summary>
    /// Resets player's available jumps when they land back on ground
    /// </summary>
    void ResetJump()
    {
        jumpCount = maxJumpCount;
    }

    /// <summary>
    /// Function to updraft player when [Q] Pressed
    /// </summary>
    void Updraft()
    {
        if (GameObject.Find("Main Camera").GetComponent<Interactor>().updraftOrbCollected == true && canUpdraft)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                canUpdraft = false;
                playerAnimator.Play("JumpFull_Spin_RM_SwordAndShield", 0, 0.0f);
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce * 3, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// Function that takes in player model Orientation and Input to determine direction of Dash
    /// </summary>
    /// <param name="forwardDirection"></param>
    /// <returns></returns>
    private Vector3 GetDirection(Transform forwardDirection)
    {
        Vector3 direction = (forwardDirection.forward * moveInput.y + forwardDirection.right * moveInput.x);
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            direction = forwardDirection.forward;
        }
        return direction.normalized;
    }

    /// <summary>
    /// Function to dash player when [L-Shift] Pressed
    /// </summary>
    private void Dash()
    {
        if (dashCDTimer > 0)
        {
            dashCDTimer -= Time.deltaTime;
        }
        if (GameObject.Find("Main Camera").GetComponent<Interactor>().dashOrbCollected == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (dashCDTimer > 0) return;
                else dashCDTimer = dashCD;



                dashing = true;
                Vector3 appliedForce = GetDirection(playerCam) * dashForce + orientation.up * dashUpForce;
                delayedAppliedForce = appliedForce;
                Invoke(nameof(DelayedAppliedForce), 0.03f);

                Invoke(nameof(ResetDash), dashDuration);
                playerAnimator.Play("MoveFWD_Normal_InPlace_SwordAndShield");
            }
        }
    }

    /// <summary>
    /// Vector3 to store appliedForce Vector to delay it
    /// </summary>
    private Vector3 delayedAppliedForce;
    /// <summary>
    /// Function to apply appliedForce Delayed
    /// </summary>
    private void DelayedAppliedForce()
    {
        rb.velocity = delayedAppliedForce;
    }

    /// <summary>
    /// Function to reset dash after it ends
    /// </summary>
    private void ResetDash()
    {
        dashing = false;
    }
}
