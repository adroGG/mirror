using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class RealCharacterInputManager : MonoBehaviour {

    private Animator animator;

    private GameObject realCharacter;
    private GameObject reflectedCharacter;

    private Renderer reflectedRenderer;


    float rotationSpeed = 80f;
    float moveSpeed = 3.5f;
    float speedModifier = 1f;

    private Vector2 moveDirection;


    void Start() {
        realCharacter = GameObject.Find("RealCharacter");
        reflectedCharacter = GameObject.Find("ReflectedCharacter");

        reflectedRenderer = GetComponentInChildren<Renderer>();

        animator = GetComponent<Animator>();
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
            speedModifier = 2f;
        }

        if (direction[1] > 0.5f) {
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

        if (rotaIzq) { // rotar izquierda
            if (anda) { //rotar con animacion de andar
                animator.SetBool("isWalking", true);
                realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            } else if (andaHaciaAtras) { //rotar con animacion de andar hacia atrás
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

