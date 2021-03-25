using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public UnityEvent interactAction;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

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
