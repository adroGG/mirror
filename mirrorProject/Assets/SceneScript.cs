using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour {
    private bool realDoorOpened;
    private bool reflectedDoorOpened;

    private bool levelCompleted;

    private void Start() {
        levelCompleted = false;
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
        if(realDoorOpened && reflectedDoorOpened) {
            levelCompleted = true;
        }

        Debug.Log("IS LEVEL COMPLETED? " + levelCompleted);
    }
}
