using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemDoor : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = 95f;
    public float doorCloseAngle = 0.0f;
    public float smooth = 3.0f;
    public bool isFinalDoor = false;
    public string sceneToLoad; // nombre de la escena a cargar
    public Light doorLight; // luz blanca dentro de la puerta
    public GameObject portalPlane; // plane blanco que simula el portal

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
        targetRotation = initialRotation * Quaternion.Euler(0, 0, doorCloseAngle);

        if (doorLight != null)
            doorLight.enabled = false;

        if (portalPlane != null)
            portalPlane.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            int doorLayerMask = ~(1 << LayerMask.NameToLayer("PortalFX"));
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f, doorLayerMask))
            {
                if (hit.transform.GetComponentInParent<SystemDoor>() == this && hit.transform.CompareTag("Door"))
                {
                    if (isFinalDoor && !CanUnlockFinalDoor())
                        return;

                    doorOpen = !doorOpen;
                    float angle = doorOpen ? doorOpenAngle : doorCloseAngle;
                    targetRotation = initialRotation * Quaternion.Euler(0, 0, angle);

                    if (doorOpen)
                        ActivateLightAndPortal();
                    else
                        DeactivateLightAndPortal();
                }
            }
        }

      
        int portalLayerMask = 1 << LayerMask.NameToLayer("PortalFX");
        Ray rayToPortal = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(rayToPortal, out RaycastHit portalHit, 0.5f, portalLayerMask))
        {
            if (doorOpen && portalHit.transform.CompareTag("Portal"))
            {
                if (!string.IsNullOrEmpty(sceneToLoad))
                    SceneManager.LoadScene(sceneToLoad);
            }
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    void ActivateLightAndPortal()
    {
        if (doorLight != null)
            doorLight.enabled = true;

        if (portalPlane != null)
            portalPlane.SetActive(true);
    }

    void DeactivateLightAndPortal()
    {
        if (doorLight != null)
            doorLight.enabled = false;

        if (portalPlane != null)
            portalPlane.SetActive(false);
    }

    bool CanUnlockFinalDoor()
    {
        return GameManager.Instance.nivelesCompletados.Count >= 4;
    }

    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Vector3 origin = Camera.main.transform.position;

        // Raycast hacia la puerta (cian)
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * 3f);

        // Raycast hacia el portal (magenta, más corto)
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * 0.5f);
    }
}
