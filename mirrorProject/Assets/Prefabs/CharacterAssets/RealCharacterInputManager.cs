using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class RealCharacterInputManager : MonoBehaviour {

    private Animator animator;
    
    private GameObject realCharacter;
    private GameObject reflectedCharacter;

    float rotationSpeed = 80f;
    float moveSpeed = 3.5f;
    float speedModifier = 1f;

    int isWalkingHash;
    int isRunningHash;
    int isWalkingBackwardsHash;

    private Vector2 moveDirection; 

    PlayerInput input;

    void Start() {
        realCharacter = GameObject.Find("RealCharacter");
        reflectedCharacter = GameObject.Find("ReflectedCharacter");

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
    }

    
    void OnTriggerEnter(Collider other) {

        Debug.Log("other: " + other);

        // Disable ReflectedCharacter on ReflectionHider element collider Enter
        if (other.gameObject.tag == "ReflectionDisabler") {
            reflectedCharacter.SetActive(false);
        }
        
        if(other.gameObject.tag == "ReflectionHider") {
            reflectedCharacter.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other) {
        // Enable ReflectedCharacter on ReflectionHider element collider Exit
        if(other.gameObject.tag == "ReflectionDisabler") {
            reflectedCharacter.SetActive(true);
        }

        if(other.gameObject.tag == "ReflectionHider") {
            reflectedCharacter.transform.position = new Vector3(realCharacter.transform.position.x, 
                                                                reflectedCharacter.transform.position.y, 
                                                                reflectedCharacter.transform.position.z);
            reflectedCharacter.SetActive(true);
        }
    }


    public void Move(InputAction.CallbackContext context) {
        moveDirection = context.ReadValue<Vector2>();
        
        if(context.performed) {
            ManageMovement(context.ReadValue<Vector2>());
        }

        if(context.canceled) {
            ResetAnimations();
        }
    }

    void ResetAnimations()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkingBackwards", false);
        animator.SetBool("isRunning", false);
    }

    void ManageMovement(Vector2 direction) {
        if(direction[1] < -0.5f) {
            animator.SetBool("isWalkingBackwards", true);
            speedModifier = 2f;
        }
        
        if(direction[1] > 0.5f) {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", true); // Correr
            speedModifier = 1f;
        }

        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(0, 0, direction.y);
        transform.position += move * (moveSpeed / speedModifier) * Time.deltaTime;
    }


    void ManageRotation(Vector2 direction) {
        bool rotaIzq = direction[0] < -0.5;
        bool rotaDerecha = direction[0] > 0.5;
        bool anda = direction[1] >= -0.5f;
        bool andaHaciaAtras = direction[1] < -0.5f;

        if(rotaIzq) { // rotar izquierda
            if (anda)
            { //rotar con animacion de andar
                animator.SetBool("isWalking", true);
                realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            }
            else if (andaHaciaAtras)
            { //rotar con animacion de andar hacia atrás
                animator.SetBool("isWalkingBackwards", true);
                realCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * (-rotationSpeed / 2) * Time.deltaTime);
            }
        }

        if(rotaDerecha) { // rotar 
                if (anda)
                {
                    animator.SetBool("isWalking", true);
                    realCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    reflectedCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);

                }
                else if (andaHaciaAtras)
                {
                    animator.SetBool("isWalkingBackwards", true);
                    realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                    reflectedCharacter.transform.Rotate(Vector3.up * (rotationSpeed / 2) * Time.deltaTime);
                }
        }
    }

    private void FixedUpdate() {
        ManageMovement(moveDirection);
        ManageRotation(moveDirection);
    }

    void HandleMovement() {
        bool isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
    }
}

