using UnityEngine;

public class GeneradorEstaciones : MonoBehaviour
{
    public GameObject prefabEstacion;
    public int filas = 4;      // Z (profundidad)
    public int columnas = 4;   // X (horizontal)
    public float separacionX = 3.5f;
    public float separacionZ = 3.5f;
    public Vector3 origen = new Vector3(-10f, 0f, -6f);


    void Start()
    {
        for (int x = 0; x < columnas; x++)
        {
            for (int z = 0; z < filas; z++)
            {
                Vector3 posicion = new Vector3(
                    origen.x + x * separacionX,
                    origen.y,
                    origen.z + z * separacionZ
                );

                Instantiate(prefabEstacion, posicion, Quaternion.identity);
            }
        }
    }
}
