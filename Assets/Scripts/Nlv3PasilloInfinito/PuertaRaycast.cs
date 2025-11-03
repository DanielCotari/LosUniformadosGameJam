using UnityEngine;

public class PuertaRaycast : MonoBehaviour
{
    public AudioClip sonidoAmbiental;
    public bool esPuertaCorrecta = false;
    public GameObject puertaVisual;

    private AudioSource audioSource;
    private bool sonidoReproducido = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (sonidoAmbiental != null)
        {
            audioSource.clip = sonidoAmbiental;
            audioSource.playOnAwake = false;
        }
    }

    public void ActivarPuerta()
    {
        if (sonidoAmbiental != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (esPuertaCorrecta)
        {
            AbrirPuerta();
        }
        else
        {
            Debug.Log("Puerta bloqueada por sonido emocional.");
        }
    }

    public void AsignarSonido(AudioClip clip)
    {
        sonidoAmbiental = clip;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogError("Falta AudioSource en " + gameObject.name);
        }
    }


    void AbrirPuerta()
    {
        if (puertaVisual != null)
            puertaVisual.SetActive(false);

        Debug.Log("¡Puerta correcta abierta!");
    }
}
