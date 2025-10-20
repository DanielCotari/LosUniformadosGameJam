using UnityEngine;
using TMPro;

public class DeliveryReport : MonoBehaviour
{
    public GameObject mensajeSalida;
    public GameObject mensajeInicial;
    public TextMeshPro textoDia1; // ← nuevo campo para el check
    public AudioSource audioSource;
    public AudioClip sonidoCheck;

    private bool informeEntregado = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Informe") && !informeEntregado)
        {
            informeEntregado = true;

            Destroy(other.gameObject);

            if (mensajeInicial != null)
                mensajeInicial.SetActive(false);

            if (mensajeSalida != null)
                mensajeSalida.SetActive(true);

            // ✅ Marcar el check en la hoja
            if (textoDia1 != null)
                textoDia1.text = "Día 1: ✓";

            // 🔊 Reproducir sonido de check
            if (audioSource != null && sonidoCheck != null)
                audioSource.PlayOneShot(sonidoCheck);


            Debug.Log("Informe entregado. Check marcado. Ahora marca tu salida.");
        }
    }
}
