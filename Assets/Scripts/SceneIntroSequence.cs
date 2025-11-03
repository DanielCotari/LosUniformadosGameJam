using UnityEngine;
using TMPro;

public class SceneIntroSequence : MonoBehaviour
{
    public TextMeshProUGUI subtitleText;
    public TextMeshProUGUI timerText;
    public GameObject enemy;
    public AudioSource alarmSound;

    public float subtitleDisplayTime = 4f;
    public float countdownTime = 120f;

    private string[] messages = new string[]
    {
        "¿Cuánto tiempo más vas a seguir huyendo?",
        "El reloj no se detiene.",
        "Acepta que el tiempo avanza.",
        "Solo entonces podrás escapar."
    };

    private int currentIndex = 0;
    private float timer = 0f;
    private bool narrationFinished = false;
    private float countdown;

    void Start()
    {
        subtitleText.text = messages[currentIndex];
        timerText.gameObject.SetActive(false);
        enemy.SetActive(false);
        countdown = countdownTime;
    }

    void Update()
    {
        if (!narrationFinished)
        {
            timer += Time.deltaTime;

            if (timer >= subtitleDisplayTime)
            {
                currentIndex++;
                timer = 0f;

                if (currentIndex < messages.Length)
                {
                    subtitleText.text = messages[currentIndex];
                }
                else
                {
                    narrationFinished = true;
                    subtitleText.text = "";

                    // Activar alarma
                    if (alarmSound != null)
                        alarmSound.Play();

                    // Activar contador y enemigo
                    timerText.gameObject.SetActive(true);
                    enemy.SetActive(true);
                }
            }
        }
        else
        {
            // Actualizar contador
            countdown -= Time.deltaTime;
            countdown = Mathf.Max(0, countdown);

            int minutes = Mathf.FloorToInt(countdown / 60);
            int seconds = Mathf.FloorToInt(countdown % 60);

            timerText.text = $"Tiempo restante: {minutes:00}:{seconds:00}";
        }
    }
}
