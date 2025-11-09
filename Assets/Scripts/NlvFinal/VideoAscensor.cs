using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoAscensor : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public string escenaSiguiente = "Menu"; // nombre de la escena que sigue

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(escenaSiguiente);
    }
}
