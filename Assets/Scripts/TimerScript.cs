/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 6/05/2024
 * Date Modified: 6/05/2024
 * Description: Game Stopwatch Timer
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    /// <summary>
    /// Gets TMP Timer Text Display
    /// </summary>
    [SerializeField] TextMeshProUGUI timerText;

    /// <summary>
    /// Stores amount of time after game start
    /// </summary>
    public float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt(elapsedTime % 1 * 100);

        timerText.text = string.Format("{00:00}:{01:00}:{02:00}", minutes,seconds,milliseconds);
    }
}
