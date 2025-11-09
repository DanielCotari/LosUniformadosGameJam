using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [System.Serializable]
    public class Task
    {
        public string name;
        public bool completed;
    }

    public List<Task> tasks = new List<Task>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CompleteTask(string taskName)
    {
        var task = tasks.Find(t => t.name == taskName);
        if (task != null && !task.completed)
        {
            task.completed = true;
            Debug.Log($"✅ Tarea completada: {taskName}");
        }
        else
        {
            Debug.Log($"⚠️ Tarea '{taskName}' no encontrada o ya completada.");
        }
    }

    public bool AllTasksCompleted()
    {
        return tasks.TrueForAll(t => t.completed);
    }
}
