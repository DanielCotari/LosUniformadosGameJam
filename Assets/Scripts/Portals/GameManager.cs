using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<string> nivelesCompletados = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarcarNivelCompletado(string nombreNivel)
    {
        if (!nivelesCompletados.Contains(nombreNivel))
            nivelesCompletados.Add(nombreNivel);
    }

    public bool TodosLosNivelesCompletados()
    {
        return nivelesCompletados.Count >= 4; // ajusta según tu diseño
    }
}
