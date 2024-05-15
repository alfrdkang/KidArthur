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
    int coinCollected = 0;

    int totalCoins;

    // Start is called before the first frame update
    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
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
        }
    }
}
