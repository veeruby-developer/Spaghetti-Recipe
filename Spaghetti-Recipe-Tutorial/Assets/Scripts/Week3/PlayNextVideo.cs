using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayNextVideo : MonoBehaviour
{
    public VideoClip[] videos;
    private VideoPlayer videoPlayer;
    private int videoClipIndex;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {

        videoPlayer.clip = videos[videoClipIndex];

    }

    // Update is called once per frame
    public void Next()
    {
        videoClipIndex++;
        //videoPlayer.clip = videos[videoClipIndex];
        videoPlayer.Play();
        if (videoClipIndex > 2)
        {
            videoClipIndex = 0;
        }
        videoPlayer.clip = videos[videoClipIndex];
    }
}
