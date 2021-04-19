using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    Animator doorAnimator;
    ParticleSystem openDoorParticles;

    void Start() {
        doorAnimator = transform.GetComponent<Animator>();
        openDoorParticles = transform.GetComponentInChildren<ParticleSystem>();

        Debug.Log("Particle system: " + openDoorParticles);
    }

    public void ManageDoor() {
        if(isOpen) {
            isOpen = false;
            Debug.Log("Door Closed");
            doorAnimator.SetBool("character_nearby", false);
            openDoorParticles.Stop();
        } else {
            isOpen = true;
            Debug.Log("Door Opened");
            doorAnimator.SetBool("character_nearby", true);
            openDoorParticles.Play();
        }
    }

}
