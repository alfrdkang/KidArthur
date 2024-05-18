/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 16/05/2024
 * Date Modified: 16/05/2024
 * Description: Player Interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    public bool keyObtained = false;
    public bool updraftOrbCollected = false;
    public bool dashOrbCollected = false;
    public float rotateSpeed = 100.0f;

    public Image strengthOrb;
    public Image updraftOrb;
    public Image jumpOrb;
    public Image dashOrb;
    public Image key;

    public Sprite strengthOrbTrue;
    public Sprite updraftOrbTrue;
    public Sprite jumpOrbTrue;
    public Sprite dashOrbTrue;

    public TextMeshProUGUI interactText;

    /// <summary>
    /// Player's Animator Component
    /// </summary>
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
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
            if (hitInfo.collider.gameObject.TryGetComponent(out Collectable collectObj))
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
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    } 
                    else if (hitInfo.collider.gameObject.name == "StrengthOrb")
                    {
                        strengthOrb.sprite = strengthOrbTrue;
                        GameObject[] movables = GameObject.FindGameObjectsWithTag("Movable");
                        foreach (GameObject mvble in movables)
                            mvble.GetComponent<Rigidbody>().mass = 12;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().strengthOrb = true;
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    }
                    else if (hitInfo.collider.gameObject.name == "UpdraftOrb")
                    {
                        updraftOrb.sprite = updraftOrbTrue;
                        updraftOrbCollected = true;
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    }
                    else if (hitInfo.collider.gameObject.name == "JumpOrb")
                    {
                        jumpOrb.sprite = jumpOrbTrue;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().maxJumpCount = 2;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().jumpCount = 2;
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    } 
                    else if (hitInfo.collider.gameObject.name == "DashOrb")
                    {
                        dashOrb.sprite = dashOrbTrue;
                        dashOrbCollected = true;
                        playerAnimator.Play("LevelUp_Battle_SwordAndShield");
                    }
                    collectObj.Interact();
                }
            }
            if (hitInfo.collider.gameObject.TryGetComponent(out DoorScript door))
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
        } else
        {
            interactText.enabled = false;
        }
    }
}
