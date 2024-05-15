/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 6/05/2024
 * Date Modified: 6/05/2024
 * Description: Automatic Door Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    /// <summary>
    /// Fetches animator of left door
    /// </summary>
    [SerializeField] private Animator DoorL = null;

    /// <summary>
    /// Fetches animator of right door
    /// </summary>
    [SerializeField] private Animator DoorR = null;

    /// <summary>
    /// Player Enters Trigger Zone
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorL.Play("DoorOpenL", 0, 0.0f);
            DoorR.Play("DoorOpenR", 0, 0.0f);
        }
    }

    /// <summary>
    /// Player Exits Trigger Zone
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorL.Play("DoorCloseL", 0, 0.0f);
            DoorR.Play("DoorCloseR", 0, 0.0f);
        }
    }
}
