using UnityEngine;


public class MainMenu : MonoBehaviour {

    private AudioManager audioManager;
    private LevelLoader levelLoader;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void PlayButton() {
        levelLoader.LoadNextLevel();
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void MouseHover() {
        audioManager.PlaySound("ButtonHover");
    }

}
