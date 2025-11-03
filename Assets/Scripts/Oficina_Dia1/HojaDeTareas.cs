using UnityEngine;
using TMPro;

public class HojaDeTareas : MonoBehaviour
{
    public TextMeshPro textoDia1;
    public AudioSource audioSource;
    public AudioClip sonidoCheck;

    private bool tareaMarcada = false;

    public void MarcarCheck()
    {
        if (!tareaMarcada)
        {
            textoDia1.text = "Día 1: ✓";
            tareaMarcada = true;

            if (audioSource != null && sonidoCheck != null)
            {
                audioSource.PlayOneShot(sonidoCheck);
            }

            Debug.Log("Check marcado en la hoja.");
        }
    }
}
