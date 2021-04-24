using UnityEngine;

public class TriggerWin : MonoBehaviour {

    private LevelLoader levelLoader;

    private void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void OnTriggerEnter(Collider other) {
        // Pasa a la siguiente escena cuando el personaje real pasa por la puerta
        // La puerta solo permanece abierta si winCondition == true
        if(other.gameObject.name == "RealCharacter") {
            Debug.Log("Paso a la siguiente escena. ");
            levelLoader.LoadNextLevel();
        }
    }

}