using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GritoShockwave : MonoBehaviour
{
    public float radioMaximo = 10f;
    public float velocidadExpansion = 5f;
    public float duracion = 1f;
    private float tiempo;

    void Update()
    {
        tiempo += Time.deltaTime;
        float radioActual = Mathf.Lerp(0f, radioMaximo, tiempo / duracion);
        transform.localScale = Vector3.one * radioActual;

        if (tiempo >= duracion)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement_Juan movimiento = other.GetComponent<PlayerMovement_Juan>();
            if (movimiento != null)
            {
                movimiento.Ralentizar(2f, 3f); 
            }
        }
    }
}

