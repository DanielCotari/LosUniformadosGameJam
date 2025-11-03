using UnityEngine;

public class DespertarVisual : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeSpeed = 1f;
    public float delayBeforeFade = 1f;
    public AudioSource ambientSound;

    private bool started = false;
    private float timer = 0f;

    void Start()
    {
        fadeCanvas.alpha = 1f; // Pantalla completamente negra

        if (ambientSound != null)
            ambientSound.Play(); // Reproduce sonido ambiental al despertar
    }

    void Update()
    {
        if (!started)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeFade)
                started = true;
        }
        else
        {
            float t = Mathf.PingPong(Time.time * fadeSpeed, 1f);
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, t);

            if (Time.time > delayBeforeFade + 3f)
            {
                fadeCanvas.alpha = 0f;
                enabled = false;
            }
        }
    }
}
