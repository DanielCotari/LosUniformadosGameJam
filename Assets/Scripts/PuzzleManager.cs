using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int[] correctOrder = { 2, 0, 1 }; // el orden correcto (puedes cambiarlo)
    private int currentStep = 0;

    public PuzzleInteraction puzzleInteraction; // referencia para terminar el puzzle

    void Start()
    {
        if (puzzleInteraction == null)
            puzzleInteraction = GetComponentInParent<PuzzleInteraction>();
    }

    public void PressButton(int index)
    {
        if (index == correctOrder[currentStep])
        {
            currentStep++;
            Debug.Log("Correcto: " + index);

            if (currentStep >= correctOrder.Length)
            {
                PuzzleSolved();
            }
        }
        else
        {
            Debug.Log("Incorrecto. Reiniciando.");
            currentStep = 0;
            // (Opcional) feedback visual: parpadeo rojo, sonido, etc.
        }
    }

    private void PuzzleSolved()
    {
        Debug.Log("¡Puzzle resuelto!");
        currentStep = 0;

        if (puzzleInteraction != null)
            puzzleInteraction.OnPuzzleSolved();
    }
}
