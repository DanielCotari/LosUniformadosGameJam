using UnityEngine;

public class EnemyKillOnTouch : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí puedes activar una animación, sonido o pantalla de muerte
            Debug.Log("El jugador ha sido alcanzado por el tiempo. Fin del juego.");
            // Destroy(other.gameObject); // o activar GameOver
        }
    }
}
