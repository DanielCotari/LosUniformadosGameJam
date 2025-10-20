using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public GameObject handpoint;
    public float grabDistance = 2f;
    private GameObject pickedObject = null;

    void Update()
    {
        if (pickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, grabDistance))
                {
                    if (hit.collider.CompareTag("Object"))
                    {
                        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.useGravity = false;
                            rb.isKinematic = true;
                            hit.transform.position = handpoint.transform.position;
                            hit.transform.SetParent(handpoint.transform);
                            pickedObject = hit.collider.gameObject;

                           
                            pickedObject.tag = "Informe";
                        }
                    }
                }

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
        }
    }
}
