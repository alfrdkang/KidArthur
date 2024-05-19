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
    void Start()
    {
        VictoryBG = GameObject.Find("VictoryBG");
        TimeTakenText = GameObject.Find("TimeTakenText").GetComponent<TextMeshProUGUI>();
        CoinCollectedText = GameObject.Find("CoinCollectedText").GetComponent<TextMeshProUGUI>();
        TotalScoreText = GameObject.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
        VictoryBG.SetActive(false); 
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
        UnityEditor.EditorApplication.isPlaying = false;
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
            VictoryBG.SetActive(true);
            TimeTakenText.text = "Time Taken: " + GameObject.Find("timerText").GetComponent<TextMeshProUGUI>().text;
            CoinCollectedText.text = "Coins Collected: " + GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected;
            if (GameObject.Find("Canvas").GetComponent<TimerScript>().elapsedTime >= maxTime)
            {
                TotalScoreText.text = (GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected).ToString();
            } else
            {
                TotalScoreText.text = "Total Score: " + (Mathf.Round(GameObject.Find("MaleCharacterPBR").GetComponent<PlayerScript>().coinCollected * (maxTime - GameObject.Find("Canvas").GetComponent<TimerScript>().elapsedTime)).ToString());
            }
        }
    }
}
