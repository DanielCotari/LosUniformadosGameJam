using UnityEngine;

public class IndicadorInteraccion : MonoBehaviour
{
    public GameObject textoIndicador;
    public float distanciaMaxima = 3f;
    public Camera camaraJugador;

    private bool visible = false;

    void Update()
    {
        Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMaxima))
        {
            bool debeMostrar = hit.transform == transform;

            if (debeMostrar != visible)
            {
                textoIndicador.SetActive(debeMostrar);
                visible = debeMostrar;
            }
        }
        else if (visible)
        {
            textoIndicador.SetActive(false);
            visible = false;
        }
    }
}
