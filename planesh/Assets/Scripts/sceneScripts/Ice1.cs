using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ice1 : Scene
{
    private string sceneName;

    //audio
    AudioSource audioData;
    [SerializeField] AudioClip sound;


    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SuperSceneManager.setCurrentSceneName(sceneName);
        Debug.Log("currentScene is " + sceneName);

        //audio
        audioData = GetComponent<AudioSource>();
    }

   public override void nextStage()
   {
       SuperSceneManager.nextStage();
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
       Debug.Log("collide");
       if(collision.gameObject.tag == "Ball")
        {
            Invoke("nextStage", 1.0f);
            audioData.Play();
        }
   }
}
