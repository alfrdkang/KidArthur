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

    public bool keyObtained;
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

    // Start is called before the first frame update
    void Start()
    {
        keyObtained = false;
        key.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Collectable collectObj))
            {
                float Rotate = rotateSpeed * Time.deltaTime;
                hitInfo.collider.gameObject.transform.Rotate(Rotate, Rotate, 0);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hitInfo.collider.gameObject.name == "Key")
                    {
                        keyObtained = true;
                        key.enabled = true;
                    } 
                    else if (hitInfo.collider.gameObject.name == "StrengthOrb")
                    {
                        strengthOrb.sprite = strengthOrbTrue;
                    }
                    else if (hitInfo.collider.gameObject.name == "UpdraftOrb")
                    {
                        updraftOrb.sprite = updraftOrbTrue;
                    }
                    else if (hitInfo.collider.gameObject.name == "JumpOrb")
                    {
                        jumpOrb.sprite = jumpOrbTrue;
                    } 
                    else if (hitInfo.collider.gameObject.name == "DashOrb")
                    {
                        dashOrb.sprite = dashOrbTrue;
                    }
                    collectObj.Interact();
                }
            }
            if (hitInfo.collider.gameObject.TryGetComponent(out DoorScript door))
            {
                if (Input.GetKeyDown(KeyCode.E) && keyObtained == true)
                {
                    door.Interact();
                } else if (Input.GetKeyDown(KeyCode.E) && keyObtained == false)
                {
                    Debug.Log("This door is locked.");
                }
            }
        }
    }
}
