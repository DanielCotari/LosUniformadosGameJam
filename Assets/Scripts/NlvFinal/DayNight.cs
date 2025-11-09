using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [Range(0.0f, 24f)] public float Hora = 12f;
    public Transform Sol;

    private void Update()
    {
        RotarSol();
    }

    void RotarSol()
    {
        float SolX = (Hora / 24f) * 360f; // Rota en base a la hora del día
        Sol.localEulerAngles = new Vector3(SolX, 0, 0);
    }
}
