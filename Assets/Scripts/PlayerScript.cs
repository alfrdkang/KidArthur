/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 30/04/2024
 * Date Modified: 16/05/2024
 * Description: Main Player Code
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder;

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

    public bool strengthOrb = false;

    public TextMeshProUGUI interactText;

    // Start is called before the first frame update
    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        interactText = GameObject.Find("interactText").GetComponent<TextMeshProUGUI>();
    }   

    // Update is called once per frame
    void Update()
    {
        
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
        } else if (collision.gameObject.tag == "Movable" && strengthOrb == false)
        {

        }
    }
}
