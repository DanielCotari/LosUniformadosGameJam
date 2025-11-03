using UnityEngine;

public class MenuDoorRaycast : MonoBehaviour
{
    public float openAngle = 95f;
    public float closeAngle = 0f;
    public float smooth = 3f;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Door") && hit.transform.IsChildOf(transform))
                {
                    isOpen = !isOpen;
                    float angle = isOpen ? openAngle : closeAngle;
                    targetRotation = initialRotation * Quaternion.Euler(0, 0, angle);

                    if (doorLight != null)
                        doorLight.enabled = isOpen;

                    if (portalPlane != null)
                        portalPlane.SetActive(isOpen);
                }
            }
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
