using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFinisher : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(FinishLevel());
    }

    private IEnumerator FinishLevel() {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

}
