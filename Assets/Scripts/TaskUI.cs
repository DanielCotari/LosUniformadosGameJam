using UnityEngine;
using TMPro; // Para TextMeshPro

public class TaskUI : MonoBehaviour
{
    public TextMeshProUGUI taskListText;

    void Update()
    {
        if (TaskManager.Instance == null || taskListText == null) return;

        string listText = "<b><size=24>Tareas del Día</size></b>\n\n";

        foreach (var task in TaskManager.Instance.tasks)
        {
            if (task.completed)
                listText += $"<color=green>✔ {task.name}</color>\n";
            else
                listText += $"<color=white>☐ {task.name}</color>\n";
        }

        // Actualiza el texto en pantalla
        taskListText.text = listText;
    }
}
