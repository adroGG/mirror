using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeRevealer : MonoBehaviour {

    [SerializeField]
    GameObject codePart1, codePart2;
    AudioManager audioManager;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other) {
        audioManager.PlaySound("Shield Energy");
        codePart1.SetActive(true);
        codePart2.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        StartCoroutine(audioManager.StopSound("Shield Energy"));
        codePart1.SetActive(false);
        codePart2.SetActive(false);
    }

}
