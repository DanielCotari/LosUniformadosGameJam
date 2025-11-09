using UnityEngine;

public class MuroEmpujador : MonoBehaviour
{
    public float xDestino;
    public float velocidad = 10f;
    public float fuerzaEmpuje = 500f; 
    public float impulsoVertical = 5f;

    void Update()
    {
        Vector3 destino = new Vector3(xDestino, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direccionAtras = (other.transform.position - transform.position).normalized;
                Vector3 impulso = direccionAtras * fuerzaEmpuje + Vector3.up * impulsoVertical;
                rb.velocity = impulso;
            }
        }
    }
}
