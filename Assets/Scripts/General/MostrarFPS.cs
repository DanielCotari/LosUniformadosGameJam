using UnityEngine;

public class MostrarFPS : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle estilo = new GUIStyle();
        Rect rect = new Rect(10, 10, w, h * 2 / 100);
        estilo.alignment = TextAnchor.UpperLeft;
        estilo.fontSize = h * 2 / 50;
        estilo.normal.textColor = Color.white;

        float fps = 1.0f / deltaTime;
        string texto = string.Format("{0:0.} FPS", fps);
        GUI.Label(rect, texto, estilo);
    }
}
