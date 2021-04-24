using UnityEngine;
using System.Collections;

public class SceneElementsInteraction : MonoBehaviour {

    private GameObject realCharacter;

    private GameObject reflectedCharacter;
    private Renderer reflectedRenderer;
    private Light reflectedLight;

    private GameObject spotLight;

    private float mirrorZ;
    private float reflectedDepth;

    private AudioManager audioManager;
    private Collider col;


    void Start() {
        audioManager = FindObjectOfType<AudioManager>();

        realCharacter = GameObject.Find("RealCharacter");

        reflectedCharacter = GameObject.Find("ReflectedCharacter");
        reflectedRenderer = reflectedCharacter.GetComponentInChildren<Renderer>();
        reflectedLight = reflectedCharacter.GetComponentInChildren<Light>();

        mirrorZ = GameObject.Find("Mirror").transform.position.z;

        CreateSpotLight();
    }

    private void FixedUpdate() {
        if (spotLight.activeSelf) {
            Color currentColor = spotLight.GetComponent<Light>().color;
            UpdateSpotLightPosition(currentColor);
        }
    }

    void OnTriggerEnter(Collider other) {
        // Disable ReflectedCharacter on ReflectionHider element collider Enter
        if (other.gameObject.tag == "ReflectionDisabler") {
            UpdateSpotLightProperties(true, Color.red);
            ToggleReflectedCharacter(false);
            audioManager.PlaySound("Shield Energy");
        }

        if (other.gameObject.tag == "ReflectionHider") {
            UpdateSpotLightProperties(true, Color.blue);
            ToggleReflectedCharacter(false);
            audioManager.PlaySound("Shield Energy");
        }

        if (other.gameObject.tag == "ReflectionHiderWithDepth") {
            UpdateSpotLightProperties(true, Color.green);
            ToggleReflectedCharacter(false);
            audioManager.PlaySound("Shield Energy");
        }
       
    }

    void OnTriggerExit(Collider other) {
        // Enable ReflectedCharacter on ReflectionHider element collider Exit
        if (other.gameObject.tag == "ReflectionDisabler") {
            ToggleReflectedCharacter(true);
            UpdateSpotLightProperties(false, Color.red);
            audioManager.StopSound("Shield Energy");
            StartCoroutine(audioManager.StopSound("Shield Energy"));
        }

        if (other.gameObject.tag == "ReflectionHider") {
            reflectedCharacter.transform.position = new Vector3(realCharacter.transform.position.x,
                                                                reflectedCharacter.transform.position.y,
                                                                reflectedCharacter.transform.position.z);
            ToggleReflectedCharacter(true);
            UpdateSpotLightProperties(false, Color.blue);
            StartCoroutine(audioManager.StopSound("Shield Energy"));
        }

        if (other.gameObject.tag == "ReflectionHiderWithDepth") {
            reflectedDepth = realCharacter.transform.position.z - mirrorZ;
            reflectedCharacter.transform.position = new Vector3(realCharacter.transform.position.x,
                                                                reflectedCharacter.transform.position.y,
                                                                mirrorZ - reflectedDepth);
            ToggleReflectedCharacter(true);
            UpdateSpotLightProperties(false, Color.green);
            StartCoroutine(audioManager.StopSound("Shield Energy"));
        }

    }

    private void CreateSpotLight() {
        spotLight = new GameObject("Spot Light");
        spotLight.transform.position = reflectedCharacter.transform.position;
        spotLight.transform.Rotate(90f, 0f, 0f);
        Light spotLightComponent = spotLight.AddComponent<Light>();
        spotLightComponent.type = LightType.Spot;
        spotLightComponent.color = Color.red;
        spotLightComponent.range = 20f;
        spotLightComponent.spotAngle = 40f;
        spotLightComponent.intensity = 7f;

        spotLight.SetActive(false);
    }

    private void UpdateSpotLightProperties(bool isActive, Color color) {
        spotLight.GetComponent<Light>().color = color;
        UpdateSpotLightPositionWhenEnter(color); //actualizar tambien la posicion
        spotLight.SetActive(isActive);
    }

    private void UpdateSpotLightPositionWhenEnter(Color color) {
        Transform reflectedSpotLight = reflectedCharacter.GetComponentInChildren<Light>().transform;
        if (color == Color.red) {
            UpdateSpotLightPosition(color);
        } else if (color == Color.blue) {
            reflectedDepth = realCharacter.transform.position.z - mirrorZ;
            float posZ = mirrorZ - reflectedDepth;
            spotLight.transform.position = new Vector3(realCharacter.transform.position.x,
                                                        reflectedSpotLight.position.y,
                                                        posZ);

        } else if (color == Color.green) {
            UpdateSpotLightPosition(color);
        }
    }
    private void UpdateSpotLightPosition(Color color) {
        Transform reflectedSpotLight = reflectedCharacter.GetComponentInChildren<Light>().transform;
        if (color == Color.red) {
            spotLight.transform.position = new Vector3(reflectedCharacter.transform.position.x,
                                                        reflectedSpotLight.position.y,
                                                        reflectedCharacter.transform.position.z);
        } else if (color == Color.blue) {
                spotLight.transform.position = new Vector3(realCharacter.transform.position.x,
                                                            reflectedSpotLight.position.y,
                                                            reflectedCharacter.transform.position.z);

        } else if (color == Color.green) {
                reflectedDepth = realCharacter.transform.position.z - mirrorZ;
                float posZ = mirrorZ - reflectedDepth;
                spotLight.transform.position = new Vector3(realCharacter.transform.position.x,
                                                            reflectedSpotLight.position.y,
                                                            posZ);
        }
        
    }

    private void ToggleReflectedCharacter(bool visible) {
        reflectedRenderer.enabled = visible;
        reflectedLight.enabled = visible;
    }

}
