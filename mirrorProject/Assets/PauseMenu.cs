using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused && !optionsMenuUI.activeSelf) {
                Resume();
            } else if(isPaused && optionsMenuUI.activeSelf) {
                BackFromOptions();
            } else {
                Pause();
            }
        }
    }

    void Start() {
        foreach (Transform t in transform) {
            if (t.name == "OptionsMenu") {
                optionsMenuUI = t.gameObject;
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // se puede hacer camaras lentas usando esto
        isPaused = true;

    }

    void BackFromOptions() {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

}
