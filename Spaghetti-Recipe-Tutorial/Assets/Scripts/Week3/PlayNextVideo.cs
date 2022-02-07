using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayNextVideo : MonoBehaviour
{
    [Header("Videos List")]
    public VideoClip[] videos;
    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    //Video starts to play on awake
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start()
    {
        videoPlayer.clip = videos[videoClipIndex];
    }

    // Video Change logic
    public void Next()
    {
        videoClipIndex++;
        videoPlayer.Play();
        if (videoClipIndex > 2)
        {
            videoClipIndex = 0;
        }
        videoPlayer.clip = videos[videoClipIndex];
    }
}
