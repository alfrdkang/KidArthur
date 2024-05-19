/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 19/05/2024
 * Date Modified: 19/05/2024
 * Description: Player Interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Dialogue tutorialDialogue;
    public Dialogue talk;
    public Dialogue talk2;
    public Dialogue pickUpStrength;
    public Dialogue pickUpJump;
    public Dialogue pickUpDash;
    public Dialogue pickUpUpdraft;
    public Dialogue artifactsObtained;
    public Dialogue pickUpKey;
    public Dialogue allCoins;

    public TextMeshProUGUI DiagText;
    public GameObject NPCDiagBG;

    public Sprite AnnieBG;
    public Sprite ArthurBG;

    private Queue<string> sentences;
    private Queue<string> names;

    public bool diagActive = false;

    // Start is called before the first frame update
    void Start()
    {
        DiagText = GameObject.Find("DiagText").GetComponent<TextMeshProUGUI>();
        NPCDiagBG = GameObject.Find("NPCDiagBG");
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        NPCDiagBG.SetActive(true);
        diagActive = true;

        sentences.Clear();
        names.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();

        if (name == "Annie")
        {
            NPCDiagBG.GetComponent<Image>().sprite = AnnieBG;
        } else
        {
            NPCDiagBG.GetComponent<Image>().sprite = ArthurBG;
        }
        DiagText.text = sentence;
    }
    void EndDialogue()
    {
        NPCDiagBG.SetActive(false);
        diagActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1))
        {
            DisplayNextSentence();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EndDialogue();
        }
    }
}
