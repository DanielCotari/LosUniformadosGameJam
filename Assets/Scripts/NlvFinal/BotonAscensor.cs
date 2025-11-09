using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonAscensor : MonoBehaviour
{
    public PuertaAscensor controladorPuerta;
    public float distanciaInteraccion = 3f;
    public Camera camaraJugador;
    public string escenaVideo = "AscensorVideoScene";
    public float delayAntesDeVideo = 4f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = camaraJugador.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distanciaInteraccion))
            {
                if (hit.transform == transform)
                {
                    controladorPuerta.CerrarPuerta();
                    StartCoroutine(CambiarEscenaConDelay());
                }
            }
        }
    }

    System.Collections.IEnumerator CambiarEscenaConDelay()
    {
        yield return new WaitForSeconds(delayAntesDeVideo);
        SceneManager.LoadScene(escenaVideo);
    }
}
