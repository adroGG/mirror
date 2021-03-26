using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElementsInteraction : MonoBehaviour {

    private GameObject realCharacter;
    private GameObject reflectedCharacter;
    private float mirrorZ;
    private float reflectedDepth;

    void Start() {
        realCharacter = GameObject.Find("RealCharacter");
        reflectedCharacter = GameObject.Find("ReflectedCharacter");
        mirrorZ = GameObject.Find("Mirror").transform.position.z;

    }

    void OnTriggerEnter(Collider other) {

        Debug.Log("other: " + other);

        // Disable ReflectedCharacter on ReflectionHider element collider Enter
        if (other.gameObject.tag == "ReflectionDisabler") {
            reflectedCharacter.SetActive(false);
        }

        if (other.gameObject.tag == "ReflectionHider") {
            reflectedCharacter.SetActive(false);
        }

        if (other.gameObject.tag == "ReflectionHiderWithDepth") {
            reflectedCharacter.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other) {
        // Enable ReflectedCharacter on ReflectionHider element collider Exit
        if (other.gameObject.tag == "ReflectionDisabler") {
            reflectedCharacter.SetActive(true);
        }

        if (other.gameObject.tag == "ReflectionHider") {
            reflectedCharacter.transform.position = new Vector3(realCharacter.transform.position.x,
                                                                reflectedCharacter.transform.position.y,
                                                                reflectedCharacter.transform.position.z);
            reflectedCharacter.SetActive(true);
        }

        if (other.gameObject.tag == "ReflectionHiderWithDepth") {
            reflectedDepth = realCharacter.transform.position.z - mirrorZ;
            reflectedCharacter.transform.position = new Vector3(realCharacter.transform.position.x,
                                                                reflectedCharacter.transform.position.y,
                                                                mirrorZ - reflectedDepth);
            reflectedCharacter.SetActive(true);
        }
    }
}
