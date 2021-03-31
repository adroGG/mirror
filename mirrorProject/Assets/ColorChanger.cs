using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {

    Image img;

    [SerializeField] [Range(0f, 1f)] float lerpTime;
    [SerializeField] Color[] targetColors;

    private int colorIndex = 0;
    private float t = 0f;
    private int len;

    void Start() {
        img = GetComponent<Image>();
        len = targetColors.Length;
    }

    void FixedUpdate() {
        // Mathf.PingPong(Time.deltaTime * speed, 1.0f)
        img.color = Color.Lerp(img.color, targetColors[colorIndex], lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

        if (t > 0.9f) {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;

        }

    }
}
