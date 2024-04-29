using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    int playerScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncreaseScore(int scoreAdd)
    {
        playerScore += scoreAdd;
        Debug.Log(playerScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
