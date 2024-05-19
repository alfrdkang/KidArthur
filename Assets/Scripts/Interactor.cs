/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 16/05/2024
 * Date Modified: 19/05/2024
 * Description: Player Interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    /// <summary>
    /// Raycast Source (Player)
    /// </summary>
    public Transform InteractorSource;
    /// <summary>
    /// Raycast Interact Range
    /// </summary>
    public float InteractRange;

    /// <summary>
    /// Checks if key is obtained
    /// </summary>
    public bool keyObtained = false;
    /// <summary>
    /// Checks if updraft artifact orb is obtained
    /// </summary>
    public bool updraftOrbCollected = false;
    /// <summary>
    /// Checks if dash artifact orb is obtained
    /// </summary>
    public bool dashOrbCollected = false;
    /// <summary>
    /// Checks if strength artifact orb is obtained
    /// </summary>
    public bool strengthOrbCollected = false;
    /// <summary>
    /// Checks if jump artifact orb is obtained
    /// </summary>
    public bool jumpOrbCollected = false;
    /// <summary>
    /// Checks if dialogue after all artifacts are picked up has occured
    /// </summary>
    public bool artifactsTotalDiag = false;

    /// <summary>
    /// Collectable Rotation Speed
    /// </summary>
    public float rotateSpeed = 100.0f;

    /// <summary>
    /// Strength Orb UI Element
    /// </summary>
    public Image strengthOrb;
    /// <summary>
    /// Updraft Orb UI Element
    /// </summary>
    public Image updraftOrb;
    /// <summary>
    /// Jump Orb UI Element
    /// </summary>
    public Image jumpOrb;
    /// <summary>
    /// Dash Orb UI Element
    /// </summary>
    public Image dashOrb;
    /// <summary>
    /// Key UI Element
    /// </summary>
    public Image key;

    /// <summary>
    /// Strength Orb Collect UI Sprite
    /// </summary>
    public Sprite strengthOrbTrue;
    /// <summary>
    /// Updraft Orb Collect UI Sprite
    /// </summary>
    public Sprite updraftOrbTrue;
    /// <summary>
    /// Jump Orb Collect UI Sprite
    /// </summary>
    public Sprite jumpOrbTrue;
    /// <summary>
    /// Dash Orb Collect UI Sprite
    /// </summary>
    public Sprite dashOrbTrue;

    /// <summary>
    /// Interact UI Text when Player Raycast Hits Interactable
    /// </summary>
    public TextMeshProUGUI interactText;

    /// <summary>
    /// Player's Animator Component
    /// </summary>
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TriggerDialogue(FindObjectOfType<DialogueManager>().tutorialDialogue);
        key.enabled = false;
        interactText = GameObject.Find("interactText").GetComponent<TextMeshProUGUI>();
        interactText.enabled = false;
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Collectable collectObj) && !FindObjectOfType<DialogueManager>().diagActive)
            {
                if (hitInfo.collider.gameObject.tag != "Coin")
                {
                    float Rotate = rotateSpeed * Time.deltaTime;
                    hitInfo.collider.gameObject.transform.Rotate(Rotate, Rotate, 0);
                }

                if (hitInfo.collider.gameObject.name == "Key")
                {
                    interactText.text = "Press [E] to Pickup Key";
                    interactText.enabled = true;
                }
                else if (hitInfo.collider.gameObject.name == "StrengthOrb")
                {
                    interactText.text = "Press [E] to obtain STRENGTH";
                    interactText.enabled = true;
                }
                else if (hitInfo.collider.gameObject.name == "UpdraftOrb")
                {
                    interactText.text = "Press [E] to obtain UPDRAFT";
                    interactText.enabled = true;
                }
                else if (hitInfo.collider.gameObject.name == "JumpOrb")
                {
                    interactText.text = "Press [E] to obtain JUMP+";
                    interactText.enabled = true;
                }
                else if (hitInfo.collider.gameObject.name == "DashOrb")
                {
                    interactText.text = "Press [E] to obtain DASH";
                    interactText.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hitInfo.collider.gameObject.name == "Key")
                    {
                        keyObtained = true;
                        key.enabled = true;
                        TriggerDialogue(FindObjectOfType<DialogueManager>().pickUpKey);
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    } 
                    else if (hitInfo.collider.gameObject.name == "StrengthOrb")
                    {
                        strengthOrbCollected = true;
                        strengthOrb.sprite = strengthOrbTrue;
                        GameObject[] movables = GameObject.FindGameObjectsWithTag("Movable");
                        foreach (GameObject mvble in movables)
                            mvble.GetComponent<Rigidbody>().mass = 12;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().strengthOrb = true;
                        TriggerDialogue(FindObjectOfType<DialogueManager>().pickUpStrength);
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    }
                    else if (hitInfo.collider.gameObject.name == "UpdraftOrb")
                    {
                        updraftOrb.sprite = updraftOrbTrue;
                        updraftOrbCollected = true;
                        TriggerDialogue(FindObjectOfType<DialogueManager>().pickUpUpdraft);
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    }
                    else if (hitInfo.collider.gameObject.name == "JumpOrb")
                    {
                        jumpOrbCollected = true;
                        jumpOrb.sprite = jumpOrbTrue;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().maxJumpCount = 2;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().jumpCount = 2;
                        TriggerDialogue(FindObjectOfType<DialogueManager>().pickUpJump);
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    } 
                    else if (hitInfo.collider.gameObject.name == "DashOrb")
                    {
                        dashOrbCollected = true;
                        dashOrb.sprite = dashOrbTrue;
                        dashOrbCollected = true;
                        TriggerDialogue(FindObjectOfType<DialogueManager>().pickUpDash);
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    }
                    collectObj.Interact();
                }
            }
            else if (hitInfo.collider.gameObject.TryGetComponent(out DoorScript door) && !FindObjectOfType<DialogueManager>().diagActive)
            {
                if (keyObtained)
                {
                    if (hitInfo.collider.gameObject.GetComponent<DoorScript>().doorOpened)
                    {
                        interactText.text = "Press [E] to Close Door";
                        interactText.enabled = true;
                    } else
                    {
                        interactText.text = "Press [E] to Open Door";
                        interactText.enabled = true;
                    }
                } else
                {
                    interactText.text = "LOCKED";
                    interactText.enabled = true;
                }
                if (Input.GetKeyDown(KeyCode.E) && keyObtained == true)
                {
                    door.Interact();
                } else if (Input.GetKeyDown(KeyCode.E) && keyObtained == false)
                {
                    Debug.Log("This door is locked.");
                }
            }
            else if (hitInfo.collider.gameObject.name == "FemaleCharacterPBR" && !FindObjectOfType<DialogueManager>().diagActive)
            {
                interactText.text = "[E] Talk to Annie";
                interactText.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactText.enabled = false;
                    if (!artifactsTotalDiag)
                    {
                        TriggerDialogue(FindObjectOfType<DialogueManager>().talk);
                    } else
                    {
                        TriggerDialogue(FindObjectOfType<DialogueManager>().talk2);
                    }
                }
            }
        } else
        {
            interactText.enabled = false;
        }
        if (strengthOrbCollected && dashOrbCollected && jumpOrbCollected && updraftOrbCollected && !FindObjectOfType<DialogueManager>().diagActive && !artifactsTotalDiag)
        {
            TriggerDialogue(FindObjectOfType<DialogueManager>().artifactsObtained);
            artifactsTotalDiag = true;
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
