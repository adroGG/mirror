using UnityEngine;

public class SceneElementsInteraction : MonoBehaviour {

    private GameObject realCharacter;
    private GameObject reflectedCharacter;
    private GameObject spotLight;

    private float mirrorZ;
    private float reflectedDepth;


    void Start() {
        realCharacter = GameObject.Find("RealCharacter");
        reflectedCharacter = GameObject.Find("ReflectedCharacter");
        mirrorZ = GameObject.Find("Mirror").transform.position.z;

        createSpotLight();
    }

    void OnTriggerEnter(Collider other) {
        // Disable ReflectedCharacter on ReflectionHider element collider Enter
        if (other.gameObject.tag == "ReflectionDisabler") {
            reflectedCharacter.SetActive(false);
            spotLight.transform.position = new Vector3(reflectedCharacter.transform.position.x, 
                                                        3.5f, 
                                                        reflectedCharacter.transform.position.z);
            spotLight.SetActive(true);
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
            spotLight.SetActive(false);
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

    private void createSpotLight() {
        spotLight = new GameObject("Spot Light");
        spotLight.transform.position = reflectedCharacter.transform.position;
        spotLight.transform.Rotate(90f, 0f ,0f);
        Light spotLightComponent = spotLight.AddComponent<Light>();
        spotLightComponent.type = LightType.Spot;
        spotLightComponent.color = Color.red;
        spotLightComponent.range = 20f;
        spotLightComponent.spotAngle = 40f;
        spotLightComponent.intensity = 7f;

        spotLight.SetActive(false);
    }
}
