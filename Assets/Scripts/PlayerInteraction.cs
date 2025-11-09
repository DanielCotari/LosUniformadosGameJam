using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public GameObject interactionText;

    private PuzzleInteraction currentPuzzle;

    void Update()
    {
        DetectInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentPuzzle != null)
            {
                // Si es un puzzle normal, empieza el puzzle
                currentPuzzle.StartPuzzle();
            }
            else
            {
                TryInteractWithObject();
            }
        }
    }

    void DetectInteractable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // DEBUG: dibuja el raycast
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            PuzzleInteraction puzzle = hit.collider.GetComponent<PuzzleInteraction>();
            if (puzzle != null)
            {
                interactionText.SetActive(true);
                currentPuzzle = puzzle;
                Debug.Log("Puzzle detectado: " + puzzle.name);
                return;
            }
        }

        interactionText.SetActive(false);
        currentPuzzle = null;
    }

    void TryInteractWithObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // DEBUG: dibuja el raycast
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green);

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            GameObject target = hit.collider.gameObject;
            Debug.Log("Objeto detectado con tag: " + target.tag);

            // Interacción con Cama → cambia de escena
            if (target.CompareTag("Cama"))
            {
                Debug.Log("Interacción con Cama");
                SceneManager.LoadScene("NombreDeEscenaCama"); // Pon el nombre correcto
            }

            // Interacción con Pepsi → reproduce dos sonidos y destruye
            else if (target.CompareTag("Pepsi"))
            {
                PuzzleInteraction puzzle = target.GetComponent<PuzzleInteraction>();
                if (puzzle != null)
                {
                    StartCoroutine(puzzle.PlayTwoSoundsAndDestroy(target));
                }
            }
        }
    }
}
