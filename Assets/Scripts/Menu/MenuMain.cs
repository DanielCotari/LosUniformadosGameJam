using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string newGameSceneName = "Nlv1LaberintoDeRelojes"; // Cambia "X" por el nombre real de tu escena

    public void NewGame()
    {
        if (!string.IsNullOrEmpty(newGameSceneName))
            SceneManager.LoadScene(newGameSceneName);
        else
            Debug.LogWarning("No se ha asignado el nombre de la escena.");
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
