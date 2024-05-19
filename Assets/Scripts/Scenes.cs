/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 17/04/2024
 * Date Modified: 19/05/2024
 * Description: Scene Manager Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Scenes : MonoBehaviour
{
    /// <summary>
    /// Victory Black Background Object
    /// </summary>
    GameObject VictoryBG;
    /// <summary>
    /// Time Taken UI Text Element
    /// </summary>
    TextMeshProUGUI TimeTakenText;
    /// <summary>
    /// Coins Collected UI Text Element
    /// </summary>
    TextMeshProUGUI CoinCollectedText;
    /// <summary>
    /// Total Score UI Text Element
    /// </summary>
    TextMeshProUGUI TotalScoreText;

    /// <summary>
    /// Max Time to complete game
    /// </summary>
    public int maxTime = 3600;

    /// <summary>
    /// Check if game ended
    /// </summary>
    private bool gameEnd;

    void Start()
    {
        VictoryBG = GameObject.Find("VictoryBG");
        try {
            gameEnd = false;
            TimeTakenText = GameObject.Find("TimeTakenText").GetComponent<TextMeshProUGUI>();
            CoinCollectedText = GameObject.Find("CoinCollectedText").GetComponent<TextMeshProUGUI>();
            TotalScoreText = GameObject.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
            VictoryBG.SetActive(false);
        } catch
        {

        }
    }

    private void Update()
    {
        if (gameEnd == true && Input.anyKeyDown) 
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    /// <summary>
    /// Function to swap scene to main and start game
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Function to stop and quit game 
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Function to run when player enters winning area
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "EndArea")
        {
            gameEnd = true;
            VictoryBG.SetActive(true);
            TimeTakenText.text = "Time Taken: " + GameObject.Find("timerText").GetComponent<TextMeshProUGUI>().text;
            CoinCollectedText.text = "Coins Collected: " + GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected;
            if (GameObject.Find("Canvas").GetComponent<TimerScript>().elapsedTime >= maxTime) {
                TotalScoreText.text = "Total Score: " + (GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected).ToString();
            } else if (GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected == 0){
                TotalScoreText.text = "Total Score: " + Mathf.Round(maxTime - GameObject.Find("Canvas").GetComponent<TimerScript>().elapsedTime).ToString();
            } else
            {
                TotalScoreText.text = "Total Score: " + (Mathf.Round(GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected * (maxTime - GameObject.Find("Canvas").GetComponent<TimerScript>().elapsedTime)).ToString());
            }
        }
    }
}
