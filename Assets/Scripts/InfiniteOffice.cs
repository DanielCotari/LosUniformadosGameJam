using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteOffice : MonoBehaviour
{
    public Transform jugador;
    public Transform camara;

    public Transform portalInicio;
    public Transform portalFinal;
    public Transform portalIzquierda;
    public Transform portalDerecha;

    public float margen = 0.5f;

    private CharacterController controller;

    void Start()
    {
        controller = jugador.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 pos = jugador.position;

        // Eje Z: adelante y atrás
        if (pos.z >= portalFinal.position.z - margen)
        {
            Teletransportar(new Vector3(pos.x, pos.y, portalInicio.position.z + margen), "Z → inicio");
        }
        else if (pos.z <= portalInicio.position.z + margen)
        {
            Teletransportar(new Vector3(pos.x, pos.y, portalFinal.position.z - margen), "Z → final");
        }

        // Eje X: derecha → izquierda
        if (pos.x >= portalDerecha.position.x - margen)
        {
            Teletransportar(new Vector3(portalIzquierda.position.x + margen, pos.y, pos.z), "X → izquierda");
        }
        // Eje X: izquierda → derecha
        else if (pos.x <= portalIzquierda.position.x + margen)
        {
            Teletransportar(new Vector3(portalDerecha.position.x - margen, pos.y, pos.z), "X → derecha");
        }
    }

    void Teletransportar(Vector3 nuevaPos, string mensaje)
    {
        controller.enabled = false;
        jugador.position = nuevaPos;
        camara.rotation = Quaternion.Euler(0, camara.eulerAngles.y, 0);
        controller.enabled = true;

        Debug.Log("Teletransportado: " + mensaje + " → Nueva posición: " + nuevaPos);
    }
}