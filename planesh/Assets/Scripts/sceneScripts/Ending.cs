using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Ending : Scene
{
    private string sceneName;
    [SerializeField] VideoPlayer videoPlayer;

    public override void nextStage()
    {
        SuperSceneManager.nextStage();
    }


    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SuperSceneManager.setCurrentSceneName(sceneName);
        Debug.Log("currentScene is " + sceneName);

        //video
        videoPlayer.isLooping = true;
        videoPlayer.loopPointReached += FinishPlayingVideo;

        //!!!Delete when the video arrived!!!!!
        //this is just for debuging: change the scene
        //nextStage();
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void FinishPlayingVideo(VideoPlayer vp)
    {
        videoPlayer.Stop();
        nextStage();
    }

}