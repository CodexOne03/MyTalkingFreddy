using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public static VideoPlayerScript Instance;

    private VideoPlayer videoPlayer;

    void Start()
    {
        GameObject camera = Camera.main.gameObject;
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
        videoPlayer.isLooping = true;
        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayVideo(string url)
    {
        videoPlayer.url = url;
        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }
}