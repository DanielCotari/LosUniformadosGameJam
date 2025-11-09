using UnityEngine;
using UnityEngine.SceneManagement;

public class MuertePorCaida : MonoBehaviour
{
    public Transform jugador;
    public float limiteY = -10f; // Ajusta según tu escena

    void Update()
    {
        if (jugador.position.y < limiteY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
