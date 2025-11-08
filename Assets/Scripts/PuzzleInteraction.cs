using UnityEngine;

public class PuzzleInteraction : MonoBehaviour
{
    public GameObject puzzleUI;  // Panel del minijuego (PuzzleUI)
    public GameObject player;    // referencia al Player (arrastrar en inspector)
    private bool puzzleActive = false;
    private bool isSolved = false; // evita resolver varias veces

    void Start()
    {
        if (puzzleUI != null)
            puzzleUI.SetActive(false);
    }

    public void StartPuzzle()
    {
        if (puzzleActive || isSolved) return;
        puzzleActive = true;

        if (puzzleUI != null)
            puzzleUI.SetActive(true);

        var pm = player.GetComponent<PlayerMovement>();
        if (pm != null)
            pm.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndPuzzle()
    {
        if (!puzzleActive) return;
        puzzleActive = false;

        if (puzzleUI != null)
            puzzleUI.SetActive(false);

        var pm = player.GetComponent<PlayerMovement>();
        if (pm != null)
            pm.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // --- Método que pide PuzzleManager al resolverse ---
    public void OnPuzzleSolved()
    {
        if (isSolved) return;
        isSolved = true;

        // Aquí añades lo que ocurra al resolver (abrir puerta, sonido, etc.)
        EndPuzzle();
    }
}
