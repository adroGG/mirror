using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class RealCharacterInputManager : MonoBehaviour {

    private Animator animator, realAnimator, reflectedAnimator;
    private GameObject realCharacter;
    private GameObject reflectedCharacter;
    private Renderer reflectedRenderer;

    float rotationSpeed = 80f;
    float moveSpeed = 3.5f;
    float speedModifier, realSpeedModifier, reflectedSpeedModifier;

    private Vector2 moveDirection;

    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audioSource;

    void Start() {
        realCharacter = GameObject.Find("RealCharacter");
        realAnimator = realCharacter.GetComponent<Animator>();

        reflectedCharacter = GameObject.Find("ReflectedCharacter");
        reflectedRenderer = GetComponentInChildren<Renderer>();
        reflectedAnimator = reflectedCharacter.GetComponent<Animator>();
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        speedModifier = realSpeedModifier = reflectedSpeedModifier = 1f;
    }


    private void Step() {
        // Aunque tenga 0 referencias, esta funcion se
        // llama desde los eventos del Animator de Character
        audioSource.PlayOneShot(audioClip);
    }

    public void Move(InputAction.CallbackContext context) {
        moveDirection = context.ReadValue<Vector2>();
        if (context.performed) {
            ManageMovement(context.ReadValue<Vector2>());
        }
        if (context.canceled) {
            ResetAnimations();
        }
    }

    void ResetAnimations() {
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkingBackwards", false);
        animator.SetBool("isRunning", false);
    }

    void ManageMovement(Vector2 direction) {
        ResetAnimations();
        if (direction[1] < -0.5f) {
            animator.SetBool("isWalkingBackwards", true);
            realSpeedModifier = reflectedSpeedModifier = 2.8f;
        }

        if (direction[1] > 0.5f && !realAnimator.GetBool("isPushing")) {
            animator.SetBool("isRunning", true);
            realSpeedModifier = reflectedSpeedModifier = 1f;
        }

        if (direction[1] > 0.5f && realAnimator.GetBool("isPushing")) {
            realAnimator.SetBool("isRunning", true);
            reflectedAnimator.SetBool("isRunning", false);
            reflectedAnimator.SetBool("isWalking", true);
            realSpeedModifier = 1f;
            reflectedSpeedModifier = 4f;
        }

        if(gameObject.name == "RealCharacter")
            speedModifier = realSpeedModifier;
        else if(gameObject.name == "ReflectedCharacter")
            speedModifier = reflectedSpeedModifier;
        
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(0, 0, direction.y);
        transform.position += move * (moveSpeed / speedModifier) * Time.deltaTime;
    }


    void ManageRotation(Vector2 direction) {
        bool rotaIzq = direction[0] < -0.5;
        bool rotaDerecha = direction[0] > 0.5;
        bool anda = direction[1] >= -0.5f;
        bool andaHaciaAtras = direction[1] < -0.5f;

        if (rotaIzq) { // rotar izquierda
            if (anda) { // rotar con animacion de andar
                animator.SetBool("isWalking", true);
                realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            } else if (andaHaciaAtras) { // rotar con animacion de andar hacia atrás
                animator.SetBool("isWalkingBackwards", true);
                realCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * (-rotationSpeed) * Time.deltaTime);
            }
        }

        if (rotaDerecha) { // rotar 
            if (anda) {
                animator.SetBool("isWalking", true);
                realCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);

            } else if (andaHaciaAtras) {
                animator.SetBool("isWalkingBackwards", true);
                realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * (rotationSpeed) * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate() {
        if (reflectedRenderer.enabled) {
            ManageMovement(moveDirection);
            ManageRotation(moveDirection);
        }
    }

}

