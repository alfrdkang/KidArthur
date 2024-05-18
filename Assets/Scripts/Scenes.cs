using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Scenes : MonoBehaviour
{
    GameObject VictoryBG;
    TextMeshProUGUI TimeTakenText;
    TextMeshProUGUI CoinCollectedText;
    TextMeshProUGUI TotalScoreText;

    public int maxTime = 3600;
    void Start()
    {
        VictoryBG = GameObject.Find("VictoryBG");
        TimeTakenText = GameObject.Find("TimeTakenText").GetComponent<TextMeshProUGUI>();
        CoinCollectedText = GameObject.Find("CoinCollectedText").GetComponent<TextMeshProUGUI>();
        TotalScoreText = GameObject.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
        VictoryBG.SetActive(false); 
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

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
