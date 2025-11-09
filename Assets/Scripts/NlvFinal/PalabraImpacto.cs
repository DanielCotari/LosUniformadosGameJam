using UnityEngine;

public class PalabraImpacto : MonoBehaviour
{
    public float daño = 10f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            VidaJugador vida = col.gameObject.GetComponent<VidaJugador>();
            if (vida != null)
            {
                vida.RecibirDaño(daño);
            }

            Debug.Log("Golpeado por palabra: " + gameObject.name);
            Destroy(gameObject); // opcional
        }
    }
}
