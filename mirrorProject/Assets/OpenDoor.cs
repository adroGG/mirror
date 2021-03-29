using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public UnityEvent interactAction;

    void OnTriggerEnter() {
        Debug.Log("Entrando en el cubo. Ahora debe abrirse la puerta.");
        interactAction.Invoke();
    }

    void OnTriggerExit()
    {
        Debug.Log("Saliendo del cubo. Ahora debe cerrarse la puerta.");
        interactAction.Invoke();
    }

}
