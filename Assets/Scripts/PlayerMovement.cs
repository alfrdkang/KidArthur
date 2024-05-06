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
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Multiplies the speed of the player
    /// </summary>
    [SerializeField] float playerSpeed = 5f;

    /// <summary>
    /// Gets Coin Counter UI Text
    /// </summary>
    [SerializeField] TextMeshProUGUI coinText;

    /// <summary>
    /// Stores number of coins player collected
    /// </summary>
    int coinCollected = 0;

    int totalCoins;

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

    /// <summary>
    /// Number of jumps player has available
    /// </summary>
    public float jumpCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    /// <summary>
    /// Runs everytime player collides into something
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinCollected += 1;
            if (coinCollected == totalCoins)
            {
                coinText.color = Color.yellow;
            }
            coinText.text = ("Coins: " + coinCollected.ToString() + "/" + totalCoins);
        }
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
