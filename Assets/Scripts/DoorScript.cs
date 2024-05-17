/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 16/05/2024
 * Date Modified: 16/05/2024
 * Description: Locked Doors
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    /// <summary>
    /// Fetches Animator Component
    /// </summary>
    [SerializeField] private Animator Door = null;

    /// <summary>
    /// Tracks if locked door is open or closed
    /// </summary>
    public bool doorOpened;

    /// <summary>
    /// Player Interaction Function
    /// </summary>
    public void Interact()
    {
        if (doorOpened == true)
        {
            Door.Play("DoorCloseL", 0, 0.0f);
            doorOpened = false;
        } else
        {
            Door.Play("DoorOpenL", 0, 0.0f);
            doorOpened = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
