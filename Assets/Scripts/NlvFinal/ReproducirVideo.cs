using UnityEngine;
using UnityEngine.Video;

public class ReproducirVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            Debug.Log("Video iniciado.");
        }
        else
        {
            Debug.LogWarning("VideoPlayer no asignado.");
        }
    }
}
