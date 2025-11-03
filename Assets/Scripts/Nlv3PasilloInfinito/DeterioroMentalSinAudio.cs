using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DeterioroMentalSinAudio : MonoBehaviour
{
    public float tiempoParaActivar = 60f;
    public float duracionTransicion = 30f;
    public Volume volumePostProcesado;
    public Transform camaraJugador;
    public float velocidadTambaleo = 1.5f;
    public float amplitudTambaleo = 1.5f;

    private float tiempoAcumulado = 0f;
    private bool efectosActivados = false;
    private float progreso = 0f;

    // Referencias a los efectos
    private Vignette vignette;
    private ChromaticAberration aberracion;
    private LensDistortion distorsion;
    private Bloom bloom;
    private MotionBlur blur;

    private bool reinicioProgramado = false;
    private ColorAdjustments ajustesColor;


    void Start()
    {
        volumePostProcesado.profile.TryGet(out vignette);
        volumePostProcesado.profile.TryGet(out aberracion);
        volumePostProcesado.profile.TryGet(out distorsion);
        volumePostProcesado.profile.TryGet(out bloom);
        volumePostProcesado.profile.TryGet(out blur);
        volumePostProcesado.profile.TryGet(out ajustesColor);
        ajustesColor.postExposure.value = 0f;

        // Inicializar en cero
        vignette.intensity.value = 0f;
        aberracion.intensity.value = 0f;
        distorsion.intensity.value = 0f;
        bloom.intensity.value = 0f;
        blur.intensity.value = 0f;

        volumePostProcesado.enabled = true;
    }

    void Update()
    {
        tiempoAcumulado += Time.deltaTime;

        if (!efectosActivados && tiempoAcumulado >= tiempoParaActivar)
        {
            efectosActivados = true;
        }

        if (efectosActivados)
        {
            progreso += Time.deltaTime / duracionTransicion;
            AplicarTransicionVisual(Mathf.Clamp01(progreso));
            TambalearCamara();

            if (progreso >= 1f && !reinicioProgramado)
            {
                reinicioProgramado = true;
                Invoke("ReiniciarEscena", 2f); // espera 2 segundos después de completar la transición
            }

        }
    }

    void AplicarTransicionVisual(float t)
    {
        vignette.intensity.value = Mathf.Lerp(0f, 0.8f, t);               // más oscuro en los bordes
        aberracion.intensity.value = Mathf.Lerp(0f, 1.0f, t);             // distorsión de color fuerte
        distorsion.intensity.value = Mathf.Lerp(0f, -0.6f, t);            // curvatura más pronunciada
        bloom.intensity.value = Mathf.Lerp(0f, 2.5f, t);                  // luces exageradas
        blur.intensity.value = Mathf.Lerp(0f, 1.2f, t);                   // borrosidad más intensa
        ajustesColor.postExposure.value = Mathf.Lerp(0f, -5f, t); // oscurece progresivamente
        vignette.intensity.value = Mathf.Lerp(0f, 1.0f, t);        // cierre total en los bordes

    }


    void TambalearCamara()
    {
        if (camaraJugador != null)
        {
            float roll = Mathf.Sin(Time.time * velocidadTambaleo) * (amplitudTambaleo * 1.5f);
            camaraJugador.localRotation *= Quaternion.Euler(0, 0, roll);
        }
    }


    void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
