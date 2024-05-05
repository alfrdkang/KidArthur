using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator DoorL = null;
    [SerializeField] private Animator DoorR = null;

    [SerializeField] private bool Trigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Trigger)
            {
                DoorL.Play("DoorOpenL", 0, 0.0f);
                DoorR.Play("DoorOpenR", 0, 0.0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Trigger)
            {
                DoorL.Play("DoorCloseL", 0, 0.0f);
                DoorR.Play("DoorCloseR", 0, 0.0f);
            }
        }
    }
}
