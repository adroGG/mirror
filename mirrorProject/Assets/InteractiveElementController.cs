using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElementController : MonoBehaviour {

    private bool boxIsActive;
    private GameObject reflectionDisablerTrigger;

    AudioManager audioManager;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        if (transform.Find("ReflectionDisablerTrigger") != null) {
            reflectionDisablerTrigger = transform.Find("ReflectionDisablerTrigger").gameObject;
        }
    }

    public void ToggleBox() {
        boxIsActive = !boxIsActive;
        if (audioManager.CheckIfIsPlaying("Shield Energy")) {
            Debug.Log("Detecta que está sonando. ");
            audioManager.StopSound("Shield Energy");
        }
        reflectionDisablerTrigger.SetActive(!reflectionDisablerTrigger.activeSelf);
    }

    public void OpenNumPad() {

    }

    public string HUDTextDescription() {
        //puedo ver cual es el object al que está ligado este script para dar distintas descripciones
        return "Click izquierdo para interactuar";
    }

}
