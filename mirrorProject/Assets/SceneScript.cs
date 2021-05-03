﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour {
    [SerializeField]
    private GameObject realDoor, reflectedDoor;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    private TMPro.TextMeshProUGUI interactionText;
    private GameObject realDoorTrigger, reflectedDoorTrigger;
    Animator realDoorAnimator, reflectedDoorAnimator;
    private bool realDoorOpened, reflectedDoorOpened, levelCompleted, hintShown;
    AudioManager audioManager;

    private float tipTimer, tipTrigger, panelFade;
    string[] tips = { 
                      "Tengo que salir de aquí"
                    , "¿Quién es ese tipo del reflejo?"
                    , "¿Porqué me está siguiendo?"
                    , "Hace lo mismo que yo..."
                    , "¿Qué es este sitio?"
                    , "No recuerdo nada"
                    , "¿Quién está jugando conmigo?"
    };
    string[] realHints = { 
                      "Quiero irme de aquí"
                    , "¿Porqué tenemos que salir a la vez? Quiero irme solo"
                    , "La puerta se cierra si no la abrimos juntos"
                    
    };

    string[] reflectedHints = {
                      "Quiero irme de aquí"
                    , "¿Porqué tenemos que salir a la vez? Quiero irme solo"
                    , "La puerta se cierra si no la abrimos juntos"

    };

    string[] winningHints = {
                      "Ya puedo salir de aquí"
                    , "Por fin puedo salir de aquí"
                    , "¿A dónde me va a llevar esto?"
                    , "¿Donde lleva este laberinto?"
                    , "Vámonos de este laberinto"
                    , "¿Puedo salir ya?"

    };

    private void Start() {
        levelCompleted = hintShown = false;
        realDoorAnimator = realDoor.GetComponent<Animator>();
        reflectedDoorAnimator = reflectedDoor.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        realDoorTrigger = realDoor.transform.Find("triggerOpen").gameObject;
        reflectedDoorTrigger = reflectedDoor.transform.Find("triggerOpen").gameObject;

        resetTipValues();
    }

    private void FixedUpdate() {
        tipTimer += Time.deltaTime;

        if (tipTimer > tipTrigger) {
            showTip();
        }

        if(realDoorOpened && !hintShown && !Panel.activeSelf) {
            showRealHint();
        }

        if (reflectedDoorOpened && !hintShown && !Panel.activeSelf) {
            showReflectedHint();
        }

        if(levelCompleted && !hintShown && !Panel.activeSelf) {
            showWinningHint();
        }

        // Hacer desaparecer el mensaje
        if (Panel.activeSelf == true) {
        panelFade += Time.deltaTime;
        }
        if(panelFade > 3.5f) {
            HUDUpdate(false, "");
        }
    }

    public void doorTriggered(string gameObjName) {
        if (gameObjName == "RealDoor") {
            realDoorOpened = true;
            hintShown = false;
        }
        if (gameObjName == "ReflectedDoor") { reflectedDoorOpened = true; }
        reactToDoorsStatus();
    }

    public void doorUntriggered(string gameObjName) {
        if (gameObjName == "RealDoor") { realDoorOpened = false; }
        if (gameObjName == "ReflectedDoor") { reflectedDoorOpened = false; }
        reactToDoorsStatus();
    }

    private void reactToDoorsStatus() {
        if (realDoorOpened && reflectedDoorOpened) { levelCompleted = true; }
    }

    public void checkWinCondition() {
        if (levelCompleted) {
            realDoorAnimator.SetBool("winCondition", true);
            reflectedDoorAnimator.SetBool("winCondition", true);
            audioManager.PlaySound("WinCondition");
            realDoorTrigger.gameObject.SetActive(false);
            reflectedDoorTrigger.gameObject.SetActive(false);
        }
    }

    private void showTip() {
        int idx = Random.Range(0, tips.Length);
        HUDUpdate(true, tips[idx]);
        resetTipValues();
    }
    private void resetTipValues() {
        tipTrigger = Random.Range(10f, 30f);
        panelFade = 0f;
        tipTimer = 0f;
    }

    private void showRealHint() {
        int idx = Random.Range(0, realHints.Length);
        HUDUpdate(true, realHints[idx]);
        resetHintValues();
    }
    private void showReflectedHint() {
        int idx = Random.Range(0, reflectedHints.Length);
        HUDUpdate(true, reflectedHints[idx]);
        resetHintValues();
    }
    private void showWinningHint() {
        int idx = Random.Range(0, winningHints.Length);
        HUDUpdate(true, winningHints[idx]);
        resetHintValues();
    }

    private void resetHintValues() {
        panelFade = 0f;
        hintShown = true;
    }
    
    private void HUDUpdate(bool panelStatus, string text) {
        interactionText.text = text;
        Panel.SetActive(panelStatus);
    }

}
