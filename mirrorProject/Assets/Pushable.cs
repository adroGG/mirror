using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {
    private AudioManager audioManager;
    private GameObject realCharacter;
    private Animator realCharacterAnimator;
    private Rigidbody boxRB;

    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private TMPro.TextMeshProUGUI interactionText;

    // Start is called before the first frame update
    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        realCharacter = GameObject.Find("RealCharacter");
        realCharacterAnimator = realCharacter.GetComponent<Animator>();
        boxRB = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!Input.GetKey(KeyCode.E)) {
            StopPushing();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "RealCharacter") {
            if (Input.GetKey(KeyCode.E)) {
                realCharacterAnimator.SetBool("isPushing", true);
                boxRB.constraints = RigidbodyConstraints.FreezeAll;
                boxRB.constraints = ~RigidbodyConstraints.FreezePositionX;

            }
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            StopPushing();
        }

    }

    private void StopPushing() {
        if (realCharacterAnimator.GetBool("isPushing")) {
            realCharacterAnimator.SetBool("isPushing", false);
            boxRB.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "RealCharacter") {
            HUDUpdate(true, "Pulsa E para empujar");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "RealCharacter") {
            realCharacterAnimator.SetBool("isPushing", false);
            HUDUpdate(false, "");
        }
    }

    private void HUDUpdate(bool panelStatus, string text) {
        interactionText.text = text;
        Panel.SetActive(panelStatus);
    }

}
