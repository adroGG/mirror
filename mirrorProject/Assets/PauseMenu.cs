using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject ATM;

    [SerializeField]
    private bool terminalIsActive  = false;

    private AudioManager audioManager;


    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        ATM = GameObject.Find("ATM");
        foreach (Transform t in transform) {
            if (t.name == "OptionsMenu") {
                optionsMenuUI = t.gameObject;
            }
        }
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (ATM != null) {
                if (GameObject.Find("CanvasNumerico") != null) {
                    if (GameObject.Find("CanvasNumerico").gameObject.activeSelf) {
                        terminalIsActive = true;
                    } else {
                        terminalIsActive = false;
                    }
                }
            }

            if (isPaused && !optionsMenuUI.activeSelf && !terminalIsActive) {
                Resume();
            } else if (isPaused && optionsMenuUI.activeSelf && !terminalIsActive) {
                BackFromOptions();
            } else if (!terminalIsActive) {
                Pause();
            }

            terminalIsActive = false;

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

    public void MouseHover() {
        audioManager.PlaySound("ButtonHover");
    }

}
