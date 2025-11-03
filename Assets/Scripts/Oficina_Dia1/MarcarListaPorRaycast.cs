using UnityEngine;
using TMPro;

public class MarcarListaPorRaycast : MonoBehaviour
{
    public float distanciaInteraccion = 3f;
    public LayerMask capaInteractiva;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanciaInteraccion, capaInteractiva))
            {
                if (hit.collider.CompareTag("Lista"))
                {
                    HojaDeTareas hoja = hit.collider.GetComponent<HojaDeTareas>();
                    if (hoja != null)
                    {
                        hoja.MarcarCheck();
                    }
                }
            }
        }
    }
}
