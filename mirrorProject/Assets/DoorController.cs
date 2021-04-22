
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    Animator doorAnimator;
    ParticleSystem openDoorParticles;

    void Start() {
        doorAnimator = transform.GetComponent<Animator>();
        openDoorParticles = transform.GetComponentInChildren<ParticleSystem>();
    }

    public void ManageDoor() {
        if(isOpen) {
            isOpen = false;
            doorAnimator.SetBool("character_nearby", false);
            openDoorParticles.Stop();
        } else {
            isOpen = true;
            doorAnimator.SetBool("character_nearby", true);
            openDoorParticles.Play();
        }
    }

}
