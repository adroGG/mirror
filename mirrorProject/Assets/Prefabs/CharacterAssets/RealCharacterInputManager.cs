using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class RealCharacterInputManager : MonoBehaviour {

    private Animator animator;
    private GameObject realParent;
    private GameObject reflectedParent;

    private GameObject realCharacter;
    private GameObject reflectedCharacter;

    float rotationSpeed = 80f;
    float moveSpeed = 1f;

    int isWalkingHash;
    int isRunningHash;
    private Vector2 moveDirection; 

    PlayerInput input;

    void Start() {
        realCharacter = GameObject.Find("RealCharacter");
        reflectedCharacter = GameObject.Find("ReflectedCharacter");

        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision detected");
        Debug.Log(collision);
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

        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(0, 0, direction.y);
        transform.position += move * moveSpeed * Time.deltaTime;
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
                realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);


            } else if(andaHaciaAtras) { //rotar con animacion de andar hacia atrás
                animator.SetBool("isWalkingBackwards", true);
                realCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);

            }
        }

        if(rotaDerecha) { // rotar derecha
            ResetAnimations();
            if(anda) {
                animator.SetBool("isWalking", true);
                realCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);

            } else if(andaHaciaAtras) {
                animator.SetBool("isWalkingBackwards", true);
                realCharacter.transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                reflectedCharacter.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            }
        }
    }

    private void FixedUpdate() {
        ManageMovement(moveDirection);
        ManageRotation(moveDirection);
    }

    void Awake() {
        
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

