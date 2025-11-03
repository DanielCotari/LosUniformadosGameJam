using UnityEngine;

public class RaycastDetector : MonoBehaviour
{
    public float distanciaRaycast = 5f;
    public LayerMask capaPuertas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanciaRaycast, capaPuertas))
            {
                PuertaRaycast puerta = hit.collider.GetComponent<PuertaRaycast>();
                if (puerta != null)
                {
                    puerta.ActivarPuerta();
                }
            }
        }
    }
}
