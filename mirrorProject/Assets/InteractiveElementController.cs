using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElementController : MonoBehaviour {

    private bool boxIsActive;
    private GameObject reflectionDisablerTrigger;
    private GameObject ATM, canvas;

    AudioManager audioManager;
    private SceneScript sceneScript;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        sceneScript = FindObjectOfType<SceneScript>();

        if (transform.Find("ReflectionDisablerTrigger") != null) {
            reflectionDisablerTrigger = transform.Find("ReflectionDisablerTrigger").gameObject;
        } else if (transform.parent.Find("ATM") != null) {
            ATM = transform.parent.Find("ATM").gameObject;
            canvas = ATM.transform.Find("CanvasNumerico").gameObject;
        }
    }

    public void ToggleBox() {
        if(!MenuIsOpen()) {
            boxIsActive = !boxIsActive;
            if (audioManager.CheckIfIsPlaying("Shield Energy")) {
                Debug.Log("Detecta que está sonando. ");
                audioManager.StopSound("Shield Energy");
            }
            reflectionDisablerTrigger.SetActive(!reflectionDisablerTrigger.activeSelf);
        }
    }

    public void OpenNumPad() {
        if (!MenuIsOpen()) {
            if (!sceneScript.levelCompleted) {
                Debug.Log("Debe abrirse el num pad");
                canvas.SetActive(true);
                Time.timeScale = 0f;
                audioManager.PlaySound("OpenTerminal");
            }
        }
    }

    public string HUDTextDescription() {
        //puedo ver cual es el object al que está ligado este script para dar distintas descripciones
        return "Click izquierdo para activar";
    }

    private bool MenuIsOpen() {
        if(Time.timeScale == 0f) {
            return true;
        } else {
            return false;
        }
    }
    
}
