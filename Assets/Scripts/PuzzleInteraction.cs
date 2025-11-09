using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PuzzleInteraction : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public GameObject puzzleUI;
    public string taskName;
    public AudioSource audioSource;
    public AudioClip[] audioClips; // clips de sonido para la acción

    [Header("Scene Settings")]
    public string nextSceneAfterPuzzle; // escena a cargar después de resolver puzzle

    private bool puzzleActive = false;
    private bool isSolved = false;

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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 🔹 Acción según el tag del objeto
        if (gameObject.CompareTag("cama"))
        {
            Debug.Log("Cama: cambiando de escena");
            EndPuzzle();
            SceneManager.LoadScene("Dream2"); // pon el nombre exacto
        }
        else if (gameObject.CompareTag("Pepsi"))
        {
            Debug.Log("Pepsi: reproduciendo sonidos y destruyendo objeto");
            StartCoroutine(PlayTwoSoundsAndDestroy(gameObject));
        }
    }

    public void EndPuzzle()
    {
        if (!puzzleActive) return;
        puzzleActive = false;

        if (puzzleUI != null)
            puzzleUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnPuzzleSolved()
    {
        if (isSolved) return;
        isSolved = true;

        // Marca la tarea como completada
        if (!string.IsNullOrEmpty(taskName))
            TaskManager.Instance.CompleteTask(taskName);

        EndPuzzle();

        // Cambia de escena si está definida
        if (!string.IsNullOrEmpty(nextSceneAfterPuzzle))
            SceneManager.LoadScene(nextSceneAfterPuzzle);
    }

    // Para Pepsi: reproduce dos sonidos y destruye
    public IEnumerator PlayTwoSoundsAndDestroy(GameObject targetPepsi)
    {
        Destroy(targetPepsi);

        if (audioSource != null && audioClips.Length >= 2)
        {
            for (int i = 0; i < 2; i++)
            {
                audioSource.clip = audioClips[i];
                audioSource.Play();
                yield return new WaitForSeconds(audioClips[i].length);
            }
        }

        // Marca puzzle como resuelto
        OnPuzzleSolved();
    }
}
