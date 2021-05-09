using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericPanel : MonoBehaviour {

    [SerializeField]
    private TMPro.TextMeshProUGUI passText;
    [SerializeField]
    string validCode = "1111";
    [SerializeField]
    private GameObject realDoor, reflectedDoor;
    private Animator realAnimator, reflectedAnimator;
    string code = "";
    private SceneScript sceneScript;
    private AudioManager audioManager;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        sceneScript = FindObjectOfType<SceneScript>();
        realAnimator = realDoor.GetComponent<Animator>();
        reflectedAnimator = reflectedDoor.GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            transform.parent.gameObject.SetActive(false);
            code = "";
            Time.timeScale = 1f;
            // sonido de cerrar?
            // audioManager.PlaySound("SONIDO DE CERRAR");
        }
    }

    public void pressOne() {
        code += "1";
        passText.text += '1';
        IsValidCode();
    }

    public void pressTwo() {
        code += "2";
        passText.text += '2';
        IsValidCode();
    }

    public void pressThree() {
        code += "3";
        passText.text += '3';
        IsValidCode();
    }

    public void pressFour() {
        
        code += "4";
        passText.text += '4';
        IsValidCode();
    }

    public void pressFive() {
        code += "5";
        passText.text += '5';
        IsValidCode();
    }

    public void pressSix() {
        code += "6";
        passText.text += '6';
        IsValidCode();
    }

    public void pressSeven() {
        code += "7";
        passText.text += '7';
        IsValidCode();
    }

    public void pressEight() {
        code += "8";
        passText.text += '8';
        IsValidCode();
    }
    public void pressNine() {
        code += "9";
        passText.text += '9';
        IsValidCode();
    }

    private void IsValidCode() {
        audioManager.PlaySound("ButtonPress");
        if (validCode == code) { CodeIsValid(); } 
        else if(code.Length == 4) { CodeIsNotValid(); }
    }

    private void CodeIsValid() {
        Debug.Log("VALID CODE.");
        passText.text = "Correct password. The door is open. ";
        realAnimator.SetBool("character_nearby", true);
        reflectedAnimator.SetBool("character_nearby", true);
        sceneScript.forceWin();
        audioManager.PlaySound("CorrectPassword");
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        // desactivar el panel e imposibilitar volverlo a abrir
    }

    private void CodeIsNotValid() {
        Debug.Log("INVALID CODE.");
        code = "";
        passText.text = "Invalid password. \nEnter password: ";
        audioManager.PlaySound("WrongPassword");
    }


}
