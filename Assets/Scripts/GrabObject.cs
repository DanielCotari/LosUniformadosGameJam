using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public GameObject handpoint;
    private GameObject pickedObject = null;


    void Update()
    {
        if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.gameObject.transform.SetParent(null); ;
                pickedObject = null;
            }


        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            if (Input.GetKey("e") && pickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = handpoint.transform.position;
                other.gameObject.transform.SetParent(handpoint.gameObject.transform);
                pickedObject = other.gameObject;

            }


        }



    }
}
