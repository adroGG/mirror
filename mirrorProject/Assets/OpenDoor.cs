using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour {
    public UnityEvent interactAction;
    
    private Collider col;
    private SceneScript levelManager;
    private AudioManager audioManager;

    private void Start() {
        levelManager = FindObjectOfType<SceneScript>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter(Collider other) {
        if(col == null) { // Preventing diferent colliders to open doors
            col = other;
            levelManager.doorTriggered(gameObject.transform.parent.name);
            // sonido de abrir puerta
            audioManager.PlaySound("DoorOpen");
            interactAction.Invoke();
        }
    }

    void OnTriggerExit(Collider other) {
        if (col != null) { // Preventing diferent colliders to close doors
            levelManager.doorUntriggered(gameObject.transform.parent.name);
            // sonido de cerrar puerta
            audioManager.PlaySound("CloseDoor");
            interactAction.Invoke();
            col = null;
        }
    }

}
