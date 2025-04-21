using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    public RawImage videoScreen;
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    public string explanationVideoURL;
    public string exampleVideoURL;

    public GameObject playButton;
    public GameObject pauseButton;

    void Start()
    {
        videoPlayer.targetTexture = renderTexture;
        videoScreen.texture = renderTexture;

        videoScreen.gameObject.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void PlayVideoFromURL(string url)
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        videoScreen.gameObject.SetActive(true);
        vp.Play();

        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeVideo()
    {
        videoPlayer.Play();
        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        videoScreen.gameObject.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void PlayExplanation()
    {
        PlayVideoFromURL(explanationVideoURL);
    }

    public void PlayExample()
    {
        PlayVideoFromURL(exampleVideoURL);
    }
}
