using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator Door = null;
    bool doorOpened;
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
