using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("INTERACTABLE SCRIPT");
    }

    // Update is called once per frame
    void Update() {
        if(isInRange) {
            if(Input.GetKeyDown(interactKey)) {
                interactAction.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("INTERACTABLE trigger enter. ");
        if(other.gameObject.CompareTag("Player")) {
            isInRange = true;
            Debug.Log("Esta en rango");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isInRange = false;
            Debug.Log("Ha salido del rango");
        }
    }
}
