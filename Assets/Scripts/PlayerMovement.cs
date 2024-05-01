/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 30/04/2024
 * Date Modified: 30/04/2024
 * Description: Player Movement Script
 */

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
    Rigidbody rb;


    // Jump Variables

    /// <summary>
    /// Multiplies the force applied to player model when they jump
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// Number of jumps player has available
    /// </summary>
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

    /// <summary>
    /// Fetches player input and move player model
    /// </summary>
    void Run()
    {
        Vector3 playerVelo = new Vector3(moveInput.x * playerSpeed, rb.velocity.y, moveInput.y * playerSpeed);
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

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            ResetJump();
        }
    }

    /// <summary>
    /// Resets player's available jumps when they land back on ground
    /// </summary>
    void ResetJump()
    {
        jumpCount = 1;
    }
}
