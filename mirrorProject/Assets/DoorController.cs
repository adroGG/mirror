using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    Animator doorAnimator;

    void Start() {
        doorAnimator = transform.GetComponent<Animator>();
    }

    public void ManageDoor() {
        if(isOpen) {
            isOpen = false;
            Debug.Log("Door Closed");
            doorAnimator.SetBool("character_nearby", false);
        } else {
            isOpen = true;
            Debug.Log("Door Opened");
            doorAnimator.SetBool("character_nearby", true);
        }
    }

}
