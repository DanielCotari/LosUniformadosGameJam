using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisionGris : MonoBehaviour
{
    public PostProcessVolume volume;
    private ColorGrading colorGrading;

    void Start()
    {
        volume.profile.TryGetSettings(out colorGrading);
    }

    public void SetGris(bool activo)
    {
        colorGrading.saturation.value = activo ? -100f : 0f;
    }
}
