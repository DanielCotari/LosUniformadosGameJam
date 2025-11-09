using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(float cantidad)

    {
        vidaActual -= cantidad;
        Debug.Log("Vida actual: " + vidaActual);

        FindObjectOfType<DeterioroPorDano>()?.RecibirDañoVisual(cantidad);

        if (vidaActual <= 0)
        {
            Debug.Log("¡Jugador derrotado!");
        }
    }

}
