using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class EndingVideoPlayer : VisibilityController
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private UnityEvent winGameEvent;

    private void Start()
    {
        player.loopPointReached += EndReached;
    }

    public void PlayVideo()
    {
        player.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {        
        winGameEvent.Invoke();
    }
}
