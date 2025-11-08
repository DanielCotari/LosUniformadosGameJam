using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public GameObject interactionText; // referencia al texto UI "Presiona E para interactuar"

    private PuzzleInteraction currentPuzzle;

    void Update()
    {
        DetectInteractable();

        if (currentPuzzle != null && Input.GetKeyDown(KeyCode.E))
        {
            currentPuzzle.StartPuzzle();
        }
    }

   void DetectInteractable()
{
    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
    {
        PuzzleInteraction puzzle = hit.collider.GetComponent<PuzzleInteraction>();
        if (puzzle != null)
        {
            interactionText.SetActive(true);  // ? LÍNEA 23 (la que lanza el error)
            currentPuzzle = puzzle;
            return;
        }
    }

    interactionText.SetActive(false);
    currentPuzzle = null;
}

}
