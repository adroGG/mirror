using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElementController : MonoBehaviour {

    private bool isActive;
    private GameObject reflectionDisablerTrigger;

    AudioManager audioManager;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        reflectionDisablerTrigger = transform.Find("ReflectionDisablerTrigger").gameObject;
        Debug.Log("reflectionDisablerTrigger: " + reflectionDisablerTrigger);
    }

    public void ToggleBox() {
        isActive = !isActive;
        Debug.Log("Caja toggleada");
        
        if (audioManager.CheckIfIsPlaying("Shield Energy")) {
            Debug.Log("Detecta que está sonando. ");
            audioManager.StopSound("Shield Energy");
        }
        reflectionDisablerTrigger.SetActive(!reflectionDisablerTrigger.activeSelf);

    }


}
