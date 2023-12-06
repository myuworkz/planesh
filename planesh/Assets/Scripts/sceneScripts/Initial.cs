using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initial : Scene
{
    private string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SuperSceneManager.setCurrentSceneName(sceneName);
        Debug.Log(sceneName);
        Debug.Log("currentScene is " + sceneName);
    }

   public override void nextStage()
   {
       SuperSceneManager.nextStage();
   }
}
