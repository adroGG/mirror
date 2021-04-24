using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour {
    [SerializeField]
    private GameObject realDoor, reflectedDoor, Panel;
    [SerializeField]
    private TMPro.TextMeshProUGUI interactionText;
    Animator realDoorAnimator, reflectedDoorAnimator;
    private bool realDoorOpened, reflectedDoorOpened, levelCompleted;
    AudioManager audioManager;

    private void Start() {
        levelCompleted = false;
        realDoorAnimator = realDoor.GetComponent<Animator>();
        reflectedDoorAnimator = reflectedDoor.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void doorTriggered(string gameObjName) {
        if (gameObjName == "RealDoor") {
            realDoorOpened = true;
        }
        if (gameObjName == "ReflectedDoor") {
            reflectedDoorOpened = true;
        }
        reactToDoorsStatus();
    }

    public void doorUntriggered(string gameObjName) {
        if (gameObjName == "RealDoor") {
            realDoorOpened = false;
        }
        if (gameObjName == "ReflectedDoor") {
            reflectedDoorOpened = false;
        }
        reactToDoorsStatus();
    }

    private void reactToDoorsStatus() {
        if (realDoorOpened && reflectedDoorOpened) {
            levelCompleted = true;
        }
    }

    public void checkWinCondition() {
        if(levelCompleted) {
            realDoorAnimator.SetBool("winCondition", true);
            reflectedDoorAnimator.SetBool("winCondition", true);
            audioManager.PlaySound("WinCondition");
        }
    }




}
