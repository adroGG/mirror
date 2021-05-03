using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject Panel;
    public TMPro.TextMeshProUGUI interactionText;

    private InteractiveElementController elementController;

    private void Start() {
        elementController = transform.parent.gameObject.GetComponent<InteractiveElementController>();
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
        if(other.gameObject.name == "RealCharacter") {
            isInRange = true;
            HUDUpdate(true, elementController.HUDTextDescription());
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "RealCharacter") {
            isInRange = false;
            HUDUpdate(false, "");
        }
    }

    private void HUDUpdate(bool panelStatus, string text) {
        interactionText.text = text;
        Panel.SetActive(panelStatus);
    }
}
