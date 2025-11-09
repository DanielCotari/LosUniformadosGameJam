using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DeterioroPorDano : MonoBehaviour
{
    public Volume volumePostProcesado;
    public Transform camaraJugador;
    public float velocidadTambaleo = 1.5f;
    public float amplitudTambaleo = 1.5f;
    public float dañoVisualMaximo = 100f;
    public float incrementoPorGolpe = 20f;

    private float progreso = 0f;
    private bool efectosActivados = false;
    private bool reinicioProgramado = false;

    // Referencias a los efectos
    private Vignette vignette;
    private ChromaticAberration aberracion;
    private LensDistortion distorsion;
    private Bloom bloom;
    private MotionBlur blur;
    private ColorAdjustments ajustesColor;

    void Start()
    {
        volumePostProcesado.profile.TryGet(out vignette);
        volumePostProcesado.profile.TryGet(out aberracion);
        volumePostProcesado.profile.TryGet(out distorsion);
        volumePostProcesado.profile.TryGet(out bloom);
        volumePostProcesado.profile.TryGet(out blur);
        volumePostProcesado.profile.TryGet(out ajustesColor);

        // Inicializar todos los efectos en cero
        ajustesColor.saturation.value = 0f;
        vignette.intensity.value = 0f;
        aberracion.intensity.value = 0f;
        distorsion.intensity.value = 0f;
        distorsion.scale.value = 1f;
        bloom.intensity.value = 0f;
        blur.intensity.value = 0f;

        volumePostProcesado.enabled = true;
    }

    void Update()
    {
        if (efectosActivados)
        {
            float t = Mathf.Clamp01(progreso / dañoVisualMaximo);
            AplicarTransicionVisual(t);
            TambalearCamara();

            if (t >= 1f && !reinicioProgramado)
            {
                reinicioProgramado = true;
                Invoke("ReiniciarEscena", 2f);
            }
        }
    }

    public void RecibirDañoVisual(float cantidad)
    {
        progreso += cantidad;
        efectosActivados = true;
    }

    void AplicarTransicionVisual(float t)
    {
        vignette.intensity.value = Mathf.Lerp(0f, 0.8f, t);               // oscurece bordes
        aberracion.intensity.value = Mathf.Lerp(0f, 2.0f, t);             // distorsión de color fuerte
        distorsion.intensity.value = Mathf.Lerp(0f, -1.2f, t);            // curvatura agresiva
        distorsion.scale.value = Mathf.Lerp(1f, 0.8f, t);                 // achica la imagen
        bloom.intensity.value = Mathf.Lerp(0f, 2.5f, t);                  // luces exageradas
        blur.intensity.value = Mathf.Lerp(0f, 1.5f, t);                   // borrosidad más intensa
        ajustesColor.saturation.value = Mathf.Lerp(0f, 100f, t);          // saturación extrema
        // postExposure eliminado para mantener la imagen luminosa
    }

    void TambalearCamara()
    {
        if (camaraJugador != null)
        {
            float roll = Mathf.Sin(Time.time * velocidadTambaleo) * (amplitudTambaleo * 2f * (progreso / dañoVisualMaximo));
            camaraJugador.localRotation *= Quaternion.Euler(0, 0, roll);
        }
    }

    void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
