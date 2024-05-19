/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 30/04/2024
 * Date Modified: 19/05/2024
 * Description: Main Player Code
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// Gets Coin Counter UI Text
    /// </summary>
    [SerializeField] TextMeshProUGUI coinText;

    /// <summary>
    /// Stores number of coins player collected
    /// </summary>
    public int coinCollected = 0;

    /// <summary>
    /// Stores total number of coins in the game
    /// </summary>
    public int totalCoins;

    /// <summary>
    /// Checks if player has obtained strength orb 
    /// </summary>
    public bool strengthOrb = false;

    /// <summary>
    /// Interact UI Text Element when Player Raycast hits Interactable
    /// </summary>
    public TextMeshProUGUI interactText;

    // Start is called before the first frame update
    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        coinText.text = ("Coins: " + coinCollected.ToString() + "/" + totalCoins);
        interactText = GameObject.Find("interactText").GetComponent<TextMeshProUGUI>();
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
                TriggerDialogue(FindObjectOfType<DialogueManager>().allCoins);
                coinText.color = Color.yellow;
            }
            coinText.text = ("Coins: " + coinCollected.ToString() + "/" + totalCoins);
        } else if (collision.gameObject.tag == "Movable" && strengthOrb == false)
        {
            interactText.text = "You are not strong enough to push this block.";
            interactText.enabled = true;
        }
    }

    /// <summary>
    /// Runs if player is colliding with something
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Movable" && strengthOrb == false)
        {
            interactText.text = "You are not strong enough to push this block.";
            interactText.enabled = true;
        }
    }

    /// <summary>
    /// Function to trigger and start dialogue
    /// </summary>
    /// <param name="Diag"></param>
    public void TriggerDialogue(Dialogue Diag)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Diag);
    }
}
