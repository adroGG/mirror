using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour {
    public UnityEvent interactAction;

    private Collider col;

    // añadir un bool para que la puerta permanezca abierta hasta que algo salga

    void OnTriggerEnter(Collider other) {
        // Preventing diferent colliders to open and close doors
        if(col == null) {
            col = other;
            interactAction.Invoke();
            Debug.Log("Entrando en el cubo. Ahora debe abrirse la puerta.");
        }
    }

    void OnTriggerExit(Collider other) {
        // Preventing diferent colliders to open and close doors
        if (col != null) {
            Debug.Log("Saliendo del cubo. Ahora debe cerrarse la puerta.");
            interactAction.Invoke();
            col = null;
        }
    }

}
