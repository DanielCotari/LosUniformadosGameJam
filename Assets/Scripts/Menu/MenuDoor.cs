using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoor : MonoBehaviour
{
    [Header("Rotación")]
    public float openAngle = 95f;
    public float closeAngle = 0f;
    public float smooth = 3f;

    [Header("Escena")]
    public string sceneToLoad;

    [Header("Efectos visuales")]
    public Light doorLight;
    public GameObject portalPlane;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isOpen = false;

    void Start()
    {
        initialRotation = transform.localRotation;
        targetRotation = initialRotation * Quaternion.Euler(0, 0, closeAngle);

        if (doorLight != null)
            doorLight.enabled = false;

        if (portalPlane != null)
            portalPlane.SetActive(false);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    void OnMouseEnter()
    {
        // Opcional: resaltar puerta al pasar el cursor
        GetComponent<Renderer>().material.color = Color.white * 1.2f;
    }

    void OnMouseExit()
    {
        // Restaurar color original
        GetComponent<Renderer>().material.color = Color.white;
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            isOpen = true;
            targetRotation = initialRotation * Quaternion.Euler(0, 0, openAngle);

            if (doorLight != null)
                doorLight.enabled = true;

            if (portalPlane != null)
                portalPlane.SetActive(true);
        }
        else
        {
            // Cargar escena si está definida
            if (!string.IsNullOrEmpty(sceneToLoad))
                SceneManager.LoadScene(sceneToLoad);
        }
    }
}
