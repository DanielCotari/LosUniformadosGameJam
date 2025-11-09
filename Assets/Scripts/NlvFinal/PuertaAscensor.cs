using UnityEngine;

public class PuertaAscensor : MonoBehaviour
{
    public Transform puertaIzquierda;
    public float velocidad = 2f;
    public AudioSource audioPuerta; // nuevo campo
    public AudioClip sonidoCierre;  // clip de sonido

    private Vector3 posicionCerrada;
    private bool cerrar = false;
    private bool sonidoReproducido = false;

    void Start()
    {
        posicionCerrada = new Vector3(2.17f, puertaIzquierda.localPosition.y, puertaIzquierda.localPosition.z);
    }

    public void CerrarPuerta()
    {
        cerrar = true;

        if (!sonidoReproducido && sonidoCierre != null && audioPuerta != null)
        {
            audioPuerta.clip = sonidoCierre;
            audioPuerta.Play();
            sonidoReproducido = true;
        }
    }

    void Update()
    {
        if (cerrar)
        {
            puertaIzquierda.localPosition = Vector3.MoveTowards(
                puertaIzquierda.localPosition, posicionCerrada, velocidad * Time.deltaTime);
        }
    }
}
