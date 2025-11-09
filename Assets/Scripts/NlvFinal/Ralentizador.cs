using UnityEngine;

public class Ralentizador : MonoBehaviour
{
    public float factorReduccion = 0.5f;
    public float duracion = 3f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement_Juan movimiento = col.gameObject.GetComponent<PlayerMovement_Juan>();
            if (movimiento != null)
            {
                movimiento.Ralentizar(factorReduccion, duracion);
            }

            Destroy(gameObject); // opcional: destruir la palabra tras impacto
        }
    }
}
