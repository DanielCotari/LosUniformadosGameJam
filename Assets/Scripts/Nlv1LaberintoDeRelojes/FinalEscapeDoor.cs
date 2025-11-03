using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalEscapeDoor : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = 95f;
    public float doorCloseAngle = 0f;
    public float smooth = 3f;
    public Transform doorPivot; // Asigna el Empty que rota

    public string sceneToLoad;
    public Light doorLight;
    public GameObject portalPlane;
    public Transform doorTransform; // Asigna solo la puerta, no el marco

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        initialRotation = doorPivot.localRotation;
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
                if (hit.transform.GetComponentInParent<FinalEscapeDoor>() == this && hit.transform.CompareTag("Door"))
                {
                    doorOpen = !doorOpen;
                    float angle = doorOpen ? doorOpenAngle : doorCloseAngle;
                    targetRotation = initialRotation * Quaternion.Euler(0, 0, angle);

                    if (doorOpen)
                        ActivatePortal();
                    else
                        DeactivatePortal();
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

        doorPivot.localRotation = Quaternion.Slerp(doorPivot.localRotation, targetRotation, smooth * Time.deltaTime);


    }

    void ActivatePortal()
    {
        if (doorLight != null)
            doorLight.enabled = true;

        if (portalPlane != null)
            portalPlane.SetActive(true);
    }

    void DeactivatePortal()
    {
        if (doorLight != null)
            doorLight.enabled = false;

        if (portalPlane != null)
            portalPlane.SetActive(false);
    }

    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        Vector3 origin = Camera.main.transform.position;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * 3f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(origin, Camera.main.transform.forward * 0.5f);
    }
}
