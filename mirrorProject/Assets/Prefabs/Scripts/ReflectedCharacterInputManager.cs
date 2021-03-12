using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class ReflectedCharacterInputManager : MonoBehaviour {

    private Animator animator;
    int isWalkingHash;
    int isRunningHash;
    private Vector2 moveDirection; 

    PlayerInput input;

    public void Move(InputAction.CallbackContext context) {

        moveDirection = context.ReadValue<Vector2>();
        
        if(context.performed) {
            ManageMovement(context.ReadValue<Vector2>());
        }

        if(context.canceled) {
            ResetAnimations();
        }
    }

    void ResetAnimations() {
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkingBackwards", false);
        animator.SetBool("isRunning", false);
    }

    void ManageMovement(Vector2 direction) {
        if(direction[1] < -0.5f) {
            animator.SetBool("isWalkingBackwards", true);
        }
        
        if(direction[1] > 0.5f) {
            animator.SetBool("isWalking", true);
        }
    }

    void ManageRotation(Vector2 direction) {
        bool rotaIzq = direction[0] < -0.5;
        bool rotaDerecha = direction[0] > 0.5;
        bool anda = direction[1] >= -0.5f;
        bool andaHaciaAtras = direction[1] < -0.5f;

        if(rotaIzq) { // rotar izquierda
            ResetAnimations();
            if(anda) { //rotar con animacion de andar
                animator.SetBool("isWalking", true);
                gameObject.transform.Rotate(Vector3.up * -1 * (100f * Time.deltaTime));
            } else if(andaHaciaAtras) { //rotar con animacion de andar hacia atrás
                animator.SetBool("isWalkingBackwards", true);
                gameObject.transform.Rotate(Vector3.up * (100f * Time.deltaTime));
            }
        }

        if(rotaDerecha) { // rotar derecha
            ResetAnimations();
            if(anda) {
                animator.SetBool("isWalking", true);
                gameObject.transform.Rotate(Vector3.up * (100f * Time.deltaTime));
            } else if(andaHaciaAtras) {
                animator.SetBool("isWalkingBackwards", true);
                gameObject.transform.Rotate(Vector3.up * -1 *(100f * Time.deltaTime));
            }
        }
    }

    private void FixedUpdate() {
        ManageRotation(moveDirection);
    }

    void Awake() {
        
    }

    void Start() {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }


    void HandleMovement() {
        bool isWalkingBackwards = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
    }

    void OnEnable() {
        // input.CharacterControls.Enable();
    }

    void OnDisable() {
        // input.CharacterControls.Disable();
    }
}

