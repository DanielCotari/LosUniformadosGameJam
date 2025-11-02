using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "office"; // Cambia por el nombre real de tu siguiente escena

    void Start()
    {   
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Cuando termina el video, carga la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
