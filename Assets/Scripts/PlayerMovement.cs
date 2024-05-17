/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 30/04/2024
 * Date Modified: 30/04/2024
 * Description: Player Movement Script
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    Rigidbody rb;

    /// <summary>
    /// Player's Animator Component
    /// </summary>
    Animator playerAnimator;

    // Jump Variables

    /// <summary>
    /// Multiplies the force applied to player model when they jump
    /// </summary>
    public float jumpForce;

    public float maxJumpCount = 1;

    public bool canUpdraft = true;

    /// <summary>
    /// Number of jumps player has available
    /// </summary>
    public float jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
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
        Debug.Log("Reset!");
        jumpCount = maxJumpCount;
    }

    void Updraft()
    {
        if (GameObject.Find("Main Camera").GetComponent<Interactor>().updraftOrbCollected == true && canUpdraft)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                canUpdraft = false;
                playerAnimator.Play("JumpFull_Normal_RM_SwordAndShield", 0, 0.0f);
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce * 3, ForceMode.Impulse);
            }
        }
    }

    void Dash()
    {
        if (GameObject.Find("Main Camera").GetComponent<Interactor>().dashOrbCollected == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("DASH");
                playerAnimator.Play("MoveFWD_Normal_InPlace_SwordAndShield");
            }
        }
    }
}
