using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public static VideoPlayerScript Instance;

    private VideoPlayer videoPlayer;

    void Start()
    {
        Instance = this;
        GameObject camera = Camera.main.gameObject;
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = GetComponent<MeshRenderer>().material.mainTexture as RenderTexture;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
        videoPlayer.isLooping = true;
        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo(string url)
    {
        if (videoPlayer.isPlaying || videoPlayer.isPaused)
        {
            videoPlayer.Stop();
        }
        videoPlayer.url = url;
        videoPlayer.Play();
    }

    public float GetDuration(string url)
    {
        videoPlayer.url = url;
        videoPlayer.Play();
        float fc = videoPlayer.frameCount;
        float duration = fc / videoPlayer.frameRate;
        return duration;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Pause();
    }
}