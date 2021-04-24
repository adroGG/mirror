using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {


    public Sound[] sounds;

    // queremos un singleton
    public static AudioManager instance;
    private float fadeOutTime = 0.5f;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        PlaySound("MainTheme");
    }

    public void PlaySound(string name) {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("El AudioManager no encuentra el sonido con nombre " + name);
            return;
        }
        s.source.Play();
    }

    // Uso IEnumerator para poder llamarlo con StartCoroutine() y hacer el efecto de FadeOut.
    // De otra forma el sonido acaba de forma demasiado drástica. 
    public IEnumerator StopSound(string name) {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("El AudioManager no encuentra el sonido con nombre " + name);
            yield break;
        }

        // Bloque encargado de hacer el fade out
        for (float t = 0f; t < fadeOutTime; t += Time.deltaTime) {
            s.source.volume = (1 - (t / fadeOutTime));
            yield return null;
        }

        s.source.Stop();
        s.source.volume = 1f; // Aunque el source se detenga, el volumen seguiría a 0 sin esta linea y no sonaría en la segunda ocasion
    }

    public bool CheckIfIsPlaying(string name) {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s.source != null) {
            return s.source.isPlaying;
        }
        return false;
    }
}
